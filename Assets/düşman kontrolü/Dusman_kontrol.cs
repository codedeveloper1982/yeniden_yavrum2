using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;
using UnityStandardAssets.Vehicles.Car;



//namespace SplineMesh { 
public class Dusman_kontrol : MonoBehaviour
{
        public Spline[] cizgiler;
        public float konumu,arka_konum,orta_konum,aralik=10.0f;
        public int mevcut_cizginin_sirasi,arka_cizginin_sirasi,orta_cizginin_sirasi;
        private Spline mevcut_cizgi,arka_cizgi,orta_cizgi;
        private GameObject player;
        private bool ileri_git=false,geri_git=false;
        public GameObject arka_kisim,orta_kisim;
        public GameObject[] dusman;
        [SerializeField]
        private float hiz,sure;
        private float x, y, z;
    private float[] konumlar;
    private float[] hedef_konumlar;
    public GameObject[] objeler;
    private CurveSample[] egriler;
    private Spline[] kontrol_cizgileri;
    private Vector3[] hedef;
    public GameObject cube,diger_cube;
    private float gecici_konum = 0, gecici_konum_iki=0;
    //private int kay=0, onekay= 0, dikey_takip=0, yatay_takip=0;
    //private string isim;



    // Start is called before the first frame update
    void Start()
    {
            player = GameObject.FindGameObjectWithTag("Player");
            mevcut_cizginin_sirasi =arka_cizginin_sirasi=orta_cizginin_sirasi= 0;
            arka_konum = 5;
            konumu = arka_konum + aralik;
            orta_konum = konumu - 20;
            mevcut_cizgi = cizgiler[mevcut_cizginin_sirasi];
            orta_cizgi= cizgiler[orta_cizginin_sirasi];
            arka_cizgi = cizgiler[arka_cizginin_sirasi];

            CurveSample sample = mevcut_cizgi.GetSampleAtDistance(konumu);
            transform.position = mevcut_cizgi.transform.position;
            transform.position += mevcut_cizgi.transform.forward * sample.location.z;
            transform.position += mevcut_cizgi.transform.right * sample.location.x;
            transform.position += mevcut_cizgi.transform.up * sample.location.y;
            transform.rotation = sample.Rotation;
            transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x,
                mevcut_cizgi.transform.rotation.eulerAngles.y + transform.rotation.eulerAngles.y,
                transform.rotation.eulerAngles.z);


            CurveSample arka_sample= mevcut_cizgi.GetSampleAtDistance(arka_konum);
           arka_kisim.transform.position = mevcut_cizgi.transform.position;
            arka_kisim.transform.position += mevcut_cizgi.transform.forward * arka_sample.location.z;
            arka_kisim.transform.position += mevcut_cizgi.transform.right * arka_sample.location.x;
            arka_kisim.transform.position += mevcut_cizgi.transform.up * arka_sample.location.y;
            arka_kisim.transform.rotation = arka_sample.Rotation;
            arka_kisim.transform.eulerAngles = new Vector3(arka_kisim.transform.rotation.eulerAngles.x,
                mevcut_cizgi.transform.rotation.eulerAngles.y + arka_kisim.transform.rotation.eulerAngles.y,
                arka_kisim.transform.rotation.eulerAngles.z);

        konumlar = new float[objeler.Length];
        hedef_konumlar=new float[objeler.Length];
        egriler = new CurveSample[objeler.Length];
        hedef =new Vector3[objeler.Length];
        
        kontrol_cizgileri = new Spline[objeler.Length];

