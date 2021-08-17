using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;

public class Kontrol_denemesi : MonoBehaviour
{
    /// <summary>
    /// /////////////////////burasý on ve arkakontrol noktalarý için//////////////////////
    /// </summary>
    public Spline[] cizgiler;
    private float[] konum_sirasi;
    private int[] cizgi_sirasi;
    private Spline[] mevcut_cizgiler;
    private CurveSample[] mevcut_egriler;
    public float kirp = 0.3f;
    public GameObject[] cube;
    public float on, arka;
    public bool ileri = false, geri = false;
    public float otelenme = 0.2f;
    /// <summary>
    /// //////////////burasý on ve arkakontrol noktalarý için///////////////////
    /// </summary>

    //kontrol noktalarý///////////////////////
    private float[] hedef_sirasi;
    private int[] hedef_cizgi_sirasi;
    private Spline[] hedef_cizgi;
    private CurveSample[] hedef_egri;
    private Vector3[] hedef_konum;
    public GameObject gorunur_obje;
    private float [] gecici_konum;
    public float sure=2,oynatma_vakti;



    ///////drone noktalarý///////////////////////
    private float[] drone_sirasi;
    private int[] drone_cizgi_sirasi;
    private Spline[] drone_cizgi;
    private CurveSample[] drone_egri;
    public GameObject [] drone_konum;
    ///////drone noktalarý///////////////////////


    ///////drone noktalarý///////////////////////
    public GameObject[] dusmanlar;
    private float[] yatay_degisim;
    private float[] sag_sol;
    public float sagsol_hiz=0.05f;
    public int maksimum_drone_sayisi = 2;

    


    private GameObject player;

    void Start()
    {
        ///////burasý on ve arkakontrol noktalarý için///////
        konum_sirasi = new float[2];
        cizgi_sirasi = new int[2];
        mevcut_cizgiler = new Spline[2];
        mevcut_egriler = new CurveSample[2];
        arka = kirp;
        konum_sirasi[0] = on;
        konum_sirasi[1] =  arka;
        for(int i = 0; i < konum_sirasi.Length; i++) { 
        cizgi_sirasi[i] = 0;
        mevcut_cizgiler[i] = cizgiler[cizgi_sirasi[i]];
        Hareket_et(cube[i],mevcut_cizgiler[i],mevcut_egriler[i],konum_sirasi[i]);
            }
        ///////burasý on ve arkakontrol noktalarý için///////
        //------------------------------------------------------------------------
        //////////hedef konum belirlenmesi için burasý //////////////
        ///

        hedef_sirasi = new float[dusmanlar.Length];
        hedef_cizgi_sirasi=new int[dusmanlar.Length];
        hedef_cizgi=new Spline[dusmanlar.Length];
        hedef_egri = new CurveSample[dusmanlar.Length];
        hedef_konum=new Vector3[dusmanlar.Length];
        gecici_konum = new float[dusmanlar.Length];
        oynatma_vakti = Time.time;



        for (int i = 0; i < dusmanlar.Length; i++)
        {
            hedef_cizgi_sirasi[i] = 0;
            hedef_cizgi[i] = cizgiler[hedef_cizgi_sirasi[i]];
        }
        /// 
        //////////hedef konum belirlenmesi için burasý //////////////

        //-------------------------------------------------------------------------
        //////////drone konum belirlenmesi için burasý //////////////       
        /////
        drone_sirasi = new float[dusmanlar.Length];
        drone_cizgi_sirasi = new int[dusmanlar.Length];
        drone_cizgi = new Spline[dusmanlar.Length];
        drone_egri = new CurveSample[dusmanlar.Length];

        for (int i = 0; i < dusmanlar.Length; i++)
        {
            drone_cizgi_sirasi[i] = 0;
            drone_cizgi[i] = cizgiler[drone_cizgi_sirasi[i]];
            drone_sirasi[i] = konum_sirasi[0];
        }
        /////
        //////////drone konum belirlenmesi için burasý //////////////  
        ///
            player = GameObject.FindGameObjectWithTag("Player");
        //////////sað sol deðiþim ////////////// 

            yatay_degisim = new  float[dusmanlar.Length];
            sag_sol =new  float[dusmanlar.Length];
        for (int i = 0; i < dusmanlar.Length; i++)
        {
            yatay_degisim[i] = sag_sol[i] = 0;
        
        }

            //////////sað sol deðiþim ////////////// 

        }

