using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ates_denemesi : MonoBehaviour
{

    private GameObject[] fuzeler;// mermiler dizisi oluþturur
    private GameObject[] fuze_trails;
    private GameObject[] fuze_pat;
    private float[] fuze_zamani;
    public GameObject fuze;// çoðaltýlacak füze
    public GameObject fuzeatesi;
    public GameObject pat;
    private Vector3 shootpoint;// füze çýkýþ moktasý
    private Vector3 vo;
    public int fuzesayisi;// aktif olacak füze sayýsý
    private int sira;// aktif füze sýrasý
    private int ilk;//
    private bool ates;// ateþ tuþuna basýldýðýnda true deðerini almasý için
    private float timetofire = 0;// ATIÞ ZAMANI
    private Vector3 hedef;
    private float[] yon_zamani;
    private float yon_rate=0.2f;
    private Vector3 [] onceki_pozisyon;
    [SerializeField]private float firerate;//birim zamandaki füze sayýsý
    //[SerializeField] private float speed;

    //[SerializeField] private float sensorbaslangýcý;
    //[SerializeField] private float fuze_sensoru_uzunlugu;           //bunlar t8 robotunun  için kullanýlacak




    // Start is called before the first frame update
    void Start()
    {

        hedef = GameObject.FindGameObjectWithTag("Player").transform.position;//diðerinde  update içine yaz bu kodu
        Transform child = transform.GetChild(0);
        shootpoint =child.position;
        Debug.Log(child.position);
        fuzeler = new GameObject[fuzesayisi];
        fuze_trails = new GameObject[fuzesayisi];
        fuze_pat = new GameObject[fuzesayisi];
        fuze_zamani = new float[fuzesayisi];
        yon_zamani = new float[fuzesayisi];
        onceki_pozisyon =new Vector3[fuzesayisi];
        for (int i = 0; i < fuzesayisi; i++)
        {
            fuzeler[i] = Instantiate(fuze, shootpoint, Quaternion.identity);
            fuze_trails[i] = Instantiate(fuzeatesi, shootpoint, Quaternion.identity);
            fuze_pat[i] = Instantiate(pat, shootpoint, Quaternion.identity);
            fuze_zamani[i] = 0;
            //mermiler[i].tag = "roket" + i;
            fuzeler[i].SetActive(false);
            fuze_trails[i].SetActive(false);
            fuze_pat[i].SetActive(false);
            onceki_pozisyon[i]=shootpoint;

        yon_zamani[i]= Time.time + yon_rate;



        }
        sira = 0;
        ilk = sira - (fuzesayisi - 1);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            ates = true;

        }

        if (Input.GetMouseButtonUp(0))
        {
            ates = false;
        }
        if (ates && Time.time >= timetofire)
        {
            timetofire = Time.time + 1.0f / firerate;
            fuzeler[sira].transform.position = shootpoint;
            fuze_trails[sira].transform.position = shootpoint;
            onceki_pozisyon[sira]=shootpoint;
            //mermiler[sira].transform.rotation = shootpoint;
            fuzeler[sira].SetActive(true);
            fuze_trails[sira].SetActive(true);
            fuze_zamani[sira] = 0;
            vo = Calculate_velocity(hedef, shootpoint, 2);

            //fuzeler[sira].transform.GetComponent<Rigidbody>().velocity = vo;
            fuzeler[sira].transform.rotation =Quaternion.LookRotation(vo);
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




            }



        }

            for(int i = 0; i < fuzesayisi; i++)
            {
                if (fuzeler[i].activeSelf == true)
                {
                
                if (Time.time > yon_zamani[i])
                {
                    yon_zamani[i] = Time.time + yon_rate;
                    onceki_pozisyon[i] = fuzeler[i].transform.position;

                }
                fuze_zamani[i] += 0.01f;
                fuze_at(fuzeler[i], vo, fuze_zamani[i]);
                Vector3 bak = fuzeler[i].transform.position - onceki_pozisyon[i];   // Calculate_velocity(hedef, shootpoint,fuze_zamani[i] );

                fuze_trails[i].transform.position = fuzeler[i].transform.position;
                //Debug.Log(bak);
                //if(bak!=Vector3.zero)
                    fuzeler[i].transform.rotation = Quaternion.LookRotation(bak);                    
                }

            }





    }


   private Vector3 Calculate_velocity(Vector3 target , Vector3 origin,float time)
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

    private void fuze_at(GameObject fuze, Vector3 distance , float zaman)
    {
        Vector3 vek = distance;

        float x = vek.x;
        float y = vek.y;
        float z = vek.z;

        fuze.transform.position = shootpoint;
         float Vx= x * zaman;
         float Vy =(y * zaman - 0.5f * Mathf.Abs(Physics.gravity.y) * (zaman * zaman));
         float Vz =z * zaman;
        Vector3 havadan_git=new Vector3(Vx,Vy,Vz);
        fuze.transform.position = shootpoint +havadan_git;

        //fuze.transform.rotation = Quaternion.LookRotation(havadan_git.normalized);

    }


}