        for(int i = 0; i < objeler.Length; i++)
        {
            kontrol_cizgileri[i]=cizgiler[mevcut_cizginin_sirasi];
            konumlar[i] =hedef_konumlar[i]= konumu - 3 * (i + 1);
        }
    }

    // Update is called once per frame
    void Update()
    {


            for(int i = 0; i < 3; i++)
            {
                if (Vector3.Distance(transform.GetChild(i).transform.position, player.transform.position) < 15.0f)
                {
                    ileri_git = true;
                }
                if (Vector3.Distance(arka_kisim.transform.GetChild(i).transform.position, player.transform.position) < 5.0f)
                {
                    geri_git = true;
                }

            }

          /* if (konumu - arka_konum <aralik )
            {
                if (arka_konum + aralik < arka_cizgi.Length-1)
                {
                    konumu = arka_konum + aralik;

                }


            }*/ 
        
        x = Mathf.Lerp(dusman[2].transform.position.x,objeler[0].transform.position.x, Time.deltaTime * hiz);
        y = Mathf.Lerp(dusman[2].transform.position.y,objeler[0].transform.position.y, Time.deltaTime * hiz);
        z = Mathf.Lerp(dusman[2].transform.position.z,objeler[0].transform.position.z, Time.deltaTime * hiz);

        dusman[2].transform.position = new Vector3(x, y, z);
        dusman[2].transform.LookAt(player.transform);

      
            if (ileri_git)
            {
                konumu += 0.2f;
                arka_konum += 0.2f;
                orta_konum += 0.2f;
                if (konumu > mevcut_cizgi.Length) konumu = mevcut_cizgi.Length;
              //  if (arka_konum > arka_cizgi.Length) arka_konum = arka_cizgi.Length;
              //  if (orta_konum > mevcut_cizgi.Length) orta_konum = orta_cizgi.Length;

              CurveSample sample = mevcut_cizgi.GetSampleAtDistance(konumu);
            transform.position = mevcut_cizgi.transform.position;
            transform.position += mevcut_cizgi.transform.forward * sample.location.z;
            transform.position += mevcut_cizgi.transform.right * sample.location.x;
            transform.position += mevcut_cizgi.transform.up * sample.location.y;
            transform.rotation = sample.Rotation;
            transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, 
                mevcut_cizgi.transform.rotation.eulerAngles.y + transform.rotation.eulerAngles.y, 
                transform.rotation.eulerAngles.z);

  


                ileri_git = false;

                CurveSample orta_sample = orta_cizgi.GetSampleAtDistance(orta_konum);
                orta_kisim.transform.position = orta_cizgi.transform.position;
            orta_kisim.transform.position += orta_cizgi.transform.forward * orta_sample.location.z;
            orta_kisim.transform.position += orta_cizgi.transform.right * orta_sample.location.x;
            orta_kisim.transform.position += orta_cizgi.transform.up * orta_sample.location.y;
            orta_kisim.transform.rotation = orta_sample.Rotation;
            orta_kisim.transform.eulerAngles = new Vector3(orta_kisim.transform.rotation.eulerAngles.x,
                    orta_cizgi.transform.rotation.eulerAngles.y + orta_kisim.transform.rotation.eulerAngles.y,
                    orta_kisim.transform.rotation.eulerAngles.z);


                CurveSample arka_sample = arka_cizgi.GetSampleAtDistance(arka_konum);
                arka_kisim.transform.position = arka_cizgi.transform.position;
                arka_kisim.transform.position += arka_cizgi.transform.forward * arka_sample.location.z;
                arka_kisim.transform.position += arka_cizgi.transform.right * arka_sample.location.x;
                arka_kisim.transform.position += arka_cizgi.transform.up * arka_sample.location.y;
                arka_kisim.transform.rotation = arka_sample.Rotation;
                arka_kisim.transform.eulerAngles = new Vector3(arka_kisim.transform.rotation.eulerAngles.x,
                    arka_cizgi.transform.rotation.eulerAngles.y + arka_kisim.transform.rotation.eulerAngles.y,
                    arka_kisim.transform.rotation.eulerAngles.z);

        }
            if(geri_git)
            {
                konumu -= 0.2f;
                arka_konum -= 0.2f;

              CurveSample sample = mevcut_cizgi.GetSampleAtDistance(konumu);
            transform.position = mevcut_cizgi.transform.position;
            transform.position += mevcut_cizgi.transform.forward * sample.location.z;
            transform.position += mevcut_cizgi.transform.right * sample.location.x;
            transform.position += mevcut_cizgi.transform.up * sample.location.y;
            transform.rotation = sample.Rotation;
            transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, 
                mevcut_cizgi.transform.rotation.eulerAngles.y + transform.rotation.eulerAngles.y, 
                transform.rotation.eulerAngles.z);

         /*   for (int i = 0; i < kontrol_cizgileri.Length; i++)
            {
                konumlar[i] = konumu - 3 * (i + 1);
                if (konumlar[i] < 1)
                {
                    kontrol_cizgileri[i] = arka_cizgi;
                    konumlar[i] = kontrol_cizgileri[i].Length - (1-konumlar[i]);
                } else { 
                    kontrol_cizgileri[i] = mevcut_cizgi;
                }

                egriler[i] = kontrol_cizgileri[i].GetSampleAtDistance(konumlar[i]);
                objeler[i].transform.position = kontrol_cizgileri[i].transform.position;
                objeler[i].transform.position += kontrol_cizgileri[i].transform.forward * egriler[i].location.z;
                objeler[i].transform.position += kontrol_cizgileri[i].transform.right * egriler[i].location.x;
                objeler[i].transform.position += kontrol_cizgileri[i].transform.up * egriler[i].location.y;
                objeler[i].transform.rotation = egriler[i].Rotation;
                objeler[i].transform.eulerAngles = new Vector3(objeler[i].transform.rotation.eulerAngles.x,
                    kontrol_cizgileri[i].transform.rotation.eulerAngles.y + objeler[i].transform.rotation.eulerAngles.y,
                    objeler[i].transform.rotation.eulerAngles.z);
            }*/


            geri_git = false;
           
                        CurveSample arka_sample= arka_cizgi.GetSampleAtDistance(arka_konum);
            arka_kisim.transform.position = arka_cizgi.transform.position;
            arka_kisim.transform.position += arka_cizgi.transform.forward * arka_sample.location.z;
            arka_kisim.transform.position += arka_cizgi.transform.right * arka_sample.location.x;
            arka_kisim.transform.position += arka_cizgi.transform.up * arka_sample.location.y;
            arka_kisim.transform.rotation = arka_sample.Rotation;
            arka_kisim.transform.eulerAngles = new Vector3(arka_kisim.transform.rotation.eulerAngles.x,
                arka_cizgi.transform.rotation.eulerAngles.y + arka_kisim.transform.rotation.eulerAngles.y,
                arka_kisim.transform.rotation.eulerAngles.z);
             }
            ////////////ön düþman sensörü////////////////////
            float fark = (mevcut_cizgi.Length) - konumu;
            if (fark < 1.0f)
            {
                mevcut_cizginin_sirasi++;
                if (mevcut_cizginin_sirasi == cizgiler.Length) mevcut_cizginin_sirasi = 0;
                mevcut_cizgi = cizgiler[mevcut_cizginin_sirasi];
                konumu = 1;

            }
            if(konumu< 1.0f)
            {
                mevcut_cizginin_sirasi--;
                if (mevcut_cizginin_sirasi < 0)
                {
                    mevcut_cizginin_sirasi = cizgiler.Length - 1;

                }
                mevcut_cizgi = cizgiler[mevcut_cizginin_sirasi];
                konumu = mevcut_cizgi.Length-1;
            }
        ////////////ön düþman sensörü////////////////////
        ///
        /// 
        /// 
        /// 
        /// /////////orta düþman sensörü////////////////////
        fark = (orta_cizgi.Length) - orta_konum;
        if (fark < 1.0f)
        {
            orta_cizginin_sirasi++;
            if (orta_cizginin_sirasi == cizgiler.Length) orta_cizginin_sirasi = 0;
            orta_cizgi = cizgiler[mevcut_cizginin_sirasi];
            orta_konum = 1;

        }
        if (orta_konum< 1.0f)
        {
            orta_cizginin_sirasi--;
            if (orta_cizginin_sirasi < 0)
            {
                orta_cizginin_sirasi = cizgiler.Length - 1;

            }
            orta_cizgi = cizgiler[mevcut_cizginin_sirasi];
            orta_konum = orta_cizgi.Length - 1;
        }

        ///  /////////orta düþman sensörü////////////////////
        /// 
        ////////////arka düþman sensörü///////////////////
        fark = arka_cizgi.Length - arka_konum;
            if (fark < 1.0f)
            {
                arka_cizginin_sirasi++;
                if (arka_cizginin_sirasi==cizgiler.Length)
                {
                    arka_cizginin_sirasi = 0;

                }
                arka_cizgi = cizgiler[arka_cizginin_sirasi];
                arka_konum = konumu-aralik;

            }
            if(arka_konum< 1.0f)
            {
                arka_cizginin_sirasi--;
                if (arka_cizginin_sirasi < 0)
                {
                    arka_cizginin_sirasi = cizgiler.Length - 1;

                }
                arka_cizgi = cizgiler[arka_cizginin_sirasi];
            arka_konum =konumu+arka_cizgi.Length -aralik;
            
            if (arka_konum > arka_cizgi.Length) arka_konum = arka_cizgi.Length-0.4f;
        }
           ////////////arka düþman sensörü///////////////////
           ///


            //////////// düþman vurulmasý///////////////////
        if (player.GetComponent<CarController>().vuruldu == true)
        {
             string isim=player.GetComponent<CarController>().vurulan;
           // RaycastHit carp = player.GetComponent<CarController>().hit;


                for(int i = 0; i < dusman.Length; i++) { 

            if (dusman[i]!=null &&  dusman[i].tag==isim)
            {

                Debug.Log("düþman vuruldu");

            }
            }




            player.GetComponent<CarController>().vuruldu = false;

        }
            //////////// düþman vurulmasý///////////////////
            ///
            /// 
            /// 
            //////////// kontrol noktalarý///////////////////
        for (int i = 0; i < kontrol_cizgileri.Length; i++)
        { 


            if (Mathf.Abs(konumlar[i] - hedef_konumlar[i])<0.5f)
            {
                hedef_konumlar[i] = Random.Range(0, 15);
            }
            gecici_konum = konumu - konumlar[i];
             gecici_konum_iki=konumu - hedef_konumlar[i];
            
            if (konumlar[i] < hedef_konumlar[i]) konumlar[i] += 0.05f;
            if (konumlar[i] > hedef_konumlar[i]) konumlar[i] -= 0.05f;


            if (gecici_konum < 0.5f)
            {
                kontrol_cizgileri[i] = orta_cizgi;
                gecici_konum = kontrol_cizgileri[i].Length - (1 - gecici_konum);
                if (gecici_konum > kontrol_cizgileri[i].Length) gecici_konum = kontrol_cizgileri[i].Length - 0.5f;
            }
            else
            {
                kontrol_cizgileri[i] = mevcut_cizgi;
                gecici_konum =konumu- konumlar[i];
            }
            
            egriler[i] = kontrol_cizgileri[i].GetSampleAtDistance(gecici_konum);
            objeler[i].transform.position = kontrol_cizgileri[i].transform.position;
            objeler[i].transform.position += kontrol_cizgileri[i].transform.forward * egriler[i].location.z;
            objeler[i].transform.position += kontrol_cizgileri[i].transform.right * egriler[i].location.x;
            objeler[i].transform.position += kontrol_cizgileri[i].transform.up * egriler[i].location.y + new Vector3(0, 2, 0);
            objeler[i].transform.rotation = egriler[i].Rotation;
            objeler[i].transform.eulerAngles = new Vector3(objeler[i].transform.rotation.eulerAngles.x,
                kontrol_cizgileri[i].transform.rotation.eulerAngles.y + objeler[i].transform.rotation.eulerAngles.y + 2,
                objeler[i].transform.rotation.eulerAngles.z);

            if (gecici_konum_iki < 0.5f)
            {
                kontrol_cizgileri[i] = orta_cizgi;
                gecici_konum_iki = kontrol_cizgileri[i].Length-(1-gecici_konum_iki);
                if (gecici_konum > kontrol_cizgileri[i].Length) gecici_konum = kontrol_cizgileri[i].Length -0.5f;
            }
            else
            {
                kontrol_cizgileri[i] = mevcut_cizgi;
                gecici_konum_iki = konumu - hedef_konumlar[i];
            }
            Debug.Log(gecici_konum_iki);
            egriler[i]=kontrol_cizgileri[i].GetSampleAtDistance(gecici_konum_iki);
            hedef[i] = kontrol_cizgileri[i].transform.position;
            hedef[i] += kontrol_cizgileri[i].transform.forward* egriler[i].location.z;
            hedef[i] += kontrol_cizgileri[i].transform.right* egriler[i].location.x;
            hedef[i] += kontrol_cizgileri[i].transform.up* egriler[i].location.y+new Vector3(0, 2, 0);
            cube.transform.position = hedef[i];





        }



        /*if (Time.time > sure)
        {
            sure = Time.time + 1.5f;
  
            if(dikey_takip==onekay && yatay_takip == kay) {     
            onekay =Mathf.RoundToInt(Random.Range(0, 5));
            kay =Mathf.RoundToInt(Random.Range(-2, 3));

            }
            if (dikey_takip < onekay) dikey_takip++;
            if (dikey_takip > onekay) dikey_takip--;
            if (yatay_takip < kay) yatay_takip++;
            if (yatay_takip > kay) yatay_takip--;

        }
            Kaydir(cube,mevcut_cizgi, egriler[onekay], kay);
            Kaydir(diger_cube, mevcut_cizgi, egriler[dikey_takip], yatay_takip);*/
    }

    /*public void Kaydir(GameObject nesne,Spline cizgim,CurveSample ornek,int kayma)
    {
        nesne.transform.position = cizgim.transform.position;
        nesne.transform.position += cizgim.transform.forward * ornek.location.z;
        nesne.transform.position += cizgim.transform.right * ornek.location.x;
        nesne.transform.position += cizgim.transform.up * ornek.location.y;
        nesne.transform.rotation = ornek.Rotation;
        nesne.transform.position += nesne.transform.right * kayma*2;

    }*/
/*
    private void Dikey_kontrol_noktalari(GameObject obje,float rate,float ara,CurveSample egri)
    {



            rate = konumu - ara;

           if (rate < 1)
            {
                ustunde = arka_cizgi;
                rate = arka_konum + (aralik - ara);
                if (rate > ustunde.Length) rate = ustunde.Length - 1;
            }
            else
            {
            ustunde = mevcut_cizgi;
            }*/
/* 
            egri = ustunde.GetSampleAtDistance(rate);
            transform.position = ustunde.transform.position;
            obje.transform.position += ustunde.transform.forward * egri.location.z;
            obje.transform.position += ustunde.transform.right * egri.location.x;
            obje.transform.position += ustunde.transform.up * egri.location.y;
            obje.transform.rotation = egri.Rotation;
            obje.transform.eulerAngles = new Vector3(obje.transform.rotation.eulerAngles.x,
                ustunde.transform.rotation.eulerAngles.y + obje.transform.rotation.eulerAngles.y,
                obje.transform.rotation.eulerAngles.z);

        
    }*/



}
//}
