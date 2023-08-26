﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
using UnityEngine.UI;



public class Cameracontroller : MonoBehaviour
    {

        private Transform car;
    private Transform takip;

        public float distance;
        public float height;
        public float rotationdamping;
        public float heightdamping;
        public float zoomratio;
        public float defaultPOV;
        public float rotation_vector;
        [SerializeField] private float ileriye_alma;
         private bool verial,arabapatladi,sola_degme_var=false,saga_degme_var=false,dur=false,arka_degme;
    public bool ileriye_git;
    private RaycastHit saghit,arkahit,solhit;
    private float sola_donus=0,donus_hiz,gidis_hiz,hiz_oranı,mesafe;
    private int sagsol;
    private Vector3 yeni_konum, local_velocity;
    // public Transform on_kısım;


    [Header("COİN AYARLARI")]
    /// ///////////////// BURASI COİNLERLE İLGİLİ BÖLME////////////////
    public Sprite[] coinler;
    public Image para;
    private Image[] paralar;
    private float coin_time, coin_bekle = 0.01f, coinx, coiny;
    private int coin_sira;
    public GameObject canvas;
    private int para_sayisi = 6, ilk_para, sira_para;
    public Text skortxt;
    public Vector3 kaybolan;
    public bool para_goster = false;
    private float yan_carpma,yan_isin_uzunluk,arka_isin_uzunluk,carpma_mesafesi,donme_ust,yaklasma,fark,mydistance;
    private GameObject kup,dur_kup;//dur_kup silmen gerekebilir
    









    private void Start()
    {

        car = GameObject.FindGameObjectWithTag("Player").transform;
        donus_hiz = 0.9f;
        yan_isin_uzunluk = 2.5f;
        arka_isin_uzunluk = 2.5f;


        /// ///////////////// BURASI COİNLERLE İLGİLİ BÖLME////////////////
        for (int i = 0; i < 32; i++)
        {
            if (i<10)
            {
            coinler[i] = Resources.Load<Sprite>("sprite/coin000" + i);
            }
            else { 
            coinler[i] = Resources.Load<Sprite>("sprite/coin00" + i);
            }


        }


        sira_para = 0;
        ilk_para = sira_para - (para_sayisi - 1);



        coin_time = Time.time + coin_bekle;
        coin_sira = 0;
        para.sprite = coinler[coin_sira];
        ////////////////////////////////////////////////////
        paralar = new Image[para_sayisi];
        for (int i = 0; i < para_sayisi; i++)
        {

            paralar[i] = Image.Instantiate(para);
            //paralar[i].enabled = false;
            paralar[i].rectTransform.SetParent(canvas.transform);
            paralar[i].sprite = coinler[coin_sira];
            paralar[i].rectTransform.position = para.rectTransform.position;
        }

        sagsol = 1;
        yan_carpma = 1;
        donme_ust = 90;
        dur = false;
        sola_donus = 0;
        kup=GameObject.Find("gorunmez_kup");  //sonra sil
        dur_kup=GameObject.Find("dur_kup");//sonra sil
        rotation_vector = car.eulerAngles.y + sola_donus * sagsol;
        transform.position = car.transform.position + new Vector3(0, 5, 0);
    }

    private void FixedUpdate()
        {
        verial = car.transform.GetComponent<CarController>().suya_dokundu;
        arabapatladi = car.transform.GetComponent<CarController>().fail;
        local_velocity = car.InverseTransformDirection(car.GetComponent<Rigidbody>().velocity);
        if (verial)
        {

            takip = car.transform.GetComponent<CarController>().sicratma.transform;
            height = 6;
        }else if (arabapatladi)
        {
            if (distance < 6) {
                distance += 0.02f;
                height += 0.01f;
            }
        }
        else
        {
            takip = car;







            /////////////////////////////////////YAN IŞIN ////////////////////////////////

            Vector3 yan_yon = transform.right * (-1); 
            
            if (Physics.Raycast(transform.position, yan_yon, out saghit, yan_isin_uzunluk))
                {

                sagsol = -1;
                
                saga_degme_var = true;

                /* if (sola_donus<20)sola_donus += 0.5f;
*/
               // yan_carpma = Vector3.Distance(saghit.point,transform.position);


                //if (yan_carpma<3.5f) transform.position+= transform.right * 0.01f;



                }
                else if(Physics.Raycast(transform.position, yan_yon*(-1) , out solhit, yan_isin_uzunluk))
                {


                /* if (sola_donus<20)sola_donus += 0.5f;
                
 */              

               // yan_carpma = Vector3.Distance(solhit.point,transform.position);

                //if (yan_carpma<2.5f)transform.position+= transform.right * 0.01f* (-1);








                sagsol = 1;
                
                sola_degme_var= true;
                  

                }
                else
                    {
                saga_degme_var = false;
                sola_degme_var = false;

                      }
                Debug.DrawRay(transform.position, yan_yon * yan_isin_uzunluk, Color.green);
                Debug.DrawRay(transform.position, yan_yon * (-1) * yan_isin_uzunluk, Color.green);

      



              //////////////////////////////////////ARKA IŞIN///////////////////////////////  

            if (Physics.Raycast(transform.position, transform.forward * (-1), out arkahit, arka_isin_uzunluk))
            {
                carpma_mesafesi= Vector3.Distance(arkahit.point, transform.position);


                arka_degme = true;

            }
            else
            {
                arka_degme = false;
            }
            Debug.DrawRay(transform.position, transform.forward * (-1) * arka_isin_uzunluk, Color.green);

            ////////////////////////////////////////////////////////////////////////////////////

            if (arka_degme || sola_degme_var || saga_degme_var)
            {
               /* if ((gidis_hiz > 19 && local_velocity.z > 0))
                {
                dur = false;
                }
                else
                {*/
                    dur = true;
               // }

            }
            else
            {
                dur = false;
            }



            fark = car.eulerAngles.y - transform.eulerAngles.y;

            if (fark < 0)
            {
                fark = fark + 360;
            }



            if (fark > 180)
            {
                fark = 360 - fark;
            }







            gidis_hiz = car.transform.GetComponent<CarController>().CurrentSpeed;
            hiz_oranı = ((gidis_hiz + 2) / car.transform.GetComponent<CarController>().MaxSpeed);
            mesafe = Vector3.Distance(takip.transform.position, transform.position);

            yaklasma = 10 * hiz_oranı * ((distance) / mesafe);
            if (dur)
            {

                if (mesafe > distance)
                {


                    konuma_gel(transform, takip.transform.position + new Vector3(0, height, 0), Time.deltaTime * yaklasma / 3);



                }
                sola_donus = fark;
                mydistance = mesafe;


            }
            else
            {

                konuma_gel(transform, kup.transform.position, Time.deltaTime * yaklasma);


            }

            /////////////////////////////////////////////////////////////////////////////////////////////
            if (local_velocity.z < -0.5f && dur == false)
            {


                if (sola_donus < donme_ust)
                {

                    sola_donus += donus_hiz;
                }
                else
                {
                    sola_donus = donme_ust;
                }



            } else if (local_velocity.z > 0 && dur == false)
            {


                if (sola_donus>0.5)
                {
              sola_donus -= donus_hiz;
                }else{
                
                    sola_donus = 0;
                }


            }
            else if(dur==false)
            {
                sola_donus = 0;
            }



/////////////////////////////////////////////////////////////////////////////////////////







  



        }

 if (sola_donus < 30) { 
        if (sola_degme_var)
        {
            sola_donus += 0.5f;
        }

        if (saga_degme_var)
        {
            sola_donus += 0.5f;
        }

        

}






 rotation_vector = car.eulerAngles.y+sola_donus * sagsol;

            transform.LookAt(takip);
            transform.Rotate(ileriye_alma, 0, 0);




        Debug.Log(sola_donus + "               " + fark);




        /*

        float aci = car.eulerAngles.y + fark * sagsol;
        Quaternion dur_don = Quaternion.Euler(0, aci, 0);
        dur_kup.transform.position = takip.transform.position;
        dur_kup.transform.position -= dur_don * Vector3.forward * mesafe;

        Vector3 yeni_temp = dur_kup.transform.position;
        yeni_temp.y = takip.transform.position.y + height;
        dur_kup.transform.position = yeni_temp;
        dur_kup.transform.LookAt(takip);


*/


        /*

     
        /////////////////////BURASI ANİ DUVAR ARKASINA GEÇME DURUMLARI İÇİN//////////////////

            float wantedangle = rotation_vector;
            float wantedheight = takip.position.y + height;
            float myangle = transform.eulerAngles.y;
            float myheight = transform.position.y;
            float mydistance = distance;

            myangle = Mathf.LerpAngle(myangle, wantedangle, rotationdamping * Time.deltaTime);
            myheight = Mathf.LerpAngle(myheight, wantedheight, heightdamping * Time.deltaTime);
            mydistance = Mathf.LerpAngle(mydistance, distance, heightdamping * Time.deltaTime);
            Quaternion currentrotation = Quaternion.Euler(0, myangle, 0);
      
            kup.transform.position = takip.position;
            kup.transform.position -= currentrotation * Vector3.forward *distance;
       
             Vector3 temp = transform.position;
            temp.y = myheight;
            transform.position = temp;       
        if (dur)
        {

            yaklasma = 5;
        }
        else
        {
            if (yaklasma < 20) yaklasma += 0.01f;
           
            konuma_gel(transform, kup.transform.position, Time.deltaTime *yaklasma);        
        }













         transform.LookAt(takip);
        transform.Rotate(ileriye_alma, 0, 0);





        Debug.Log(wantedangle + "               " + sola_donus);










        ///////////////////////////////////////////////////////////////////////////////

*/

        if (para_goster)
        {
        Vector2 screenxy = Camera.main.WorldToScreenPoint(kaybolan);




        paralar[sira_para].gameObject.SetActive(true);
        paralar[sira_para].rectTransform.position = screenxy;
        sira_para++;
        ilk_para++;
        if (sira_para == para_sayisi - 1) sira_para = 0;
        if (ilk_para == para_sayisi - 1) ilk_para = 0;
        if (ilk_para >= 0) paralar[ilk_para].gameObject.SetActive(false);
            para_goster = false;
        }













        if (Time.time > coin_time)
        {
            coin_time = Time.time + coin_bekle;



            for (int i = 0; i < para_sayisi; i++)
            {
                if (paralar[i].isActiveAndEnabled == true)
                {

                    paralar[i].sprite = coinler[coin_sira];
                    coinx = paralar[i].rectTransform.position.x;
                    coiny = paralar[i].rectTransform.position.y;

                    coinx = Mathf.Lerp(coinx, skortxt.rectTransform.position.x, 0.05f);
                    coiny = Mathf.Lerp(coiny, skortxt.rectTransform.position.y, 0.05f);
                    paralar[i].rectTransform.position = new Vector2(coinx, coiny);



                    if (Vector2.Distance(paralar[i].rectTransform.position,skortxt.rectTransform.position)<1)
                    {
                        paralar[i].gameObject.SetActive(false);
                    }
                }




            }
            coin_sira++;
            if (coin_sira == coinler.Length) coin_sira = 0;


        }

    }

   

    private void LateUpdate()
        {


            float wantedangle = rotation_vector;
            float wantedheight = takip.position.y + height;
            float myangle = kup.transform.eulerAngles.y;
            float myheight = kup.transform.position.y;
             mydistance = distance;

            myangle = Mathf.LerpAngle(myangle, wantedangle, rotationdamping * Time.deltaTime);
            myheight = Mathf.LerpAngle(myheight, wantedheight, heightdamping * Time.deltaTime);
            mydistance = Mathf.LerpAngle(mydistance, distance, heightdamping * Time.deltaTime);


            Quaternion currentrotation = Quaternion.Euler(0, myangle, 0);
            kup.transform.position = takip.position; //new Vector3(car.transform.position.x, car.transform.position.y, car.transform.position.z+ileriye_alma);
            kup.transform.position -= currentrotation * Vector3.forward * mydistance;

            
            Vector3 temp = kup.transform.position;
            temp.y = myheight;
            kup.transform.position = temp;
            kup.transform.LookAt(takip);
            //transform.Rotate(ileriye_alma, 0, 0);





        

    }



    public void ileri_down()
    {

        car.transform.GetComponent<CarUserControl>().ileriye_git = true;

    }
    public void ileri_up()
    {

        car.transform.GetComponent<CarUserControl>().ileriye_git = false;

    }
    public void geri_down()
    {

        car.transform.GetComponent<CarUserControl>().geriye_git = true;

    }
    public void geri_up()
    {

        car.transform.GetComponent<CarUserControl>().geriye_git = false;

    }
    public void saga_down()
    {

        car.transform.GetComponent<CarUserControl>().saga_don = true;

    }
    public void saga_up()
    {

        car.transform.GetComponent<CarUserControl>().saga_don = false;

    }
    public void sola_down()
    {

        car.transform.GetComponent<CarUserControl>().sola_don = true;

    }
    public void sola_up()
    {

        car.transform.GetComponent<CarUserControl>().sola_don = false;

    }

    public void ates_down()
    {

        car.transform.GetComponent<CarUserControl>().ates = true;

    }
    public void ates_up()
    {

         car.transform.GetComponent<CarUserControl>().ates= false;

    }

    private void konuma_gel(Transform konum_1,Vector3 konum_2, float hiz)
    {
        float x = konum_1.position.x;
        float y = konum_1.position.y;
        float z = konum_1.position.z; 

         x = Mathf.Lerp(x, konum_2.x, hiz);
         y = Mathf.Lerp(y, konum_2.y, hiz);
         z = Mathf.Lerp(z, konum_2.z, hiz);



        konum_1.position = new Vector3(x, y, z);

    }


}

