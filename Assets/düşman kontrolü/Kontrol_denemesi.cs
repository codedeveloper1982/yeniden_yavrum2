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
    public float on, arka,dusman_eklenen_yer;
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
    private float[] gecici_konum;
    public float sure = 2, oynatma_vakti;



    ///////drone noktalarý///////////////////////
    private float[] drone_sirasi;
    private int[] drone_cizgi_sirasi;
    private Spline[] drone_cizgi;
    private CurveSample[] drone_egri;
    public GameObject konum_noktasi;
    private GameObject[] drone_konum;
    ///////drone noktalarý///////////////////////


    ///////drone noktalarý///////////////////////
    public GameObject[] dusmanlar;
    private float[] yatay_degisim;
    private float[] dikey_degisim;
    private float[] sag_sol;
    private float[] cik_in;
    public float sagsol_hiz = 0.03f;
    public float cikin_hiz = 0.01f;
    public int maksimum_drone_sayisi = 2;


    ///////can çubuklarý///////////////////////
    /*public GameObject can_bar;
    private Transform cam;*/
    // CAN BARI YAPIMINA SONRA KARAR VER.
    private float[] dusman_cani;
    public GameObject patlama;
    public GameObject parcali_drone1,parcali_drone5,parcali_drone11c,parcali_mayinci,parcali_drone6;




    private GameObject player;
    [Header("BURASI  PATLAMA ÝLE ÝLGÝLÝ")]
    public float minforce;
    public float maxforce;
    public float radius;
    private GameObject[] infilak;
    private int infilak_sirasi, onceki_infilak;
    public int infilak_sayisi = 4;

    [Header("ENNKAZ ÝLE ÝLGÝLÝ")]
    private float toplama_suresi = 4;
    private float[] toplamaya_basla;
    private string[] toplanan_drone;
    private GameObject yerden_kaldir;
    private bool[] enkaz_topla;
    /*
        [Header("dusman ateþleri")]
        public GameObject ates;
        private GameObject[] muzzle;
        private int muzzle_sayisi,muzzle_sirasi;
    */

    private bool[] ates;
    private float[] ates_vakti;
    private float[] ates_araligi;
    private float[] oburune_gec;
    private float[] gecis_vakti;
    private bool[] gecis_bool;
    private Vector3[] ates_konum;

    [Header("drone 6 füzeleri")]
    private GameObject[] fuzeler;// mermiler dizisi oluþturur
    private GameObject[] fuze_trails;
    private GameObject[] fuze_pat;
    private GameObject[] fuze_buyuk_pat;
    private float[] fuze_zamani;
    public GameObject fuze;// çoðaltýlacak füze
    public GameObject fuzeatesi;
    public GameObject pat;
    public GameObject buyuk_pat;
    private Vector3 []shootpoint;// füze çýkýþ moktasý
    public int fuzesayisi;// aktif olacak füze sayýsý
    private int sira;// aktif füze sýrasý
    private int ilk;//
    private Vector3 hedef;
    private float[] yon_zamani;
    private float yon_rate = 0.1f;
    private Vector3[] onceki_pozisyon;
    private bool[] fuze_at;
    Vector3[] vo;
    [SerializeField] private float sensorbaslangýcý;
    [SerializeField] private float fuze_sensoru_uzunlugu;


    [Header("kývývým kodlarý")]
    public GameObject kivilcim_prefab;
    private GameObject[] kivilcimlar;
    private int kivilcim_sayisi = 10,ilk_kivilcim, son_kivilcim;

    [Header("laser kodlarý")]
    public GameObject laser_prefab;
    private GameObject[] laserler;
    private GameObject [] laser_pat;
    private int laser_sayisi = 10,ilk_laser, son_laser;
    private Vector3 laserpoint;
    private bool laser_at;
    [SerializeField] private float laser_sensorbaslangýcý;
    [SerializeField] private float laser_sensoru_uzunlugu;

    [Header("rocket kodlarý")]
    private GameObject[] rocketler;// mermiler dizisi oluþturur
    private GameObject[] rocket_trails;
    private GameObject[] rocket_pat;
    private float[] rocket_hiz_x,rocket_hiz_y,rocket_hiz_z;
    public GameObject rocket;// çoðaltýlacak füze
    private GameObject[] parent_rocket;

   // private Vector3[] rocket_shootpoint;// füze çýkýþ moktasý
    public int rocketsayisi=6;// aktif olacak füze sayýsý
    private int rocket_sira;// aktif füze sýrasý
    private int rocket_ilk;//
    private float rocket_hiz = 0.2f;
    private Vector3 rocket_hedef;
    private bool[] rocket_at;
    [SerializeField] private float rocket_sensorbaslangýcý;
    [SerializeField] private float rocket_sensoru_uzunlugu;



    [Header("mayin kodlarý")]
    private GameObject[] mayinlar;
    public GameObject mayin_prefab;
    private GameObject[] mayin_pat;
    private Vector3[] mayin_hedefi;

    public int mayin_sayisi = 6;// aktif olacak füze sayýsý
    private int mayin_sira;// aktif füze sýrasý
    private int mayin_ilk;//
    private bool[] mayin_birak;


    ///////aktif düþman sayýsý      ///////////////////////
    private int aktif_dusman_sayisi,dusman_sirasi,onceki_sira,sonraki_sira;
    private bool diger_dusmana_gec;
    private int [] dusman_seviyesi= { 1, 2, 3 };
    private float obur_dusmana_gecis_vakti;
    private float dusman_hizi =5;
    private float oteleme_dur_kalk = 0;
    private float uzaklik;


    void Start()
    {
        ///////burasý on ve arkakontrol noktalarý için///////bide dusman ekleme yeri
        konum_sirasi = new float[3];
        cizgi_sirasi = new int[3];
        mevcut_cizgiler = new Spline[3];
        mevcut_egriler = new CurveSample[3];
        arka = kirp;
        konum_sirasi[0] = on;
        konum_sirasi[1] =  arka;
        konum_sirasi[2] =  dusman_eklenen_yer;
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
        drone_konum= new GameObject[dusmanlar.Length];

        for (int i = 0; i < dusmanlar.Length; i++)
        {
            drone_cizgi_sirasi[i] = 0;
            drone_cizgi[i] = cizgiler[drone_cizgi_sirasi[i]];
            drone_sirasi[i] = konum_sirasi[0];
            drone_konum[i] = Instantiate(konum_noktasi,transform.position,Quaternion.identity);
        }
        /////
        //////////drone konum belirlenmesi için burasý //////////////  
        ///
            player = GameObject.FindGameObjectWithTag("Player");
        //////////sað sol deðiþim ////////////// 

            yatay_degisim = new  float[dusmanlar.Length];
            dikey_degisim=new  float[dusmanlar.Length];
            sag_sol =new  float[dusmanlar.Length];
            cik_in=new  float[dusmanlar.Length];
        dusman_cani=new  float[dusmanlar.Length];
      //  muzzle_sayisi = 0;        
        for (int i = 0; i < dusmanlar.Length; i++)
        {
            yatay_degisim[i] = sag_sol[i] = 0;
            dikey_degisim[i] = cik_in[i] = 0;
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
        //////////sað sol deðiþim ////////////// 
        //cam = Camera.main.transform;CAN BARI YAPIMINA SONRA KARAR VER.
        patlama.SetActive(false);



        //////////ates etme ayarlarý ////////////// 
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
            ates_araligi[i] = Random.Range(5.0f, 10.0f);
            ates_vakti[i] = Time.time + ates_araligi[i];
            oburune_gec[i]=1;//bunu dier dronelar ile ayrý ayrý yap
            gecis_vakti[i] = Time.time + oburune_gec[i];
            gecis_bool[i]=false;
        }


        //////////drone 6  fuze ayarlarý //////////////         
        hedef =player.transform.position;//diðerinde  update içine yaz bu kodu
        //shootpoint = transform.GetChild(0).position;          //bunu update içerisinde belirle
        fuzeler = new GameObject[fuzesayisi];
        fuze_trails = new GameObject[fuzesayisi];
        fuze_pat = new GameObject[fuzesayisi];
        fuze_buyuk_pat=new GameObject[fuzesayisi];
        fuze_zamani = new float[fuzesayisi];
        yon_zamani = new float[fuzesayisi];
        onceki_pozisyon = new Vector3[fuzesayisi];
        shootpoint= new Vector3[fuzesayisi];
        vo=new Vector3[fuzesayisi];
        fuze_at= new bool[fuzesayisi];
        
        for (int i = 0; i < fuzesayisi; i++)
        {
            shootpoint[i] = Vector3.zero;
            fuzeler[i] = Instantiate(fuze, shootpoint[i], Quaternion.identity);
            fuze_trails[i] = Instantiate(fuzeatesi, shootpoint[i], Quaternion.identity);
            fuze_pat[i] = Instantiate(pat, shootpoint[i], Quaternion.identity);
            fuze_buyuk_pat[i] = Instantiate(buyuk_pat, shootpoint[i], Quaternion.identity);
            fuze_zamani[i] = 0;
            //mermiler[i].tag = "roket" + i;
            fuzeler[i].SetActive(false);
            fuze_trails[i].SetActive(false);
            fuze_pat[i].SetActive(false);
            fuze_buyuk_pat[i].SetActive(false);
            onceki_pozisyon[i] = shootpoint[i];
            fuze_at[i] = false;

            yon_zamani[i] = Time.time + yon_rate;



        }
        sira = 0;
        ilk = sira - (fuzesayisi - 1);
        //////////////////////////////////////kývýlcým ayarlarý///////////////////////////
        son_kivilcim = 0;
        ilk_kivilcim = son_kivilcim - (kivilcim_sayisi - 1);

        kivilcimlar = new GameObject[kivilcim_sayisi];

        for (int i = 0; i < kivilcim_sayisi; i++)
        {
            kivilcimlar[i] = Instantiate(kivilcim_prefab, transform.position, Quaternion.identity);
            kivilcimlar[i].SetActive(false);
        }
        //////////////////////////////////////laser ayarlarý///////////////////////////
        son_laser = 0;
        ilk_laser = son_laser - (laser_sayisi - 1);

        laserler = new GameObject[laser_sayisi];
        laser_pat= new GameObject[laser_sayisi];

        for (int i = 0; i < laser_sayisi; i++)
        {
            laserler[i] = Instantiate(laser_prefab, transform.position, Quaternion.identity);
            laser_pat[i] = Instantiate(pat, transform.position, Quaternion.identity);
            laserler[i].SetActive(false);
            laser_pat[i].SetActive(false);
        }
        //////////////////////////////////////rocket ayarlarý///////////////////////////
        rocket_sira = 0;
        rocket_ilk = rocket_sira - (rocketsayisi - 1);

        rocketler = new GameObject[rocketsayisi];
        rocket_trails= new GameObject[rocketsayisi];
        rocket_pat = new GameObject[rocketsayisi];
        rocket_hiz_x = new float[rocketsayisi];
        rocket_hiz_y = new float[rocketsayisi];
        rocket_hiz_z = new float[rocketsayisi];
        rocket_at=new bool[rocketsayisi];
        parent_rocket=new GameObject[rocketsayisi];

        for (int i = 0; i < rocketsayisi; i++)
        {
            rocketler[i] = Instantiate(rocket, transform.position, Quaternion.identity);
            rocket_trails[i] = Instantiate(fuzeatesi, transform.position, Quaternion.identity);
            rocket_pat[i] = Instantiate(pat, transform.position, Quaternion.identity);
            rocket_hiz_x[i] = 0.0f;
            rocket_hiz_y[i] = 0.0f;
            rocket_hiz_z[i] = 0.0f;
            rocketler[i].SetActive(false);
            rocket_trails[i].SetActive(false);
            rocket_pat[i].SetActive(false);
            rocket_at[i] = false;
        }

        //////////////////////////////////////rocket ayarlarý///////////////////////////
        ///
        mayin_sira = 0;
        mayin_ilk = mayin_sira - (mayin_sayisi - 1);

        mayinlar = new GameObject[mayin_sayisi];
        mayin_pat= new GameObject[mayin_sayisi];
        mayin_birak=new bool[mayin_sayisi];
        mayin_hedefi=new Vector3[mayin_sayisi];
        for (int i = 0; i < mayin_sayisi; i++)
        {
            mayinlar[i] = Instantiate(mayin_prefab, transform.position, Quaternion.identity);
            mayin_pat[i] = Instantiate(pat, transform.position, Quaternion.identity);
            mayin_pat[i].SetActive(false);
            mayinlar[i].SetActive(false); 
        }
        ////////////////////dusman sayýsý////////////////// enkaz ile ilgili ayarlarda burada
        ///
        
        diger_dusmana_gec = false;
        dusman_sirasi =0;
        aktif_dusman_sayisi = 0;
        onceki_sira = 0;
        toplamaya_basla = new float[dusmanlar.Length];
        toplanan_drone= new string[dusmanlar.Length];
        enkaz_topla= new bool[dusmanlar.Length];

        for(int i = 0; i < dusmanlar.Length; i++)
        {
            dusmanlar[i].SetActive(false);
            enkaz_topla[i] = false;
            toplamaya_basla[i] = 0;

        }
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
                uzaklik = Mathf.Abs(gecici_konum[i] - drone_sirasi[i]);

                    if (uzaklik < 7.0f)
                    {
                        oteleme_dur_kalk = otelenme;

                    }
                    else
                    {
                        oteleme_dur_kalk = 0;
                    }

                Hareket_et(drone_konum[i], drone_cizgi[i], drone_egri[i], drone_sirasi[i]);//hareket kodlarý burada olsun (sensör harici olanlar)
                Drone_hareketi(dusmanlar[i], drone_konum[i], dusman_hizi, sag_sol[i], cik_in[i]);
                if (Mathf.Abs(sag_sol[i] - yatay_degisim[i]) < 0.3f)
                {
                    yatay_degisim[i] = Random.Range(-5, 6);//bunlarý sonra for döngüsünde çalýþtýr.

                }

                if (sag_sol[i] > yatay_degisim[i]) sag_sol[i] -= sagsol_hiz;//bunlarý sonra for döngüsünde çalýþtýr.
                if (sag_sol[i] < yatay_degisim[i]) sag_sol[i] += sagsol_hiz;//bunlarý sonra for döngüsünde çalýþtýr.
               
                
                if (Mathf.Abs(cik_in[i] - dikey_degisim[i]) < 0.33f)
                {
                    dikey_degisim[i] = Random.Range(0, 5);//bunlarý sonra for döngüsünde çalýþtýr.

                }

                if (cik_in[i] > dikey_degisim[i]) cik_in[i] -= cikin_hiz;//bunlarý sonra for döngüsünde çalýþtýr.
                if (cik_in[i] < dikey_degisim[i]) cik_in[i] += cikin_hiz;//bunlarý sonra for döngüsünde çalýþtýr.


            }

        }

        ///////burasý on ve arkakontrol noktalarý için///////
        if (ileri)
        {
            for (int i = 0; i < konum_sirasi.Length; i++)
            {
                Hareket_et(cube[i], mevcut_cizgiler[i], mevcut_egriler[i], konum_sirasi[i]);
                konum_sirasi[i] += otelenme;

                if (konum_sirasi[i] > mevcut_cizgiler[i].Length - kirp)
                {
                    cizgi_sirasi[i]++;
                    if (cizgi_sirasi[i] == cizgiler.Length) cizgi_sirasi[i] = 0;//bu konum deðil karýþtýrma
                    mevcut_cizgiler[i] = cizgiler[cizgi_sirasi[i]];
                    konum_sirasi[i] = kirp;
                }

            }

            for (int i = 0; i < dusmanlar.Length; i++)
            {

                if (dusmanlar[i].activeSelf == true)
                {


                    drone_sirasi[i] += oteleme_dur_kalk;
                    
                    
                }
                
            }
            ileri = false;
        }
        if (geri)
        {

            for (int i = 0; i < konum_sirasi.Length; i++)
            {
                Hareket_et(cube[i], mevcut_cizgiler[i], mevcut_egriler[i], konum_sirasi[i]);
                konum_sirasi[i] -= otelenme;

                if (konum_sirasi[i] < kirp)
                {
                    cizgi_sirasi[i]--;
                    if (cizgi_sirasi[i] < 0) cizgi_sirasi[i] = cizgiler.Length - 1;//bu konum deðil karýþtýrma
                    mevcut_cizgiler[i] = cizgiler[cizgi_sirasi[i]];
                    konum_sirasi[i] = mevcut_cizgiler[i].Length - kirp;
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
            if (dusmanlar[i].activeSelf == true)
            {
                if (Mathf.Abs(gecici_konum[i] - drone_sirasi[i]) < 0.2f)
                {
                    hedef_sirasi[i] = Random.Range(2, 11);


                }



                gecici_konum[i] = konum_sirasi[0] - hedef_sirasi[i];

                if (gecici_konum[i] < kirp)
                {
                    hedef_cizgi_sirasi[i] = cizgi_sirasi[0] - 1;
                    if (hedef_cizgi_sirasi[i] < 0) hedef_cizgi_sirasi[i] = cizgiler.Length - 1;//bu konum deðil karýþtýrma
                    hedef_cizgi[i] = cizgiler[hedef_cizgi_sirasi[i]];
                    gecici_konum[i] = (hedef_cizgi[i].Length - kirp) - (kirp - gecici_konum[i]);


                }
                else if (gecici_konum[i] > kirp)
                {
                    hedef_cizgi_sirasi[i] = cizgi_sirasi[0];
                    hedef_cizgi[i] = cizgiler[hedef_cizgi_sirasi[i]];


                }
                //hareket_et(gorunur_obje, hedef_cizgi[0], hedef_egri[0], gecici_konum);

                //Hedef_konum_belirle(hedef_konum[0], hedef_cizgi[0], hedef_egri[0], gecici_konum);

                hedef_egri[i] = hedef_cizgi[i].GetSampleAtDistance(gecici_konum[i]);
                hedef_konum[i] = hedef_cizgi[i].transform.position;
                hedef_konum[i] += hedef_cizgi[i].transform.forward * hedef_egri[i].location.z;
                hedef_konum[i] += hedef_cizgi[i].transform.right * hedef_egri[i].location.x;
                hedef_konum[i] += hedef_cizgi[i].transform.up * hedef_egri[i].location.y;

                //gorunur_obje.transform.position = hedef_konum[1];                       bunu sonra silebilirsin



                ///
                ///////burasý on ve arkakontrol noktalarý için///////

                //------------------------------------------------------------------------------------------------------



                if (drone_cizgi_sirasi[i] == hedef_cizgi_sirasi[i])
                {
                    float yaklasma;
                    if (uzaklik < 7) yaklasma = 0.05f;
                    else yaklasma = 0.2f;
                    


                    if (drone_sirasi[i] < gecici_konum[i]) drone_sirasi[i] +=yaklasma;
                    if (drone_sirasi[i] > gecici_konum[i]) drone_sirasi[i] -=yaklasma;

                }
                else if (drone_cizgi_sirasi[i] == cizgiler.Length - 1 && hedef_cizgi_sirasi[i] == 0)
                {
                    drone_sirasi[i] += 0.1f;
                }
                else if (hedef_cizgi_sirasi[i] == cizgiler.Length - 1 && drone_cizgi_sirasi[i] == 0)
                {
                    drone_sirasi[i] -= 0.1f;
                }
                else if ((drone_cizgi_sirasi[i] < hedef_cizgi_sirasi[i]) && (drone_cizgi_sirasi[i] != cizgiler.Length - 1))
                {
                    drone_sirasi[i] += 0.1f;

                }
                else if ((drone_cizgi_sirasi[i] > hedef_cizgi_sirasi[i]) && (hedef_cizgi_sirasi[i] != cizgiler.Length - 1))
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
                                drone_sirasi[a] += 0.1f;
                                drone_sirasi[i] -= 0.1f;
                            }
                            else
                            {
                                drone_sirasi[a] -= 0.1f;
                                drone_sirasi[i] += 0.1f;
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
                    if (drone_cizgi_sirasi[i] < 0) drone_cizgi_sirasi[i] = cizgiler.Length - 1;//bu konum deðil karýþtýrma
                    drone_cizgi[i] = cizgiler[drone_cizgi_sirasi[i]];
                    drone_sirasi[i] = drone_cizgi[i].Length - kirp;


                }


            }
            //////////// düþman vurulmasý///////////////////
            if (player.GetComponent<CarController>().vuruldu == true)
            {
                string isim = player.GetComponent<CarController>().vurulan;
                // RaycastHit carp = player.GetComponent<CarController>().hit;

                for (int a = 0; a < dusmanlar.Length; a++)
                {
                    if (dusmanlar[a].activeSelf == true)
                    {

                        if (dusmanlar[a] != null && dusmanlar[a].name == isim)
                        {

                            dusman_cani[a] -= 50;

                            if (dusman_cani[a] == 0 || dusman_cani[a] < 0)
                            {
                                infilak[infilak_sirasi].SetActive(true);
                                infilak[infilak_sirasi].transform.position = dusmanlar[a].transform.position;
                                if(dusmanlar[a].tag=="drone_1b")Patla(dusmanlar[a], parcali_drone1);
                                else if(dusmanlar[a].tag=="drone_5")Patla(dusmanlar[a], parcali_drone5);
                                else if(dusmanlar[a].tag=="drone11_c")Patla(dusmanlar[a], parcali_drone11c);
                                else if(dusmanlar[a].tag=="mayinci")Patla(dusmanlar[a],parcali_mayinci);
                                else if(dusmanlar[a].tag=="füzeci")Patla(dusmanlar[a],parcali_drone6);                                
                                /*parcali_drone1.transform.position=dusmanlar[a].transform.position;
                                parcali_drone1.SetActive(true);
                                dusmanlar[a].SetActive(false);
*/



                                onceki_infilak = infilak_sirasi + 2;
                                if (onceki_infilak == infilak_sayisi || onceki_infilak > infilak_sayisi)
                                {
                                    onceki_infilak = onceki_infilak - infilak_sayisi;

                                }
                                infilak[onceki_infilak].SetActive(false);
                                infilak_sirasi++;
                                if (infilak_sirasi == 3) infilak_sirasi = 0;
                                toplamaya_basla[a] = Time.time + toplama_suresi;
                                enkaz_topla[a] = true;
                                toplanan_drone[a] = dusmanlar[a].tag;
                                aktif_dusman_sayisi--;
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
            //////////// ates etme kodlarý///////////////////
            if (Time.time > ates_vakti[i] && dusmanlar[i].activeSelf == true)
            {
                ates[i] = true;

                if (Time.time > ates_vakti[i] + ates_araligi[i])
                {
                    ates_araligi[i] = Random.Range(5.0f, 10.0f); 
                    ates_vakti[i] = Time.time + ates_araligi[i];

                }


            }
            else ates[i] = false;




            if (ates[i])
            {
                if (dusmanlar[i].tag == "drone_5")
                {
                    if (Time.time > gecis_vakti[i])
                    {
                        gecis_vakti[i] = Time.time + oburune_gec[i];
                        if (gecis_bool[i] == false)
                        {
                            gecis_bool[i] = true;
                            ates_konum[i] = new Vector3(3.89f, 0.27f, 6.32f);
                        }
                        else
                        {
                            ates_konum[i] = new Vector3(-3.89f, 0.27f, 6.32f);
                            gecis_bool[i] = false;

                        }




                    }


                    GameObject muzzle = dusmanlar[i].transform.GetChild(0).gameObject;
                    muzzle.SetActive(true);
                    muzzle.transform.localPosition = ates_konum[i];

                    Vector3 fuze_ucu = muzzle.transform.position+new Vector3(Random.Range(0,2),Random.Range(0,2),Random.Range(0,2));
                    //fuze_ucu.z += sensorbaslangýcý;
                    RaycastHit hit;
                    if (Physics.Raycast(fuze_ucu, muzzle.transform.forward, out hit, 15))
                    {
                        if (hit.collider.tag == "Player") { 
                        kivilcimlar[son_kivilcim].SetActive(true);
                        kivilcimlar[son_kivilcim].transform.position = hit.point;
                        kivilcimlar[son_kivilcim].transform.rotation = Quaternion.LookRotation(hit.normal);
                        kivilcimlar[son_kivilcim].transform.SetParent(hit.collider.transform);
                        kivilcimlar[son_kivilcim].transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                        son_kivilcim++;
                        ilk_kivilcim++;
                        if (son_kivilcim == kivilcim_sayisi)
                        {

                            son_kivilcim = 0;
                        }
                         if (ilk_kivilcim == kivilcim_sayisi)
                        {

                             ilk_kivilcim= 0;
                        }
                        if (ilk_kivilcim >= 0)
                        {
                       kivilcimlar[ilk_kivilcim].SetActive(false);

                        }
                       }
                    }
                    Debug.DrawRay(fuze_ucu, muzzle.transform.forward * 15, Color.green);


                }
                else if (dusmanlar[i].tag == "füzeci")//drone 6 ateþ etmesi bu 
                {
                    if (Time.time > gecis_vakti[i])
                    {
                        gecis_vakti[i] = Time.time + oburune_gec[i];
                        if (gecis_bool[i] == false)
                        {
                            gecis_bool[i] = true;
                            ates_konum[i] = new Vector3(3, 0.6f, 0.7f);
                            fuze_at[i] = true;
                        }
                        else
                        {
                            ates_konum[i] = new Vector3(-3, 0.6f, 0.7f);
                            gecis_bool[i] = false;
                            fuze_at[i] = true;

                        }
                    }

                    if (fuze_at[i])
                    {
                        GameObject fuze_shootpoint = dusmanlar[i].transform.GetChild(0).gameObject;
                        fuze_shootpoint.transform.localPosition = ates_konum[i];
                        shootpoint[sira] = fuze_shootpoint.transform.position;
                        fuzeler[sira].transform.position = shootpoint[sira];
                        fuze_trails[sira].transform.position = shootpoint[sira];
                        onceki_pozisyon[sira] = shootpoint[sira];
                        //mermiler[sira].transform.rotation = shootpoint;
                        fuzeler[sira].SetActive(true);
                        fuze_trails[sira].SetActive(true);
                        fuze_zamani[sira] = 0;
                        float hedef_miktari = Random.Range(5, 7);
                        float max_hiz = player.transform.GetComponent<CarController>().MaxSpeed;
                        float mevcut_hiz = player.transform.GetComponent<CarController>().CurrentSpeed;
                        hedef = player.transform.position + player.transform.forward *(hedef_miktari-(5-5*(mevcut_hiz/max_hiz)));
                         vo[sira] = Calculate_velocity(hedef, shootpoint[sira], 1f);
                        fuzeler[sira].transform.rotation = Quaternion.LookRotation(vo[sira]);
                        sira++;
                        ilk++;
                        if (sira == fuzesayisi)
                        {
                            sira = 0;
                        }
                        if (ilk == fuzesayisi)
                        {
                            ilk = 0;
                        }

                        if (ilk >= 0)
                        {

                            fuzeler[ilk].SetActive(false);
                            fuze_trails[ilk].SetActive(false);
                            fuze_pat[ilk].SetActive(false);
                            fuze_buyuk_pat[ilk].SetActive(false);




                        }
                        fuze_at[i] = false;
                    }

                }else if(dusmanlar[i].tag == "drone11_c")
                {

                    if (Time.time > gecis_vakti[i])
                    {
                        gecis_vakti[i] = Time.time + oburune_gec[i];
                        if (gecis_bool[i] == false)
                        {
                            gecis_bool[i] = true;
                            ates_konum[i] = new Vector3(0.63f, -0.09f, 0.6f);
                            laser_at = true;
                        }
                        else
                        {
                            ates_konum[i] = new Vector3(-0.63f, -0.09f, 0.6f);
                            gecis_bool[i] = false;
                            laser_at = true;
                        }
                    }



                    if (laser_at)
                    {
                        GameObject l_shootpoint = dusmanlar[i].transform.GetChild(0).gameObject;
                        l_shootpoint.transform.localPosition = ates_konum[i];
                        laserpoint = l_shootpoint.transform.position;
                        laserler[son_laser].transform.position = laserpoint;
                        laserler[son_laser].transform.rotation =dusmanlar[i].transform.rotation;
                        laserler[son_laser].SetActive(true);

                        son_laser++;
                        ilk_laser++;
                        if (son_laser == laser_sayisi)
                        {
                            son_laser = 0;
                        }
                        if (ilk_laser ==laser_sayisi )
                        {
                            ilk_laser = 0;
                        }

                        if (ilk_laser >= 0)
                        {

                            laserler[ilk_laser].SetActive(false);
                            laser_pat[ilk_laser].SetActive(false);





                        }


                        laser_at = false;
                    }

                         

                }else if (dusmanlar[i].tag == "drone_1b")
                {

                    if (Time.time > gecis_vakti[i])
                    {
                        gecis_vakti[i] = Time.time + oburune_gec[i];
                        if (gecis_bool[i] == false)
                        {
                            gecis_bool[i] = true;
                            ates_konum[i] = new Vector3(1.6f, -0.3f, 0.25f);
                            rocket_at[i] = true;
                        }
                        else
                        {
                            ates_konum[i] = new Vector3(-1.6f, -0.3f, 0.25f);
                            gecis_bool[i] = false;
                             rocket_at[i]= true;
                        }
                    }



                    if (rocket_at[i])
                    {
                        GameObject l_shootpoint = dusmanlar[i].transform.GetChild(0).gameObject;
                        l_shootpoint.transform.localPosition = ates_konum[i];
                        rocketler[rocket_sira].transform.position = l_shootpoint.transform.position;
                        rocket_trails[rocket_sira].transform.position = l_shootpoint.transform.position;
                        float max_hiz = player.transform.GetComponent<CarController>().MaxSpeed;
                        float mevcut_hiz = player.transform.GetComponent<CarController>().CurrentSpeed;
                        Vector3 yone_bak= player.transform.position + player.transform.forward * (3 - (3 - 3 * (mevcut_hiz / max_hiz)));
                        parent_rocket[rocket_sira] = dusmanlar[i];
                        rocketler[rocket_sira].transform.LookAt(yone_bak);
                        rocketler[rocket_sira].SetActive(true);
                        rocket_trails[rocket_sira].SetActive(true);
                        rocket_hiz_x[rocket_sira] = 0.0f;
                        rocket_hiz_y[rocket_sira] = 0.0f;
                        rocket_hiz_z[rocket_sira] = 0.0f;


                        rocket_sira++;
                        rocket_ilk++;
                        if ( rocket_sira== rocketsayisi)
                        {
                             rocket_sira= 0;
                        }
                        if ( rocket_ilk== rocketsayisi)
                        {
                             rocket_ilk= 0;
                        }

                        if ( rocket_ilk>= 0)
                        {

                            rocketler[rocket_ilk].SetActive(false);
                            rocket_pat[rocket_ilk].SetActive(false);
                            rocket_trails[rocket_ilk].SetActive(false);
                        rocket_hiz_x[rocket_ilk] = 0.0f;
                        rocket_hiz_y[rocket_ilk] = 0.0f;
                        rocket_hiz_z[rocket_ilk] = 0.0f;

                        }


                        rocket_at[i] = false;
                    }



                }else if (dusmanlar[i].tag == "mayinci")
                {
                    if (Time.time > gecis_vakti[i])
                    {
                        gecis_vakti[i] = Time.time + oburune_gec[i];
                        if (gecis_bool[i] == false)
                        {
                            gecis_bool[i] = true;
                            ates_konum[i] = new Vector3(0, -1.54f, 1.66f);
                             mayin_birak[i] = true;
                        }
                        else
                        {
                            ates_konum[i] = new Vector3(0, -1.54f, 1.66f);
                            gecis_bool[i] = false;
                        mayin_birak[i] = true;
                        }

                    }


                    if (mayin_birak[i] && uzaklik<10)
                    {
                        GameObject l_shootpoint = dusmanlar[i].transform.GetChild(0).gameObject;
                        l_shootpoint.transform.localPosition = ates_konum[i];
                        mayinlar[mayin_sira].transform.position = l_shootpoint.transform.position;
                        mayinlar[mayin_sira].SetActive(true);
                        mayin_hedefi[mayin_sira] = drone_konum[i].transform.position;
                        mayin_hedefi[mayin_sira] += drone_konum[i].transform.right * Random.Range(-2,3);
                        mayin_hedefi[mayin_sira].y += 0.7f;


                        mayin_sira++;
                        mayin_ilk++;
                        if (mayin_sira == mayin_sayisi)
                        {
                            mayin_sira = 0;
                        }
                        if (mayin_ilk == mayin_sayisi)
                        {
                             mayin_ilk= 0;
                        }

                        if (mayin_ilk >= 0)
                        {

                            mayinlar[mayin_ilk].SetActive(false);
                            mayin_pat[mayin_ilk].SetActive(false);

                        }



                            mayin_birak[i] = false;
                    }


                }


            }
            else
            {
                if (dusmanlar[i].tag == "drone_5")
                {
                    GameObject muzzle = dusmanlar[i].transform.GetChild(0).gameObject;
                    muzzle.SetActive(false);
                }

            }

            if (enkaz_topla[i])
            {

                if (toplamaya_basla[i] < Time.time)
                {

                    if (toplanan_drone[i] == "drone_1b") yerden_kaldir = parcali_drone1;
                    else if (toplanan_drone[i] == "drone_5") yerden_kaldir = parcali_drone5;
                    else if (toplanan_drone[i] == "drone11_c") yerden_kaldir = parcali_drone11c;
                    else if (toplanan_drone[i] == "mayinci") yerden_kaldir = parcali_mayinci;
                    else if (toplanan_drone[i] == "füzeci") yerden_kaldir = parcali_drone6;
                    foreach (Transform t in yerden_kaldir.transform)
                    {

                        t.localPosition = Vector3.zero;
                        yerden_kaldir.SetActive(false);
                    }


                    enkaz_topla[i] = false;
                }


            }



        }

        for (int i = 0; i < fuzesayisi; i++)
        {
            if (fuzeler[i].activeSelf == true)
            {

                if (Time.time > yon_zamani[i])
                {
                    yon_zamani[i] = Time.time + yon_rate;
                    onceki_pozisyon[i] = fuzeler[i].transform.position;

                }
                fuze_zamani[i] += 0.02f;
                fuze_gonder(fuzeler[i], vo[i], fuze_zamani[i], shootpoint[i]);
                Vector3 bak = fuzeler[i].transform.position - onceki_pozisyon[i];
                fuze_trails[i].transform.position = fuzeler[i].transform.position;
                if (bak != Vector3.zero) fuzeler[i].transform.rotation = Quaternion.LookRotation(bak);
                Vector3 fuze_ucu = fuzeler[i].transform.position;
                fuze_ucu.z += sensorbaslangýcý;
                RaycastHit hit;
                if (Physics.Raycast(fuze_ucu, fuzeler[i].transform.forward, out hit, fuze_sensoru_uzunlugu))
                {
                   
                    if (hit.collider.tag == "Player" )
                    {
                        fuze_pat[i].SetActive(true);
                        fuze_pat[i].transform.position = hit.point;
                        fuze_pat[i].transform.rotation = Quaternion.LookRotation(hit.normal);                   

                    }
                    else if(hit.collider.tag =="füzeci" || hit.collider.tag == "drone_5" ||hit.collider.tag == "drone_1b"||hit.collider.tag == "drone11_c")
                    {


                        return;

                    }
                    else
                    {
                        fuze_buyuk_pat[i].SetActive(true);
                        fuze_buyuk_pat[i].transform.position = hit.point;
                        fuze_buyuk_pat[i].transform.rotation = Quaternion.LookRotation(hit.normal);

                    }
                    

                    fuzeler[i].SetActive(false);
                }
                Debug.DrawRay(fuze_ucu, fuzeler[i].transform.forward * fuze_sensoru_uzunlugu, Color.green);

            }



        }

        for (int i = 0; i < laser_sayisi; i++)
        {
            if (laserler[i].activeSelf == true)
            {
                laserler[i].transform.position += laserler[i].transform.forward * 0.5f;
                Vector3 laser_ucu = laserler[i].transform.position;
                laser_ucu.z += laser_sensorbaslangýcý;
                RaycastHit hit;
                if (Physics.Raycast(laser_ucu, laserler[i].transform.forward, out hit, laser_sensoru_uzunlugu))
                {
                  //    Debug.Log("laser ile vuruldun");
                            laserler[i].SetActive(false);
                            laser_pat[i].SetActive(true);
                            laser_pat[i].transform.position = hit.point;
                            //laser_pat[i].transform.SetParent(hit.collider.transform);
                            if (hit.collider.tag == "Player")laser_pat[i].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                            else laser_pat[i].transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                }
                Debug.DrawRay(laser_ucu, laserler[i].transform.forward * laser_sensoru_uzunlugu, Color.green);

            }



        }


        for (int i = 0; i < rocketsayisi; i++)
        {
            if (rocketler[i].activeSelf == true)
            {
                /*if (rocket_hiz_x[i] < 0.9f && rocket_hiz_y[i] < 0.5f && rocket_hiz_z[i] > 0.3f){
                    Vector3 rocket_cikis = parent_rocket[i].transform.position;
                     rocket_cikis+= parent_rocket[i].transform.right * rocket_hiz_x[i];
                     rocket_cikis-= parent_rocket[i].transform.up * rocket_hiz_y[i];
                    rocketler[i].transform.position = rocket_cikis;
                    rocket_trails[i].transform.position = rocket_cikis;
                    rocket_trails[i].SetActive(false);
                    rocket_hiz_x[i] +=0.01f;
                    rocket_hiz_y[i]+=0.01f;
                    rocketler[i].transform.LookAt(player.transform);
                   Debug.Log(rocket_hiz_x[i]+"merhaba");
        }else
        {    */   
                    rocket_trails[i].SetActive(true);
               
                rocketler[i].transform.position += rocketler[i].transform.forward *rocket_hiz;
                rocket_trails[i].transform.position = rocketler[i].transform.position;
                Vector3 laser_ucu = rocketler[i].transform.position;
                laser_ucu.z += rocket_sensorbaslangýcý;
                RaycastHit hit;
                if (Physics.Raycast(laser_ucu, rocketler[i].transform.forward, out hit, rocket_sensoru_uzunlugu))
                {
                    //    Debug.Log("laser ile vuruldun");

                    if (hit.collider.tag == "drone_1b")
                    {

                        return;
                    }else if (hit.collider.tag == "Player")
                    {
                    rocketler[i].SetActive(false);
                    rocket_pat[i].SetActive(true);
                    rocket_pat[i].transform.position = hit.point;
                    //laser_pat[i].transform.SetParent(hit.collider.transform);                        
                        
                        
                        rocket_pat[i].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    }
                    else
                    {
                    rocketler[i].SetActive(false);
                    rocket_pat[i].SetActive(true);
                    rocket_pat[i].transform.position = hit.point;
                    rocket_pat[i].transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                    }
                }
                Debug.DrawRay(laser_ucu, rocketler[i].transform.forward * rocket_sensoru_uzunlugu, Color.green);

       // }

                


            }



        }

        for (int i = 0; i < mayin_sayisi; i++)
        {
            if (mayinlar[i].activeSelf == true)
            {
                mayin_hareketi(mayinlar[i], mayin_hedefi[i], 3);
             RaycastHit[] mayin_hit;
            mayin_hit = new RaycastHit[3];
            int yon=-1;

            for (int a = 0; a < 3; a++)
            {
                    if (Physics.Raycast(mayinlar[i].transform.position, Quaternion.AngleAxis(30*yon, mayinlar[i].transform.up)*mayinlar[i].transform.forward*(-1), out mayin_hit[a], 0.5f))
                    {
                        if (mayin_hit[a].collider.tag == "drone_1b")
                        {

                            return;
                        }
                        else if (mayin_hit[a].collider.tag == "Player")
                        {
                            mayinlar[i].SetActive(false);
                            mayin_pat[i].SetActive(true);
                            mayin_pat[i].transform.position =mayinlar[i].transform.position ;
                            //laser_pat[i].transform.SetParent(hit.collider.transform);                        


                            mayin_pat[i].transform.localScale = new Vector3(0.5f,0.5f,0.5f);
                        }

                    }
                    Debug.DrawRay(mayinlar[i].transform.position, Quaternion.AngleAxis(30 * yon, mayinlar[i].transform.up) * mayinlar[i].transform.forward* -0.5f, Color.green);

                    yon++;

                }           
            
            }





        }

        ///////////////////düþman sayýsý///////////////////
        ///
        if (aktif_dusman_sayisi == 0)
        {
            if (dusman_sirasi < dusman_seviyesi.Length) { 
            diger_dusmana_gec = true;

            aktif_dusman_sayisi = dusman_seviyesi[dusman_sirasi];


            
            sonraki_sira = onceki_sira + aktif_dusman_sayisi;
                if (sonraki_sira > dusmanlar.Length) sonraki_sira = dusmanlar.Length;
            obur_dusmana_gecis_vakti=Time.time+ Random.Range(5.0f, 10.0f);
                dusman_sirasi++;
            }

        }
        if (diger_dusmana_gec) {
            if (obur_dusmana_gecis_vakti < Time.time) {
            for(int i = onceki_sira; i < sonraki_sira; i++)
            {
             dusmanlar[i].SetActive(true);
                    drone_cizgi_sirasi[i] = cizgi_sirasi[2];
                    drone_cizgi[i] = cizgiler[drone_cizgi_sirasi[i]];
                    drone_sirasi[i] =konum_sirasi[2];
                    dusmanlar[i].transform.position = cube[2].transform.position;
                    
                    if (Mathf.Abs(gecici_konum[i]-drone_sirasi[i])< 10.0f)
                {
                    oteleme_dur_kalk = otelenme;

                    }
                     else
                     {
                     oteleme_dur_kalk = 0;
                    }

                }
                onceki_sira += aktif_dusman_sayisi;
                

            diger_dusmana_gec = false;
            dusman_hizi = 3;

            }



        }


 }

    private void fuze_gonder(GameObject fuze, Vector3 distance, float zaman,Vector3 shootpoint)
    {
        Vector3 vek = distance;

        float x = vek.x;
        float y = vek.y;
        float z = vek.z;

        fuze.transform.position = shootpoint;
        float Vx = x * zaman;
        float Vy = (y * zaman - 0.5f * Mathf.Abs(Physics.gravity.y) * (zaman * zaman));
        float Vz = z * zaman;
        Vector3 havadan_git = new Vector3(Vx, Vy, Vz);
        fuze.transform.position = shootpoint + havadan_git;

    }

    private Vector3 Calculate_velocity(Vector3 target, Vector3 origin, float time)
    {

        Vector3 distance = target - origin;
        Vector3 distancexz = distance;


        float sy = distance.y;
        float sxz = distancexz.magnitude;

        float Vxz = sxz / time;
        float Vy = sy / time + .5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distancexz.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
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

     private void Drone_hareketi(GameObject obje,GameObject hedef,float hiz,float sol_sag,float in_cik)
     {
         Vector3 koordinat;

         koordinat = hedef.transform.position;
         koordinat += hedef.transform.right * sol_sag;
        koordinat +=hedef.transform.up * in_cik;



       float  x = Mathf.Lerp(obje.transform.position.x, koordinat.x, Time.deltaTime * hiz);
       float  y = Mathf.Lerp(obje.transform.position.y, koordinat.y, Time.deltaTime * hiz);
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

    private void mayin_hareketi(GameObject obje, Vector3 hedef, float hiz)
    {
        Vector3 koordinat;

        koordinat = hedef;



        float x = Mathf.Lerp(obje.transform.position.x, koordinat.x, Time.deltaTime * hiz);
        float y = Mathf.Lerp(obje.transform.position.y, koordinat.y, Time.deltaTime * hiz);
        float z = Mathf.Lerp(obje.transform.position.z, koordinat.z, Time.deltaTime * hiz);

        obje.transform.position = new Vector3(x, y, z);

    }

}
