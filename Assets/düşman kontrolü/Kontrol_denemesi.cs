using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;

public class Kontrol_denemesi : MonoBehaviour
{
    /// <summary>
    /// /////////////////////buras� on ve arkakontrol noktalar� i�in//////////////////////
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
    /// //////////////buras� on ve arkakontrol noktalar� i�in///////////////////
    /// </summary>

    //kontrol noktalar�///////////////////////
    private float[] hedef_sirasi;
    private int[] hedef_cizgi_sirasi;
    private Spline[] hedef_cizgi;
    private CurveSample[] hedef_egri;
    private Vector3[] hedef_konum;
    public GameObject gorunur_obje;
    private float [] gecici_konum;
    public float sure=2,oynatma_vakti;



    ///////drone noktalar�///////////////////////
    private float[] drone_sirasi;
    private int[] drone_cizgi_sirasi;
    private Spline[] drone_cizgi;
    private CurveSample[] drone_egri;
    public GameObject [] drone_konum;
    ///////drone noktalar�///////////////////////


    ///////drone noktalar�///////////////////////
    public GameObject[] dusmanlar;
    private float[] yatay_degisim;
    private float[] sag_sol;
    public float sagsol_hiz=0.05f;
    public int maksimum_drone_sayisi = 2;


    ///////can �ubuklar�///////////////////////
    /*public GameObject can_bar;
    private Transform cam;*/
    // CAN BARI YAPIMINA SONRA KARAR VER.
    private float [] dusman_cani;
    public GameObject patlama;
    public GameObject parcali_drone1;




    private GameObject player;
    [Header("BURASI  PATLAMA �LE �LG�L�")]
    public float minforce;
    public float maxforce;
    public float radius;
    private GameObject[] infilak;
    private int  infilak_sirasi,onceki_infilak;
    public int infilak_sayisi=4;

    [Header("ENNKAZ �LE �LG�L�")]
    private float toplama_suresi=3,toplamaya_basla;
    private bool enkaz_topla=false;
    /*
        [Header("dusman ate�leri")]
        public GameObject ates;
        private GameObject[] muzzle;
        private int muzzle_sayisi,muzzle_sirasi;
    */

    private bool[] ates;
    private float[] ates_vakti;
    private float[] ates_araligi;
    private float [] oburune_gec;
    private float[] gecis_vakti;
    private bool[] gecis_bool;
    private Vector3[] ates_konum;