    // Update is called once per frame
    void Update()
    {


            if (Vector3.Distance(cube[0].transform.position, player.transform.position) < 15.0f)
            {
                ileri = true;
            }
            if (Vector3.Distance(cube[1].transform.position, player.transform.position) < 10.0f)
            {
                geri = true;
            }


        for (int i = 0; i < dusmanlar.Length; i++)
        {
            if (dusmanlar[i].activeSelf == true)
            { 
                Hareket_et(drone_konum[i], drone_cizgi[i], drone_egri[i], drone_sirasi[i]);//hareket kodlarý burada olsun (sensör harici olanlar)
            Drone_hareketi(dusmanlar[i], drone_konum[i], 5, 2, sag_sol[i], yatay_degisim[i]);
            if (Mathf.Abs(sag_sol[i] - yatay_degisim[i]) < 0.3f)
            {
                yatay_degisim[i] = Random.Range(-5, 6);//bunlarý sonra for döngüsünde çalýþtýr.

            }

            if (sag_sol[i] > yatay_degisim[i]) sag_sol[i] -= sagsol_hiz;//bunlarý sonra for döngüsünde çalýþtýr.
            if (sag_sol[i] < yatay_degisim[i]) sag_sol[i] += sagsol_hiz;//bunlarý sonra for döngüsünde çalýþtýr.
             }

        }

        ///////burasý on ve arkakontrol noktalarý için///////
        if (ileri) {
        for (int i = 0; i < konum_sirasi.Length; i++)
        {
            Hareket_et(cube[i], mevcut_cizgiler[i], mevcut_egriler[i], konum_sirasi[i]);
            konum_sirasi[i] += otelenme;

            if (konum_sirasi[i] > mevcut_cizgiler[i].Length - kirp)
            {
                cizgi_sirasi[i]++;
                if (cizgi_sirasi[i] == cizgiler.Length) cizgi_sirasi[i] = 0;//bu konum deðil karýþtýrma
                mevcut_cizgiler[i] = cizgiler[cizgi_sirasi[i]];
                konum_sirasi[i] =kirp;
            }
           
        }

            for (int i = 0; i < dusmanlar.Length; i++)
            {
                if (dusmanlar[i].activeSelf == true)
                {
                    drone_sirasi[i] += otelenme;
                }
            }
            ileri = false;
        }
        if(geri)
        {

        for (int i = 0; i < konum_sirasi.Length; i++)
        {
            Hareket_et(cube[i], mevcut_cizgiler[i], mevcut_egriler[i], konum_sirasi[i]);
            konum_sirasi[i] -= otelenme;

            if (konum_sirasi[i] < kirp)
            {
                cizgi_sirasi[i]--;
                if (cizgi_sirasi[i]<0) cizgi_sirasi[i] = cizgiler.Length-1;//bu konum deðil karýþtýrma
                mevcut_cizgiler[i] = cizgiler[cizgi_sirasi[i]];
                konum_sirasi[i] =mevcut_cizgiler[i].Length- kirp;
            }


        }

            for (int i = 0; i < dusmanlar.Length; i++)
            {
                if (dusmanlar[i].activeSelf == true)
                {
                    drone_sirasi[i] -= otelenme;
                }
            }
            geri = false;
        }
        ///////burasý on ve arkakontrol noktalarý için///////bitti
        ///

        for (int i = 0; i < dusmanlar.Length; i++)
        {
            if (dusmanlar[i].activeSelf == true) { 
        if (Mathf.Abs(gecici_konum[i]-drone_sirasi[i])<0.2f) { 
        hedef_sirasi [i] = Random.Range(2, 11);
                

        }



        gecici_konum[i] = konum_sirasi[0] - hedef_sirasi[i];

        if (gecici_konum[i] < kirp)
        {
            hedef_cizgi_sirasi[i] = cizgi_sirasi[0] - 1; 
            if (hedef_cizgi_sirasi[i] < 0) hedef_cizgi_sirasi[i] = cizgiler.Length - 1;//bu konum deðil karýþtýrma
            hedef_cizgi[i] = cizgiler[hedef_cizgi_sirasi[i]];
            gecici_konum[i] = (hedef_cizgi[i].Length - kirp) - (kirp - gecici_konum[i]);


        }
        else if (gecici_konum[i] >kirp)
        {
            hedef_cizgi_sirasi[i]=cizgi_sirasi[0];
            hedef_cizgi[i] = cizgiler[hedef_cizgi_sirasi[i]];


        }
        //hareket_et(gorunur_obje, hedef_cizgi[0], hedef_egri[0], gecici_konum);

        //Hedef_konum_belirle(hedef_konum[0], hedef_cizgi[0], hedef_egri[0], gecici_konum);

        hedef_egri[i] = hedef_cizgi[i].GetSampleAtDistance(gecici_konum[i]);
        hedef_konum[i] = hedef_cizgi[i].transform.position;
        hedef_konum[i] += hedef_cizgi[i].transform.forward * hedef_egri[i].location.z;
        hedef_konum[i] += hedef_cizgi[i].transform.right * hedef_egri[i].location.x;
        hedef_konum[i] += hedef_cizgi[i].transform.up * hedef_egri[i].location.y;

       gorunur_obje.transform.position = hedef_konum[1];



        ///
        ///////burasý on ve arkakontrol noktalarý için///////

        //------------------------------------------------------------------------------------------------------



        if (drone_cizgi_sirasi[i] == hedef_cizgi_sirasi[i]) {



            if (drone_sirasi[i] < gecici_konum[i]) drone_sirasi[i] += 0.05f;
            if (drone_sirasi[i] > gecici_konum[i]) drone_sirasi[i] -= 0.05f;

        }
        else if (drone_cizgi_sirasi[i] == cizgiler.Length - 1 && hedef_cizgi_sirasi[i]==0) {
            drone_sirasi[i] += 0.1f;
        } else if (hedef_cizgi_sirasi[i]== cizgiler.Length - 1 && drone_cizgi_sirasi[i] ==0 ) {
            drone_sirasi[i] -= 0.1f;
        } else if ((drone_cizgi_sirasi[i] < hedef_cizgi_sirasi[i]) && (drone_cizgi_sirasi[i] != cizgiler.Length - 1))
        {
            drone_sirasi[i] += 0.1f;

        } else if ((drone_cizgi_sirasi[i] > hedef_cizgi_sirasi[i]) && (hedef_cizgi_sirasi[i] != cizgiler.Length - 1))
        {
            drone_sirasi[i] -= 0.1f;
        }

            for (int a = 0; a < dusmanlar.Length; a++)
            {
                if (a != i)
                {
                    if (Mathf.Abs(sag_sol[a] - sag_sol[i]) < 1.5f && Mathf.Abs(drone_sirasi[a] - drone_sirasi[i]) < 2)
                    {
                        if (drone_sirasi[a] > drone_sirasi[i])
                        {
                            drone_sirasi[a]+= 0.1f;
                            drone_sirasi[i]-= 0.1f;
                        }
                        else
                        {
                            drone_sirasi[a]-= 0.1f;
                            drone_sirasi[i]+= 0.1f;
                        }


                    }



                }

            }




            if (drone_sirasi[i] > drone_cizgi[i].Length - kirp)
        {
            drone_cizgi_sirasi[i]++;
            if (drone_cizgi_sirasi[i] == cizgiler.Length) drone_cizgi_sirasi[i] = 0;//bu konum deðil karýþtýrma
            drone_cizgi[i] = cizgiler[drone_cizgi_sirasi[i]];
            drone_sirasi[i] = kirp;

        }

        if (drone_sirasi[i] < kirp)
        {
            drone_cizgi_sirasi[i]--;
            if (drone_cizgi_sirasi[i] <0) drone_cizgi_sirasi[i] =cizgiler.Length-1 ;//bu konum deðil karýþtýrma
            drone_cizgi[i] = cizgiler[drone_cizgi_sirasi[i]];
            drone_sirasi[i] =drone_cizgi[i].Length- kirp;


        }

        Debug.Log(i);
            } 
        }


       

    }


