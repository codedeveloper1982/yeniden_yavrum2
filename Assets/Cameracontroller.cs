using System.Collections;
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
    private Vector3 yeni_konum, local_velocity,hedef_konum,coin_ucu,arkayon;
    // public Transform on_kısım;


    [Header("COİN AYARLARI")]
    /// ///////////////// BURASI COİNLERLE İLGİLİ BÖLME////////////////
    public Sprite[] coinler;
    public Image para;
    private Image[] paralar;
    private float coin_time, coin_bekle = 0.01f, coinx, coiny, coinx_scale, coiny_scale;
    private int coin_sira;
    public GameObject canvas;
    private int para_sayisi = 6, ilk_para, sira_para;
    public Text skortxt;
    public Image board_para;
    public Vector3 kaybolan,kamera_yon;
    public bool para_goster = false,don=false;
    private float yan_carpma,yan_isin_uzunluk,arka_isin_uzunluk,carpma_mesafesi,donme_ust,yaklasma,fark,mydistance,kamera_uzunluk=1;
    private GameObject kup;
   






    #region BURASI ARABANIN CANI İÇİN
    
    
    public GameObject can_gosterge;
    public float player_cani;
    private Transform dusman;



    #endregion  







    private void Start()
    {
        dusman= GameObject.FindGameObjectWithTag("düşman kontrol").transform;
        car = GameObject.FindGameObjectWithTag("Player").transform;
        donus_hiz = 0.9f;
        yan_isin_uzunluk = 1.2f;
        arka_isin_uzunluk = 2.0f;


        /// ///////////////// BURASI COİNLERLE İLGİLİ BÖLME////////////////
        for (int i = 0; i < 16; i++)
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
        kup = Resources.Load<GameObject>("kup(camera için)");
        kup = Instantiate(kup, transform.position, Quaternion.identity);

       // rotation_vector = car.eulerAngles.y + sola_donus * sagsol;
       // transform.position = car.transform.position + new Vector3(0, 5, 0);
        yaklasma = 8;
    }

    private void FixedUpdate()
        {
        player_cani = dusman.GetComponent<Kontrol_denemesi>().playercani;
        Debug.Log("player can:" + player_cani);
        if(player_cani>=0)can_gosterge.GetComponent<Image>().fillAmount = (player_cani / 100);
        



        verial = car.transform.GetComponent<CarController>().suya_dokundu;
        arabapatladi = car.transform.GetComponent<CarController>().fail;
        local_velocity = car.InverseTransformDirection(car.GetComponent<Rigidbody>().velocity);
        if (verial)
        {

           // takip = car.transform.GetComponent<CarController>().sicratma.transform;
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

            Vector3 yan_yon = takip.transform.right * (-1); 
            
            if (Physics.Raycast(takip.transform.position, yan_yon, out saghit, yan_isin_uzunluk))
                {

                sagsol = -1;
                
                saga_degme_var = true;

                if (sola_donus < 45) sola_donus +=donus_hiz*2 ;



                }
                else if(Physics.Raycast(takip.transform.position, yan_yon*(-1) , out solhit, yan_isin_uzunluk))
                {


                sagsol = 1;
                
                sola_degme_var= true;
                if (sola_donus < 45) sola_donus += donus_hiz*2;                

                }
                else
                    {
                saga_degme_var = false;
                sola_degme_var = false;

                      }
                Debug.DrawRay(takip.transform.position, yan_yon * yan_isin_uzunluk, Color.green);
                Debug.DrawRay(takip.transform.position, yan_yon * (-1) * yan_isin_uzunluk, Color.green);



            /*

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
                dur = true;
            }
            else
            {
                dur = false;
            }

*/

            if (local_velocity.z < -0.5f)
            {


                if (sola_donus < donme_ust)
                {


                    sola_donus += donus_hiz;
                }
                else
                {
                    sola_donus = donme_ust;
                }



            }
            else if (local_velocity.z > 0 && !(sola_degme_var || saga_degme_var))
            {


                if (sola_donus > 0.5)
                {
                    sola_donus -= donus_hiz;
                }
                else
                {

                    sola_donus = 0;
                }


            }


















        }
        if (dur)
        {
            sola_donus = transform.eulerAngles.y - takip.transform.eulerAngles.y;

            if (sola_donus < 0) sola_donus = sola_donus + 360;
            if (sola_donus > 180) sola_donus = 360 - sola_donus;



        }



        rotation_vector = car.eulerAngles.y+sola_donus * sagsol;


        /////////////////////////////////////KAMERA IŞINLARI

        // kamera_yon = takip.transform.right * (-1);
        dur = false;
        RaycastHit kamera_hit;

        for (int i = 0; i < 3; i++)
        {
            float angle = i * Mathf.PI / 2;

            kamera_yon = kup.transform.right * Mathf.Cos(angle) + kup.transform.forward * Mathf.Sin(angle)*(-1);

            if (Physics.Raycast(kup.transform.position, kamera_yon, out kamera_hit, kamera_uzunluk))
        {
                
                dur = true;
                break;



        }
            Debug.DrawRay(kup.transform.position, kamera_yon * kamera_uzunluk, Color.green);
        }


        coin_ucu = takip.transform.position;
     arkayon = kup.transform.position-takip.transform.position;
        mesafe = Vector3.Distance(kup.transform.position, takip.transform.position);

        arkayon = arkayon.normalized;
    RaycastHit arka_hit;
            if (Physics.Raycast(takip.transform.position, arkayon, out arka_hit, mesafe))
            {

            dur = true;
               

        }
Debug.DrawRay(coin_ucu, arkayon * mesafe, Color.green);





        if (local_velocity.z > 4.5f)
        {
            dur = false;

        }





        if (dur)
        {

        }
        else
        {
        konuma_gel(transform, kup.transform.position, yaklasma*Time.deltaTime);

        }













        ///////////////////////////////////////////////////////































        transform.LookAt(takip);
        transform.Rotate(ileriye_alma, 0, 0);



        //////////////////////////////////////deneme
        ///


        float wantedangle = rotation_vector;
        float wantedheight = takip.position.y + height;
        float myangle = kup.transform.eulerAngles.y;
        float myheight = kup.transform.position.y;

        myangle = Mathf.LerpAngle(myangle, wantedangle, rotationdamping * Time.deltaTime);
        myheight = Mathf.LerpAngle(myheight, wantedheight, heightdamping * Time.deltaTime);
        mydistance = Mathf.LerpAngle(mydistance, distance, heightdamping * Time.deltaTime);


        Quaternion currentrotation = Quaternion.Euler(0, myangle, 0);
        kup.transform.position = takip.position; //new Vector3(car.transform.position.x, car.transform.position.y, car.transform.position.z+ileriye_alma);
        kup.transform.position = takip.transform.position - currentrotation * Vector3.forward * mydistance;


        Vector3 temp = kup.transform.position;
        temp.y = myheight;
        kup.transform.position = temp;

        kup.transform.LookAt(takip);



        //////////////////////////////////////////


























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
                    ///////////////// tabelaya gidişlerini eşitle
                    paralar[i].sprite = coinler[coin_sira];
                    coinx = paralar[i].rectTransform.position.x;
                    coiny = paralar[i].rectTransform.position.y;

                    coinx = Mathf.Lerp(coinx, board_para.rectTransform.position.x, 0.1f);
                    coiny = Mathf.Lerp(coiny, board_para.rectTransform.position.y, 0.1f);
                    paralar[i].rectTransform.position = new Vector2(coinx, coiny);

                    ///////////////// tabelaya boyutlarını eşitle
                    coinx_scale = paralar[i].rectTransform.rect.width;
                    coiny_scale = paralar[i].rectTransform.rect.height;

                    coinx_scale = Mathf.Lerp(coinx_scale, board_para.rectTransform.rect.width, 0.05f);
                    coiny_scale = Mathf.Lerp(coiny_scale, board_para.rectTransform.rect.height, 0.05f);
                    paralar[i].rectTransform.sizeDelta = new Vector2(coinx_scale, coiny_scale);

                    paralar[i].rectTransform.localScale = new Vector3(1, 1, 1);



                    if (Vector2.Distance(paralar[i].rectTransform.position, board_para.rectTransform.position) < 1)
                    {
                        paralar[i].gameObject.SetActive(false);
                    }
                }




            }
            coin_sira++;
            if (coin_sira == coinler.Length) coin_sira = 0;


        }

    }

   
    /*
    private void LateUpdate()
        {










    }

*/

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