    void Start()
    {
        ///////buras� on ve arkakontrol noktalar� i�in///////
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
        ///////buras� on ve arkakontrol noktalar� i�in///////
        //------------------------------------------------------------------------
        //////////hedef konum belirlenmesi i�in buras� //////////////
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
        //////////hedef konum belirlenmesi i�in buras� //////////////

        //-------------------------------------------------------------------------
        //////////drone konum belirlenmesi i�in buras� //////////////       
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
        //////////drone konum belirlenmesi i�in buras� //////////////  
        ///
            player = GameObject.FindGameObjectWithTag("Player");
        //////////sa� sol de�i�im ////////////// 

            yatay_degisim = new  float[dusmanlar.Length];
            sag_sol =new  float[dusmanlar.Length];
        dusman_cani=new  float[dusmanlar.Length];
      //  muzzle_sayisi = 0;        
        for (int i = 0; i < dusmanlar.Length; i++)
        {
            yatay_degisim[i] = sag_sol[i] = 0;
            dusman_cani[i] = 100;
           
           /* if (dusmanlar[i].tag == "drone_5")
            {
                muzzle_sayisi++;
            }*/


        
        }

        infilak = new GameObject[infilak_sayisi];

        for (int i = 0; i < infilak_sayisi; i++)
        {

            infilak[i] = Instantiate(patlama, patlama.transform.position, Quaternion.identity);
            infilak[i].SetActive(false);
        }
        infilak_sirasi = 0;
        //////////sa� sol de�i�im ////////////// 
        //cam = Camera.main.transform;CAN BARI YAPIMINA SONRA KARAR VER.
        patlama.SetActive(false);



        //////////ates etme ayarlar� ////////////// 
        ates = new bool[dusmanlar.Length];
        ates_vakti= new float[dusmanlar.Length];
        ates_araligi= new float[dusmanlar.Length];
        oburune_gec=new float[dusmanlar.Length];
        gecis_vakti=new float[dusmanlar.Length];
        gecis_bool= new bool[dusmanlar.Length];
        ates_konum=new Vector3[dusmanlar.Length];
        for (int i = 0; i < dusmanlar.Length; i++)
        {
            ates[i]=false;
            ates_araligi[i] = Random.Range(5, 10);
            ates_vakti[i] = Time.time + ates_araligi[i];
            oburune_gec[i]=1;//bunu dier dronelar ile ayr� ayr� yap
            gecis_vakti[i] = Time.time + oburune_gec[i];
            gecis_bool[i]=false;
        }


       /* muzzle = new GameObject[muzzle_sayisi];
        for (int i = 0; i < muzzle_sayisi; i++)
        {

           muzzle[i]=Instantiate(ates, patlama.transform.position, Quaternion.identity);
           muzzle[i].SetActive(false);

        }
        Debug.Log(muzzle_sayisi);
*/
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
                Hareket_et(drone_konum[i], drone_cizgi[i], drone_egri[i], drone_sirasi[i]);//hareket kodlar� burada olsun (sens�r harici olanlar)
            Drone_hareketi(dusmanlar[i], drone_konum[i], 5, 2, sag_sol[i], yatay_degisim[i]);
            if (Mathf.Abs(sag_sol[i] - yatay_degisim[i]) < 0.3f)
            {
                yatay_degisim[i] = Random.Range(-5, 6);//bunlar� sonra for d�ng�s�nde �al��t�r.

            }

            if (sag_sol[i] > yatay_degisim[i]) sag_sol[i] -= sagsol_hiz;//bunlar� sonra for d�ng�s�nde �al��t�r.
            if (sag_sol[i] < yatay_degisim[i]) sag_sol[i] += sagsol_hiz;//bunlar� sonra for d�ng�s�nde �al��t�r.
             }

        }

        ///////buras� on ve arkakontrol noktalar� i�in///////
        if (ileri) {
        for (int i = 0; i < konum_sirasi.Length; i++)
        {
            Hareket_et(cube[i], mevcut_cizgiler[i], mevcut_egriler[i], konum_sirasi[i]);
            konum_sirasi[i] += otelenme;

            if (konum_sirasi[i] > mevcut_cizgiler[i].Length - kirp)
            {
                cizgi_sirasi[i]++;
                if (cizgi_sirasi[i] == cizgiler.Length) cizgi_sirasi[i] = 0;//bu konum de�il kar��t�rma
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
                if (cizgi_sirasi[i]<0) cizgi_sirasi[i] = cizgiler.Length-1;//bu konum de�il kar��t�rma
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
        ///////buras� on ve arkakontrol noktalar� i�in///////bitti
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
            if (hedef_cizgi_sirasi[i] < 0) hedef_cizgi_sirasi[i] = cizgiler.Length - 1;//bu konum de�il kar��t�rma
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
        ///////buras� on ve arkakontrol noktalar� i�in///////

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
            if (drone_cizgi_sirasi[i] == cizgiler.Length) drone_cizgi_sirasi[i] = 0;//bu konum de�il kar��t�rma
            drone_cizgi[i] = cizgiler[drone_cizgi_sirasi[i]];
            drone_sirasi[i] = kirp;

        }

        if (drone_sirasi[i] < kirp)
        {
            drone_cizgi_sirasi[i]--;
            if (drone_cizgi_sirasi[i] <0) drone_cizgi_sirasi[i] =cizgiler.Length-1 ;//bu konum de�il kar��t�rma
            drone_cizgi[i] = cizgiler[drone_cizgi_sirasi[i]];
            drone_sirasi[i] =drone_cizgi[i].Length- kirp;


        }


            }
            //////////// d��man vurulmas�///////////////////
            if (player.GetComponent<CarController>().vuruldu == true)
            {
                string isim = player.GetComponent<CarController>().vurulan;
                // RaycastHit carp = player.GetComponent<CarController>().hit;

               for(int a = 0; a < dusmanlar.Length; a++) { 
                if (dusmanlar[a].activeSelf == true)
                    { 

                    if (dusmanlar[a] != null && dusmanlar[a].name == isim)
                    {

                    dusman_cani[a] -= 50;

                        if (dusman_cani[a] == 0 || dusman_cani[a] < 0)
                        {
                            infilak[infilak_sirasi].SetActive(true);
                            infilak[infilak_sirasi].transform.position=dusmanlar[a].transform.position;
                                Patla(dusmanlar[a],parcali_drone1);
                                /*parcali_drone1.transform.position=dusmanlar[a].transform.position;
                                parcali_drone1.SetActive(true);
                                dusmanlar[a].SetActive(false);
*/



                                onceki_infilak = infilak_sirasi + 2;
                                if(onceki_infilak==infilak_sayisi || onceki_infilak > infilak_sayisi)
                                {
                                    onceki_infilak = onceki_infilak - infilak_sayisi;

                                }
                            infilak[onceki_infilak].SetActive(false);
                                infilak_sirasi++;
                                if (infilak_sirasi == 3) infilak_sirasi = 0;
                                toplamaya_basla = Time.time + toplama_suresi;
                                enkaz_topla = true;


                        /*patlama.SetActive(true);
                        patlama.transform.position = dusmanlar[a].transform.position;
                            parcali_drone1.transform.position=dusmanlar[a].transform.position;
                            parcali_drone1.SetActive(true);

                            dusmanlar[a].SetActive(false);*/
                        }

                    }
                 }
                }




                player.GetComponent<CarController>().vuruldu = false;

            }
            //////////// ates etme kodlar�///////////////////
            if (Time.time > ates_vakti[i])
            {
                ates[i] = true;

                if(Time.time > ates_vakti[i] + ates_araligi[i])
                {
                    ates_araligi[i] = Random.Range(5, 10);
                    ates_vakti[i] = Time.time + ates_araligi[i];

                }


            } else ates[i] = false;




            if (ates[i])
            {
                if (dusmanlar[i].tag == "drone_5")
                {
                    if(Time.time > gecis_vakti[i])
                    {
                        gecis_vakti[i] = Time.time + oburune_gec[i];
                        if (gecis_bool[i] == false)
                        {
                            gecis_bool[i] = true;
                            ates_konum[i] = new Vector3(3.89f, 0.27f, 6.32f);
                        }
                        else {
                             ates_konum[i]= new Vector3(-3.89f, 0.27f, 6.32f);
                             gecis_bool[i] = false;        

                        }




                    }


                    GameObject muzzle =dusmanlar[i].transform.GetChild(0).gameObject;
                    muzzle.SetActive(true);
                    muzzle.transform.localPosition = ates_konum[i];
                }
                
               
            }else {
                if (dusmanlar[i].tag == "drone_5") {
                    GameObject muzzle =dusmanlar[i].transform.GetChild(0).gameObject;
                    muzzle.SetActive(false);
 }

                }




        }

        // Debug.Log(dusman_cani[1]+"   "+dusmanlar[1].tag);
        /*CAN BARI YAPIMINA SONRA KARAR VER.
        can_bar.transform.position = dusmanlar[0].transform.position + new Vector3(0, 0.5f, 0);
        can_bar.transform.position += dusmanlar[0].transform.forward * 1.2f; 
        can_bar.transform.LookAt(can_bar.transform.position + cam.forward);
       */
        if (enkaz_topla)
        {

            if(toplamaya_basla< Time.time)
            {
                foreach (Transform t in parcali_drone1.transform)
                {

                    t.localPosition = Vector3.zero;
                    parcali_drone1.SetActive(false);
                }


                enkaz_topla = false;
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


    private void Patla(GameObject pasif_olcak,GameObject aktif_olacak)
    {
        aktif_olacak.transform.position = pasif_olcak.transform.position;
        aktif_olacak.transform.rotation = pasif_olcak.transform.rotation;
        aktif_olacak.SetActive(true);
        pasif_olcak.SetActive(false);

        foreach (Transform t in aktif_olacak.transform)
        {
            var rb = t.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 patlama_noktasi = aktif_olacak.transform.position;//- new Vector3(0, 0, 0)
                rb.AddExplosionForce(Random.Range(minforce, maxforce), patlama_noktasi, radius);
            }


        }



    }

    
}
