using System;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;


#pragma warning disable 649
//namespace UnityStandardAssets.Vehicles.Car{
    internal enum CarDriveType
    {
        FrontWheelDrive,
        RearWheelDrive,
        FourWheelDrive
    }


    public class CarController : MonoBehaviour
    {
    #region ARABANIN UNÝTY ASSET ORÝJÝNAL KODLARI
        [SerializeField] private CarDriveType m_CarDriveType = CarDriveType.FourWheelDrive;
  

        [SerializeField] private WheelCollider[] m_WheelColliders = new WheelCollider[4];
        [SerializeField] private GameObject[] m_WheelMeshes = new GameObject[4];
        [SerializeField] private Vector3 m_CentreOfMassOffset;
        [SerializeField] private float m_MaximumSteerAngle;
        // 0 is raw physics , 1 the car will grip in the direction it is facing
        [Range(0, 1)] [SerializeField] private float m_TractionControl; // 0 is no traction control, 1 is full interference
        [SerializeField] private float m_FullTorqueOverAllWheels;
        [SerializeField] private float m_ReverseTorque;
        [SerializeField] private float m_Downforce = 100f;
        [SerializeField] private float m_Topspeed = 200;
        [SerializeField] private static int NoOfGears = 5;
        [SerializeField] private float m_RevRangeBoundary = 1f;



        private Quaternion[] m_WheelMeshLocalRotations;
        private Vector3 m_Prevpos, m_Pos;
        private float m_SteerAngle;
        private int m_GearNum;
        private float m_GearFactor;
        private float m_OldRotation;
        private float m_CurrentTorque;
        private Rigidbody m_Rigidbody;
        private const float k_ReversingThreshold = 0.01f;
        private float donmeal;
        private CarUserControl kontrolet;
        private float hiza_basili, donmeye_basili;
        private float m_BrakeTorque;


        public bool Skidding { get; private set; }
        public float BrakeInput { get; private set; }
        public float CurrentSteerAngle { get { return m_SteerAngle; } }
        public float CurrentSpeed { get { return m_Rigidbody.velocity.magnitude * 2.23693629f; } }
        public float MaxSpeed { get { return m_Topspeed; } }
        public float Revs { get; private set; }
        public float AccelInput { get; private set; }



    #endregion





    #region ÜÇÜNCÜNÜN ROKETÝ

    [Header("ROKET AYARLARI")]
    //ROKET ÝLE ÝLGÝLÝ KODLAR

    GameObject[] mermiler;// mermiler dizisi oluþturur
        GameObject[] roket_trails;
        GameObject[] patlama;
        GameObject[] kucuk_pat;
        private GameObject bullet;// çoðaltýlacak füze
        private GameObject roketatesi;
        private GameObject pat;
        private GameObject shootpoint;// füze çýkýþ moktasý
        public int mermisayisi;// aktif olacak füze sayýsý
        public int sira;// aktif füze sýrasý
        public int ilk;//
        public bool ates;// ateþ tuþuna basýldýðýnda true deðerini almasý için
        public float timetofire = 0;// ATIÞ ZAMANI
        public float firerate;//birim zamandaki füze sayýsý
        [SerializeField] private float speed;

        [SerializeField] private float sensorbaslangýcý;
        [SerializeField] private float fuze_sensoru_uzunlugu;


    /// ////////////// FÜZE SESÝ ÝLE ÝLGÝLÝ ELEMANLAR
    private GameObject fuzeSesi;
   





    #endregion

    #region ÖZEL GRAVÝTY AYARLARI

    //ARABANIN UÇABÝLMESÝ ÝÇÝN GRAVÝTY AYARLARI
    [Header("GRAVÝTY AYARLARI")]   
        
        [SerializeField] private float azaltma=0.5f;
        [SerializeField] private float globalGravity = -9.81f;
        private Rigidbody m_rb;
        private float gravityScale;



    #endregion

    public RaycastHit hit;
    public bool vuruldu;
    public string vurulan;




    private GameObject kucuk_patlama;



    /*
    [Header("ön sensör ayarlarý")]
    [SerializeField] private float on_sensor_uzunlugu;
    public bool ileri_git;

    */
    #region SU SIÇRAMASI ÝLE ÝLGLÝ KISIM

    ///    SU SIÇRATMA/////////////
    [Header("su sýçratma ayarlarý")]
    GameObject[] su_sicratma;
    private GameObject sicratma;
    private int ilk_su, su_sira, su_sayisi = 3;
    public bool suya_dokundu=false;



    #endregion


    [Header("patlama sesi")]
    public GameObject patlama_sesi;
    




    /// /////////arabanýn patlmasý  ///////////
    private GameObject yangin, araba_patlama;
    public bool fail;


    /// /////////öne eðim verme  ///////////
    private float max_egim = 14,mevcut_egim;

    /// /////////araba çeþidi ayarý ///////////
    private string araba;




    #region BÝRÝNCÝ ATEÞ AYARLARI
    private GameObject taramali_sesi , taramali_bitisi;
    private float timetobullet, bulletrate=20.0f;
    private GameObject bullet_trace;
    private GameObject[] siluetler;
    private int siluet_sayisi = 20, ilk_siluet, sira_siluet;
    private float menzil_uzunlugu=30;
    public GameObject kivilcim_ana;
    private GameObject[] kivilcimlar;
    private int kivilcim_sayisi = 10, ilk_kivilcim, son_kivilcim;


    #endregion


    #region LAZERAYARLARI

    [Header("LAZER AYARLARI")]

    // LAZER AYARLARI
    public float lazer_mesafesi;
    public GameObject lr;
    public GameObject lazer_yakan;
    private GameObject[] lazeratesler;
    private int l_ates_sayisi = 70, ilk_lazer_ates, son_lazer_ates;
    private float lazeruzunluðu;
    public GameObject degen;
    private GameObject laserbas, laserdevam, laserbitis; ////////// LASER SESLERÝ
    private  float laserDevamZamani;



    #endregion


    //kursun  ayarlarý
    #region KURÞUN AYARLARI




    [Header("KURÞUN AYARLARI")]


    GameObject[] kursunlar;// kursunlar dizisi oluþturur
    public int kursunsayisi;// aktif olacak füze sayýsý
    private GameObject kursun;
    private int kursun_sirasi, kursun_ilk;
    public float kursun_sensoru_uzunlugu;
    public GameObject kursuntara, kursunbitis;



    #endregion

    // Use this for initialization
    private void Start()
        {


        #region  RESOURCELAR

        yangin= Resources.Load<GameObject>("patlamalar/yangýn");
        araba_patlama= Resources.Load<GameObject>("patlamalar/BigExplosion");
        lazer_yakan= Resources.Load<GameObject>("lazer_yakan");

        #endregion


        yangin = Instantiate(yangin, transform.position, Quaternion.identity);
        yangin.SetActive(false);


        araba_patlama = Instantiate(araba_patlama, transform.position, Quaternion.identity);
        araba_patlama.SetActive(false);



        araba = transform.name;


        

          shootpoint = GameObject.FindGameObjectWithTag("atisnoktasi");

        if(araba=="ücüncü prototip")
        {


            #region ÜÇÜNCÜNÜN BAÞLANGIÇ AYARLARI

            azaltma = 0.84f;

            mermiler = new GameObject[mermisayisi];
            roket_trails = new GameObject[mermisayisi];
            patlama = new GameObject[mermisayisi];
            kucuk_pat = new GameObject[mermisayisi];
            bullet = Resources.Load<GameObject>("füze/missile");
            pat=Resources.Load<GameObject>("patlamalar/patlama1");
            roketatesi=Resources.Load<GameObject>("patlamalar/roket_trail");
            kucuk_patlama=Resources.Load<GameObject>("patlamalar/küçük patlama");
            for (int i = 0; i < mermisayisi; i++)
            {
                mermiler[i] = Instantiate(bullet, shootpoint.transform.position, Quaternion.identity);
                roket_trails[i] = Instantiate(roketatesi, shootpoint.transform.position, Quaternion.identity);
                patlama[i] = Instantiate(pat, shootpoint.transform.position, Quaternion.identity);
                kucuk_pat[i] = Instantiate(kucuk_patlama, shootpoint.transform.position, Quaternion.identity);
                //mermiler[i].tag = "roket" + i;
                mermiler[i].SetActive(false);
                roket_trails[i].SetActive(false);
                patlama[i].SetActive(false);
                kucuk_pat[i].SetActive(false);
            }
            sira = 0;
            ilk = sira - (mermisayisi - 1);



            fuzeSesi = transform.GetChild(1).gameObject;





            #endregion

        }
        else if(araba=="birinci_prototip")
        {


            #region BÝRÝNCÝNÝN BAÞLANGIÇ AYARLARI

            azaltma = 0.86f;
            taramali_sesi = transform.GetChild(6).gameObject;
            taramali_bitisi = transform.GetChild(7).gameObject;

            taramali_bitisi.SetActive(false);
            siluetler = new GameObject[siluet_sayisi];
            bullet_trace= Resources.Load<GameObject>("bullet trance/bullet trace");
            for (int i = 0; i < siluet_sayisi; i++)
            {


                siluetler[i] = GameObject.Instantiate(bullet_trace, transform.position, Quaternion.identity);
                siluetler[i].SetActive(false);

            }
            sira_siluet = 0;
            ilk_siluet = sira_siluet - (siluet_sayisi - 1);
            timetobullet = Time.time + 1.0f / bulletrate;

            ////////////////////////////////////////////

            son_kivilcim = 0;
            ilk_kivilcim = son_kivilcim - (kivilcim_sayisi - 1);

            kivilcimlar = new GameObject[kivilcim_sayisi];

            for (int i = 0; i < kivilcim_sayisi; i++)
            {
                kivilcimlar[i] = Instantiate(kivilcim_ana, transform.position, Quaternion.identity);
                kivilcimlar[i].SetActive(false);
            }


            #endregion


        }
        else if(araba== "ikinci_prototip")
        {


            #region ÝKÝCÝNÝN BAÞLANGIÞ AYARLARI


           

            azaltma = 0.84f;
            // lazer ile ilgili ayarlar
            lr.SetActive(false);
            degen.SetActive(false);


            son_lazer_ates = 0;
            ilk_lazer_ates = son_lazer_ates - (l_ates_sayisi - 1);

            lazeratesler = new GameObject[l_ates_sayisi];

            for (int i = 0; i < l_ates_sayisi; i++)
            {
                lazeratesler[i] = Instantiate(lazer_yakan, transform.position, Quaternion.identity);
                lazeratesler[i].SetActive(false);
            }

            lazer_yakan.SetActive(false);

            ////////////////////SES ÝLE ÝLGÝLÝ KISIM

            laserbas = transform.GetChild(6).gameObject;
            laserdevam = transform.GetChild(7).gameObject;
            laserbitis = transform.GetChild(8).gameObject;
            laserDevamZamani = Time.time;




            #endregion




        }
        else if (araba== "dördüncü_prototip")
        {


            #region DÖRDÜNCÜ BAÞLANGIÇ AYARLA
            azaltma = 0.84f;

            kursuntara= transform.GetChild(6).gameObject;
            kursunbitis= transform.GetChild(6).gameObject;
            kucuk_patlama = Resources.Load<GameObject>("patlamalar/küçük patlama");
            kursun = Resources.Load<GameObject>("lazer");
            kursunlar = new GameObject[kursunsayisi];
            kucuk_pat = new GameObject[kursunsayisi];

            for (int i = 0; i < kursunsayisi; i++)
            {
                kursunlar[i] = Instantiate(kursun, shootpoint.transform.position, Quaternion.identity);
                kucuk_pat[i] = Instantiate(kucuk_patlama, shootpoint.transform.position, Quaternion.identity);
                //kursunlar[i].tag = "roket" + i;
                kursunlar[i].SetActive(false);
                kucuk_pat[i].SetActive(false);
            }

            kursun_sirasi = 0;
            kursun_ilk = kursun_sirasi - (kursunsayisi - 1);
            #endregion

        }


        Application.targetFrameRate = 60;

        #region LASTÝKLERÝ ATAMA AYARI


        m_WheelMeshLocalRotations = new Quaternion[4];

        m_WheelMeshes[0] = GameObject.Find("fr");
        m_WheelMeshes[1] = GameObject.Find("fl");
        m_WheelMeshes[2] = GameObject.Find("rl");
        m_WheelMeshes[3] = GameObject.Find("rr");

        m_WheelColliders[0] = GameObject.Find("fr (1)").transform.GetComponent<WheelCollider>();
        m_WheelColliders[1] = GameObject.Find("fl (1)").transform.GetComponent<WheelCollider>();
        m_WheelColliders[2] = GameObject.Find("rl (1)").transform.GetComponent<WheelCollider>();
        m_WheelColliders[3] = GameObject.Find("rr (1)").transform.GetComponent<WheelCollider>();




        #endregion





        for (int i = 0; i < 4; i++)
            {
                m_WheelMeshLocalRotations[i] = m_WheelMeshes[i].transform.localRotation;

            }
            m_WheelColliders[0].attachedRigidbody.centerOfMass = m_CentreOfMassOffset;

            m_Rigidbody = GetComponent<Rigidbody>();
            m_CurrentTorque = m_FullTorqueOverAllWheels - (m_TractionControl * m_FullTorqueOverAllWheels);
            kontrolet = GetComponent<CarUserControl>();

            m_rb = transform.GetComponent<Rigidbody>();



            //FÜZE KODLARI
        #region SIÇRAYAN SU BAÞLANGIÇ AYARI

        sicratma= Resources.Load<GameObject>("su_sicrama/susýcrama_efek (1)");
        su_sicratma = new GameObject[su_sayisi];
        for (int i = 0; i < su_sayisi; i++)
        {
            su_sicratma[i] = Instantiate(sicratma, transform.position, Quaternion.identity);
            su_sicratma[i].SetActive(false);

        }
        su_sira = 0;
        ilk_su = su_sira - (su_sayisi - 1);



        #endregion

       // araba_patlama= GameObject.FindGameObjectWithTag("araba_patlama");
      //  yangin = GameObject.FindGameObjectWithTag("yangýn");

        araba_patlama.SetActive(false);
        yangin.SetActive(false);
        fail = false;
        
    }











    private void Update()
        {
        
        donmeal = kontrolet.donme;
            m_MaximumSteerAngle = donmeal;
        donmeye_basili = kontrolet.h;
        hiza_basili = kontrolet.v;

        // if (Input.GetKeyDown(KeyCode.K)) transform.position = baslangic;
        float yeni_azaltma;

        if (donmeye_basili != 0) yeni_azaltma = 0.7f;
        else yeni_azaltma = azaltma;


        mevcut_egim = transform.localEulerAngles.x;














        //////////////////////////////////////////// //ARABA ÝÇÝN ÖZEL GRAVÝTY OLUÞTURUYORUZ
        #region ÖZEL YERÇEKÝMÝ AYARLARI


        gravityScale = 1 - yeni_azaltma * (CurrentSpeed / (MaxSpeed+donmeye_basili));
            Vector3 gravity = globalGravity * gravityScale * Vector3.up;
           m_rb.AddForce(gravity, ForceMode.Acceleration);
            m_rb.drag=0.5f-(0.5f-0.01f)* (CurrentSpeed / (MaxSpeed+donmeye_basili));
        if (mevcut_egim > max_egim) mevcut_egim = 0;



            m_rb.AddForceAtPosition(Vector3.up * (-1) * (80-80*(mevcut_egim/max_egim)), transform.position + transform.forward * 2.5f);


        if ((transform.rotation.x<-0.002f || transform.rotation.x>0.002f) && CurrentSpeed<10)
            {
                m_rb.drag= 0.01f;
                globalGravity = -2;


            }
            else
            {
                globalGravity = -9.81f;

            }

            Vector3 local_velocity = transform.InverseTransformDirection(transform.GetComponent<Rigidbody>().velocity);

            if(local_velocity.z< -1 && kontrolet.v>0)
            {
                Vector3 ileri = globalGravity * gravityScale * transform.forward * (-2);
                m_rb.AddForce(ileri, ForceMode.Acceleration);

            }
        #endregion

        //////////////////////////////////////////// //ARABA ÝÇÝN ÖZEL GRAVÝTY OLUÞTURUYORUZ










        /////////////////////////////ANTÝ STUCK SÝSTEMÝ////////////////////////////////////////////



        #region ARABANIN TAKILMAMASI ÝÇÝN DESTEK KODLARI


        if ((hiza_basili==1 || hiza_basili==-1) && CurrentSpeed < 3.5f) {
            Vector3 dikey_destek = globalGravity * gravityScale * transform.forward * (-5)*hiza_basili;
            m_rb.AddForce(dikey_destek, ForceMode.Acceleration);
            }
           if((donmeye_basili==1 || donmeye_basili==-1) && CurrentSpeed < 3.5f) {
            Vector3 yatay_destek = globalGravity * gravityScale * transform.right * (-5) * donmeye_basili;
            m_rb.AddForce(yatay_destek, ForceMode.Acceleration);
            }


 #endregion




        ///////////////////////////////ANTÝ STUCK SÝSTEMÝ//////////////////////////////////////









        //////////////////////////////////////////////////ARABA ATEÞ ETME AYARLARI
        if (kontrolet.control_degis == false) { 



        if (Input.GetMouseButton(0))
            {
                ates = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                ates = false;
            }
        }
        else
        {


        }




     



        if (araba=="ücüncü prototip") {



        #region ÜÇÜNCÜ ARABA ATEÞ AYARI


            if (ates && Time.time >= timetofire && fail==false)
            {


                #region GÖRLSEL KISIM
                timetofire = Time.time + 1.0f / firerate;
                mermiler[sira].transform.position = shootpoint.transform.position;
                roket_trails[sira].transform.position = shootpoint.transform.position;
                mermiler[sira].transform.rotation = shootpoint.transform.rotation;
                mermiler[sira].SetActive(true);
                roket_trails[sira].SetActive(true);








                sira++;
                ilk++;
                if (sira == mermisayisi)
                {
                    sira = 0;
                }
                if (ilk == mermisayisi)
                {
                    ilk = 0;
                }

                if (ilk >= 0)
                {

                    mermiler[ilk].SetActive(false);
                    roket_trails[ilk].SetActive(false);
                    patlama[ilk].SetActive(false);
                    kucuk_pat[ilk].SetActive(false);
                    
                    


                }
                #endregion



                #region SES KISMI
   
                fuzeSesi.GetComponent<AudioSource>().Play(0);

                #endregion





            }

            //FÜZE HAREKETLERÝ
            for (int i =0; i < mermisayisi; i++)
            {

                if (mermiler[i].activeSelf==true) {
                    mermiler[i].transform.position += mermiler[i].transform.forward * (speed * Time.deltaTime);
                    roket_trails[i].transform.position = mermiler[i].transform.position;

                Vector3 fuze_ucu = mermiler[i].transform.position;
                fuze_ucu.z += sensorbaslangýcý;



                if(Physics.Raycast(fuze_ucu,mermiler[i].transform.forward,out hit, fuze_sensoru_uzunlugu))
                {
                    vuruldu = true;
                    vurulan = hit.collider.name;
                    string carpma = hit.collider.tag;
                   

                    if(carpma=="füzeci" || carpma=="drone_5" || carpma == "drone_1b" || carpma == "drone_11c")
                    {
                        kucuk_pat[i].SetActive(true);
                        kucuk_pat[i].transform.position = hit.point;
                        kucuk_pat[i].transform.rotation = Quaternion.LookRotation(hit.normal);
                    }
                    else { 
                    //Debug.Log("çarpma" + i);
                    patlama[i].SetActive(true);
                    patlama[i].transform.position = hit.point;
                    patlama[i].transform.rotation = Quaternion.LookRotation(hit.normal);

                       }

                    mermiler[i].SetActive(false);
                        patlama_sesi.transform.GetComponent<AudioSource>().Play(0);

                }
                Debug.DrawRay(fuze_ucu,mermiler[i].transform.forward*fuze_sensoru_uzunlugu,Color.green);
                
                 }
                



            } 
           #endregion



        }else if (araba=="birinci_prototip") {

            #region BÝRÝNCÝ ARABA ATEÞ AYARI
            if (ates==true && fail==false)
            {
                    #region GÖRSEL KISIM
                GameObject fisek = transform.GetChild(5).gameObject;

                fisek.SetActive(true);
                Vector3 sapma;
                sapma=new Vector3(UnityEngine.Random.Range(0.0f, 0.5f), UnityEngine.Random.Range(0.0f, 0.9f),UnityEngine.Random.Range(0.0f, 0.9f));
                Vector3 sapma2=new Vector3(UnityEngine.Random.Range(0.0f, 0.2f), UnityEngine.Random.Range(0.0f, 0.2f),UnityEngine.Random.Range(0.0f, 0.2f));


                RaycastHit bullet_hit;               


                if (Time.time >= timetobullet)
                {



                    
                    siluetler[sira_siluet].SetActive(true);
                    siluetler[sira_siluet].transform.position = fisek.transform.position+sapma2;
                    siluetler[sira_siluet].transform.rotation = fisek.transform.rotation;

                    sira_siluet++;
                    ilk_siluet++;
                    if (sira_siluet == siluet_sayisi)
                    {
                        sira_siluet = 0;
                    }
                    if (ilk_siluet == siluet_sayisi)
                    {
                        ilk_siluet = 0;
                    }

                    if (ilk_siluet >= 0)
                    {

                        siluetler[ilk_siluet].SetActive(false);


                    }




                    if (Physics.Raycast(fisek.transform.position+sapma,fisek.transform.forward, out bullet_hit,menzil_uzunlugu))
                    {



                        kivilcimlar[son_kivilcim].SetActive(true);
                        kivilcimlar[son_kivilcim].transform.position = bullet_hit.point;
                        kivilcimlar[son_kivilcim].transform.rotation = Quaternion.LookRotation(bullet_hit.normal);
                        kivilcimlar[son_kivilcim].transform.SetParent(bullet_hit.collider.transform);
                        if (bullet_hit.collider.tag != "Untagged")
                        {
                            vuruldu = true;
                            vurulan = bullet_hit.collider.name;
                            kivilcimlar[son_kivilcim].transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);


                        }
                        else
                        {
                            kivilcimlar[son_kivilcim].transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                        }

                        son_kivilcim++;
                        ilk_kivilcim++;
                        if (son_kivilcim == kivilcim_sayisi)
                        {

                            son_kivilcim = 0;
                        }
                        if (ilk_kivilcim == kivilcim_sayisi)
                        {

                            ilk_kivilcim = 0;
                        }
                        if (ilk_kivilcim >= 0)
                        {
                            kivilcimlar[ilk_kivilcim].SetActive(false);

                        }




                    }
                    Debug.DrawRay(fisek.transform.position + sapma, fisek.transform.forward*menzil_uzunlugu , Color.green);





                    
                    timetobullet = Time.time + 1.0f / bulletrate;
                   /* bullet_trace.SetActive(true);
                    bullet_trace.transform.localScale = new Vector3(-0.05f, -0.05f, -7.5f) * buyukluk;*/
                }
                else
                {
                   // bullet_trace.SetActive(false);

                }

                #endregion

                //////////////////////ATEÞ SESÝ///////////////////
               

                #region SES KISMI
                if (taramali_sesi.activeSelf == false)
                {                
                    taramali_bitisi.SetActive(false);
                    taramali_sesi.SetActive(true);
                taramali_sesi.GetComponent<AudioSource>().Play(0);

                }
                #endregion
            }
            else
            {
                #region TÜFEK VE MUZZLE BÝTÝÞÝ
                

               
                transform.GetChild(5).gameObject.SetActive(false);
                       taramali_sesi.SetActive(false);

                if (taramali_bitisi.activeSelf == false) { 
                taramali_bitisi.SetActive(true);
                taramali_bitisi.GetComponent<AudioSource>().Play(0);
                    }
     
                #endregion          
            }


            #region TÜFEKTEN ÇIKAN SÝLÜET EFEK AYARI
            for (int i = 0; i < siluetler.Length; i++)
            {
                if (siluetler[i].activeSelf == true)
                {

                    siluetler[i].transform.position += siluetler[i].transform.forward * UnityEngine.Random.Range(100, 150) * Time.deltaTime;


                }
            } 
            #endregion


            
            
            #endregion




        }else if (araba == "ikinci_prototip")
        {

            #region ÝKÝNÝ ARABA ATEÞ AYARI

            



            if (ates == true && fail == false)
            {
                #region GÖRSEL KISIM
                lr.SetActive(true);
                lr.transform.localScale = new Vector3(lr.transform.localScale.x, lr.transform.localScale.y, lazeruzunluðu);
                lr.transform.position = shootpoint.transform.position;
                lr.transform.rotation= shootpoint.transform.rotation;
                Vector3 namlu = shootpoint.transform.position;//ýþýn baþlangýþ noktasý
                RaycastHit laser_hit;//ýþýn çarpmasý belirlenecek
                if (Physics.Raycast(namlu, shootpoint.transform.forward, out laser_hit, lazer_mesafesi))
                {

                    lazeruzunluðu =(0.5f+Vector3.Distance(shootpoint.transform.position, laser_hit.point)) * 0.25f;

                    lr.transform.localScale = new Vector3(lr.transform.localScale.x, lr.transform.localScale.y,lazeruzunluðu);
                   

                    lazer_yakan.SetActive(true);
                    lazer_yakan.transform.position = laser_hit.point;

                    lazeratesler[son_lazer_ates].SetActive(true);
                    lazeratesler[son_lazer_ates].transform.position = laser_hit.point;
                    lazeratesler[son_lazer_ates].transform.rotation = Quaternion.LookRotation(laser_hit.normal);
                    lazeratesler[son_lazer_ates].transform.SetParent(laser_hit.collider.transform);


                    degen.SetActive(true);

                    degen.transform.position = laser_hit.point;
                    degen.transform.rotation = Quaternion.LookRotation(laser_hit.normal);
                    degen.transform.position = degen.transform.position + degen.transform.forward * 0.1f;




                    if (laser_hit.collider.tag != "Untagged")
                    {
                        vuruldu = true;
                        vurulan = laser_hit.collider.name;
                        lazeratesler[son_lazer_ates].transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);


                    }
                    else
                    {

                        lazeratesler[son_lazer_ates].transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                    }



                    son_lazer_ates++;
                    ilk_lazer_ates++;
                    if (son_lazer_ates == l_ates_sayisi)
                    {

                        son_lazer_ates = 0;
                    }
                    if (ilk_lazer_ates == l_ates_sayisi)
                    {

                        ilk_lazer_ates = 0;
                    }
                    if (ilk_lazer_ates >= 0)
                    {
                        lazeratesler[ilk_lazer_ates].SetActive(false);

                    }



                    }
                    else
                {

                    lazeruzunluðu += 0.5f;
                    degen.SetActive(false);

                }
                Debug.DrawRay(namlu, shootpoint.transform.forward * lazer_mesafesi, Color.green);
                #endregion


                #region SES KISMI
                
 

                    if (laserbas.activeSelf==false)
                    {
                    laserDevamZamani = Time.time + laserbas.transform.GetComponent<AudioSource>().clip.length;
                    laserbitis.SetActive(false);
                    laserbas.SetActive(true);
                    laserbas.GetComponent<AudioSource>().Play(0);



                    }

                    if (laserDevamZamani<Time.time && laserdevam.activeSelf==false)
                    {


                    laserdevam.SetActive(true);
                    laserdevam.GetComponent<AudioSource>().Play(0);

                    }
                #endregion

            }
            else
            {
                #region LAZER ATEÞÝ BÝTÝNCE OLACAKLAR
                lr.SetActive(false);
                lazer_yakan.SetActive(false);
                lazeruzunluðu = 0;
                degen.SetActive(false);

                ////////////////SES BÝTÝÞÝ
                if (laserbitis.activeSelf == false)
                {
                    laserbas.SetActive(false);
                    laserdevam.SetActive(false);

                    laserbitis.SetActive(true);
                    laserbitis.GetComponent<AudioSource>().Play(0);
                }

                #endregion

            }


            #endregion





        }
        else if(araba== "dördüncü_prototip")
        {


            #region DÖRDÜNCÜ ARABA  ATEÞ AYARI

           



            if (ates && Time.time >= timetofire && fail == false)
            {
                timetofire = Time.time + 1.0f / firerate;
                kursunlar[kursun_sirasi].transform.position = shootpoint.transform.position;
                kursunlar[kursun_sirasi].transform.rotation = shootpoint.transform.rotation;
                kursunlar[kursun_sirasi].SetActive(true);
                kursun_sirasi++;
                kursun_ilk++;
                if (kursun_sirasi == kursunsayisi)
                {
                    kursun_sirasi = 0;
                }
                if (kursun_ilk == kursunsayisi)
                {
                    kursun_ilk = 0;
                }

                if (kursun_ilk >= 0)
                {

                    kursunlar[kursun_ilk].SetActive(false);
                    kucuk_pat[kursun_ilk].SetActive(false);




                }


                #region SES KISMI
                if (kursuntara.activeSelf == false)
                {
                    kursunbitis.SetActive(false);
                    kursuntara.SetActive(true);
                    kursuntara.GetComponent<AudioSource>().Play(0);

                }
                #endregion



            }
            else
            {
                #region SES bitiþi
                    kursunbitis.SetActive(true);
                    kursuntara.SetActive(false);
                    kursunbitis.GetComponent<AudioSource>().Play(0);

                #endregion



            }


                #region dördüncü kurþun hareketi
            for (int i = 0; i < kursunsayisi; i++)
            {

                if (kursunlar[i].activeSelf == true)
                {
                    kursunlar[i].transform.position += kursunlar[i].transform.forward * (speed * Time.deltaTime);

                    Vector3 kursun_ucu = kursunlar[i].transform.position;
                    kursun_ucu.z += sensorbaslangýcý;



                    if (Physics.Raycast(kursun_ucu, kursunlar[i].transform.forward, out hit, kursun_sensoru_uzunlugu))
                    {
                        vuruldu = true;
                        vurulan = hit.collider.name;

                        kucuk_pat[i].SetActive(true);
                        kucuk_pat[i].transform.position = hit.point;
                        kucuk_pat[i].transform.rotation = Quaternion.LookRotation(hit.normal);


                        kursunlar[i].SetActive(false);

                    }
                    Debug.DrawRay(kursun_ucu, kursunlar[i].transform.forward * kursun_sensoru_uzunlugu, Color.green);

                }


            }



                #endregion




 #endregion









            }


        #region ARABANIN SUYA BATMASI DURUMU AYARLARI

        


        RaycastHit[] su_hit;
        bool[] su;
        float[] su_sensoru;
        //bool[] havada;
        su_hit = new RaycastHit[3];
        su = new bool[3];
        su_sensoru= new float[3];
        //havada = new bool[4];
        su_sensoru[0] = -0.5f;        
        su_sensoru[1] = 0;
        su_sensoru[2] = 0.5f; 
        /* */
        for (int i = 0; i < su_hit.Length; i++)
        {
            Vector3 direction = new Vector3(0,-1,0);




            if (Physics.Raycast(transform.position + transform.forward * su_sensoru[i], direction*0.2f, out su_hit[i], 2f))
            {

                if (su_hit[i].collider.tag == "su" )
                {
                    su[i] = true;


                }
                else
                {
                    su[i] = false;

                }

                if (su[i])
                {
                    
                    if (su_sicratma[i].activeSelf == false) { 
                    su_sicratma[i].transform.position = su_hit[i].point;
                    sicratma.transform.position = su_hit[i].point;
                    su_sicratma[i].transform.rotation = Quaternion.LookRotation(su_hit[i].normal); ; 
                    su_sicratma[i].SetActive(true);
                    suya_dokundu = true;

                    }


                }
                  
            }


            Debug.DrawRay(transform.position + transform.forward * su_sensoru[i], direction * 0.2f, Color.green);


        }
        #endregion







        /////////////////////////////////////////////////////ARABA PATLAMASI
       


        #region ARABA PATLAMA AYARLARI

      

        if (Input.GetKeyDown(KeyCode.L)) fail = true;

        if (fail == true)
        {
            yangin.SetActive(true);
            araba_patlama.SetActive(true);
        yangin.transform.position = transform.position +transform.forward*(-0.2f)+transform.up*0.1f ;
        araba_patlama.transform.position = transform.position;
            kontrolet.carpat = true;
        }   
  #endregion




 /////////////////////////////////////////////////////ARABA PATLAMASI





    }


















































        private void GearChanging()
        {
            float f = Mathf.Abs(CurrentSpeed / MaxSpeed);
            float upgearlimit = (1 / (float)NoOfGears) * (m_GearNum + 1);
            float downgearlimit = (1 / (float)NoOfGears) * m_GearNum;

            if (m_GearNum > 0 && f < downgearlimit)
            {
                m_GearNum--;
            }

            if (f > upgearlimit && (m_GearNum < (NoOfGears - 1)))
            {
                m_GearNum++;
            }
        }


        // simple function to add a curved bias towards 1 for a value in the 0-1 range
        private static float CurveFactor(float factor)
        {
            return 1 - (1 - factor) * (1 - factor);
        }


        // unclamped version of Lerp, to allow value to exceed the from-to range
        private static float ULerp(float from, float to, float value)
        {
            return (1.0f - value) * from + value * to;
        }


        private void CalculateGearFactor()
        {
            float f = (1 / (float)NoOfGears);
            // gear factor is a normalised representation of the current speed within the current gear's range of speeds.
            // We smooth towards the 'target' gear factor, so that revs don't instantly snap up or down when changing gear.
            var targetGearFactor = Mathf.InverseLerp(f * m_GearNum, f * (m_GearNum + 1), Mathf.Abs(CurrentSpeed / MaxSpeed));
            m_GearFactor = Mathf.Lerp(m_GearFactor, targetGearFactor, Time.deltaTime * 5f);
        }


        private void CalculateRevs()
        {
            // calculate engine revs (for display / sound)
            // (this is done in retrospect - revs are not used in force/power calculations)
            CalculateGearFactor();
            var gearNumFactor = m_GearNum / (float)NoOfGears;
            var revsRangeMin = ULerp(0f, m_RevRangeBoundary, CurveFactor(gearNumFactor));
            var revsRangeMax = ULerp(m_RevRangeBoundary, 1f, gearNumFactor);
            Revs = ULerp(revsRangeMin, revsRangeMax, m_GearFactor);
        }


        public void Move(float steering, float accel, float footbrake)
        {
            for (int i = 0; i < 4; i++)
            {
                Quaternion quat;
                Vector3 position;
                m_WheelColliders[i].GetWorldPose(out position, out quat);
                m_WheelMeshes[i].transform.position = position;
                m_WheelMeshes[i].transform.rotation = quat;
            }

            //clamp input values
            steering = Mathf.Clamp(steering, -1, 1);
            AccelInput = accel = Mathf.Clamp(accel, 0, 1);
            BrakeInput = footbrake = -1 * Mathf.Clamp(footbrake, -1, 0);


            //Set the steer on the front wheels.
            //Assuming that wheels 0 and 1 are the front wheels.
            m_SteerAngle = steering * m_MaximumSteerAngle;
            m_WheelColliders[0].steerAngle = m_SteerAngle;
            m_WheelColliders[1].steerAngle = m_SteerAngle;

            SteerHelper();
            ApplyDrive(accel, footbrake);
            CapSpeed();

            //Set the handbrake.
            //Assuming that wheels 2 and 3 are the rear wheels.



            CalculateRevs();
            GearChanging();

            AddDownForce();
            //  CheckForWheelSpin();
            TractionControl();

        }


        private void CapSpeed()
        {
            float speed = m_Rigidbody.velocity.magnitude;


            speed *= 2.23693629f;
            if (speed > m_Topspeed)
                m_Rigidbody.velocity = (m_Topspeed / 2.23693629f) * m_Rigidbody.velocity.normalized;


        }


        private void ApplyDrive(float accel, float footbrake)
        {

            float thrustTorque;
            switch (m_CarDriveType)
            {
                case CarDriveType.FourWheelDrive:
                    thrustTorque = accel * (m_CurrentTorque / 4f);
                    for (int i = 0; i < 4; i++)
                    {
                        m_WheelColliders[i].motorTorque = thrustTorque;

                    }
                    break;

                case CarDriveType.FrontWheelDrive:
                    thrustTorque = accel * (m_CurrentTorque / 2f);
                    m_WheelColliders[0].motorTorque = m_WheelColliders[1].motorTorque = thrustTorque;


                    break;

                case CarDriveType.RearWheelDrive:
                    thrustTorque = accel * (m_CurrentTorque / 2f);
                    m_WheelColliders[2].motorTorque = m_WheelColliders[3].motorTorque = thrustTorque;
                    break;

            }

            for (int i = 0; i < 4; i++)
            {
                /* if (CurrentSpeed > 5 && Vector3.Angle(transform.forward, m_Rigidbody.velocity) < 50f)
                {
                    m_WheelColliders[i].brakeTorque = m_BrakeTorque*footbrake;
                }
                else*/ 
                if (footbrake > 0)
                {
                     /*m_WheelColliders[i].brakeTorque = 0f;*/
                    m_WheelColliders[i].motorTorque = -m_ReverseTorque * footbrake;

                }
            }
        }


        private void SteerHelper()
        {
            for (int i = 0; i < 4; i++)
            {
                WheelHit wheelhit;
                m_WheelColliders[i].GetGroundHit(out wheelhit);
                if (wheelhit.normal == Vector3.zero)
                    return; // wheels arent on the ground so dont realign the rigidbody velocity
            }

            // this if is needed to avoid gimbal lock problems that will make the car suddenly shift direction
            if (Mathf.Abs(m_OldRotation - transform.eulerAngles.y) < 10f)
            {
                var turnadjust = (transform.eulerAngles.y - m_OldRotation);
                Quaternion velRotation = Quaternion.AngleAxis(turnadjust, Vector3.up);
                m_Rigidbody.velocity = velRotation * m_Rigidbody.velocity;
            }
            m_OldRotation = transform.eulerAngles.y;
        }


        // this is used to add more grip in relation to speed
        private void AddDownForce()
        {
            m_WheelColliders[0].attachedRigidbody.AddForce(-transform.up * m_Downforce *
                                                         m_WheelColliders[0].attachedRigidbody.velocity.magnitude);

        }


        // checks if the wheels are spinning and is so does three things
        // 1) emits particles
        // 2) plays tiure skidding sounds
        // 3) leaves skidmarks on the ground
        // these effects are controlled through the WheelEffects class
        /* private void CheckForWheelSpin()
        {
            // loop through all wheels
            for (int i = 0; i < 4; i++)
            {
                WheelHit wheelHit;
                m_WheelColliders[i].GetGroundHit(out wheelHit); 
                /*
                  if (Mathf.Abs(wheelHit.forwardSlip) >= m_SlipLimit || Mathf.Abs(wheelHit.sidewaysSlip) >= m_SlipLimit)
                {
                   // m_WheelEffects[i].EmitTyreSmoke();

                    // avoiding all four tires screeching at the same time
                    // if they do it can lead to some strange audio artefacts
                    if (!AnySkidSoundPlaying())
                    {
                      //  m_WheelEffects[i].PlayAudio();
                    }
                    continue;


   if (m_WheelEffects[i].PlayingAudio)
                {
                    m_WheelEffects[i].StopAudio();
                }
                m_WheelEffects[i].EndSkidTrail();

                }
                 */
        // is the tire slipping above the given threshhold


        // if it wasnt slipping stop all the audio

        // end the trail generation


        // crude traction control that reduces the power to wheel if the car is wheel spinning too much
        private void TractionControl()
        {
            WheelHit wheelHit;
            switch (m_CarDriveType)
            {
                case CarDriveType.FourWheelDrive:
                    // loop through all wheels
                    for (int i = 0; i < 4; i++)
                    {
                        m_WheelColliders[i].GetGroundHit(out wheelHit);

                        AdjustTorque(wheelHit.forwardSlip);
                    }
                    break;

                case CarDriveType.RearWheelDrive:
                    m_WheelColliders[2].GetGroundHit(out wheelHit);
                    AdjustTorque(wheelHit.forwardSlip);

                    m_WheelColliders[3].GetGroundHit(out wheelHit);
                    AdjustTorque(wheelHit.forwardSlip);
                    break;

                case CarDriveType.FrontWheelDrive:
                    m_WheelColliders[0].GetGroundHit(out wheelHit);
                    AdjustTorque(wheelHit.forwardSlip);

                    m_WheelColliders[1].GetGroundHit(out wheelHit);
                    AdjustTorque(wheelHit.forwardSlip);
                    break;
            }
        }


        private void AdjustTorque(float forwardSlip)
        {
            if (m_CurrentTorque >= 0)
            {
                m_CurrentTorque -= 10 * m_TractionControl;
            }
            else
            {
                m_CurrentTorque += 10 * m_TractionControl;
                if (m_CurrentTorque > m_FullTorqueOverAllWheels)
                {
                    m_CurrentTorque = m_FullTorqueOverAllWheels;
                }
            }
        }



    }
//}