    private void Hareket_et(GameObject obje,Spline cizgi,CurveSample egri,float konumu){
                egri = cizgi.GetSampleAtDistance(konumu);
        obje.transform.position = cizgi.transform.position;
        obje.transform.position += cizgi.transform.forward * egri.location.z;
        obje.transform.position += cizgi.transform.right * egri.location.x;
        obje.transform.position += cizgi.transform.up * egri.location.y;
        obje.transform.rotation = egri.Rotation;
        obje.transform.eulerAngles = new Vector3(obje.transform.rotation.eulerAngles.x,
            cizgi.transform.rotation.eulerAngles.y + obje.transform.rotation.eulerAngles.y,
            obje.transform.rotation.eulerAngles.z);

    }

     private void Drone_hareketi(GameObject obje,GameObject hedef,float hiz,float yukseklik,float sol_sag,float degisim)
     {
         Vector3 koordinat;

         koordinat = hedef.transform.position;
         koordinat += hedef.transform.right * sol_sag;



       float  x = Mathf.Lerp(obje.transform.position.x, koordinat.x, Time.deltaTime * hiz);
       float  y = Mathf.Lerp(obje.transform.position.y, koordinat.y+yukseklik, Time.deltaTime * hiz);
       float  z = Mathf.Lerp(obje.transform.position.z, koordinat.z, Time.deltaTime * hiz);

         obje.transform.position = new Vector3(x, y, z);
         obje.transform.LookAt(player.transform);

     }




    
}
