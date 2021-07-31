using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SplineMesh { 
public class Dusman_kontrol : MonoBehaviour
{
        public Spline[] cizgiler;
        public float konumu,arka_konum,aralik=10.0f;
        public int mevcut_cizginin_sirasi,arka_cizginin_sirasi;
        private Spline mevcut_cizgi,arka_cizgi;
        private GameObject player;
        private bool ileri_git=false,geri_git=false;
        public GameObject arka_kisim;



    // Start is called before the first frame update
    void Start()
    {
            player = GameObject.FindGameObjectWithTag("Player");
            mevcut_cizginin_sirasi =arka_cizginin_sirasi= 0;
            arka_konum = 5;
            konumu = arka_konum + aralik;
            mevcut_cizgi = cizgiler[mevcut_cizginin_sirasi];
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

        }

    // Update is called once per frame
    void Update()
    {

            for(int i = 0; i < 3; i++)
            {
                if (Vector3.Distance(transform.GetChild(i).transform.position, player.transform.position) < 5.0f)
                {
                    ileri_git = true;
                }
                if (Vector3.Distance(arka_kisim.transform.GetChild(i).transform.position, player.transform.position) < 5.0f)
                {
                    geri_git = true;
                }

            }

            if (konumu - arka_konum <aralik )
            {
                if (arka_konum + aralik < arka_cizgi.Length-1)
                {
                    konumu = arka_konum + aralik;

                }


            }


            if(ileri_git)
            {
                konumu += 0.5f;
                arka_konum += 0.5f;
                if (konumu > mevcut_cizgi.Length) konumu = mevcut_cizgi.Length;
                if (arka_konum > arka_cizgi.Length) arka_konum = arka_cizgi.Length;

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
                konumu -= 0.5f;
                arka_konum -= 0.5f;

              CurveSample sample = mevcut_cizgi.GetSampleAtDistance(konumu);
            transform.position = mevcut_cizgi.transform.position;
            transform.position += mevcut_cizgi.transform.forward * sample.location.z;
            transform.position += mevcut_cizgi.transform.right * sample.location.x;
            transform.position += mevcut_cizgi.transform.up * sample.location.y;
            transform.rotation = sample.Rotation;
            transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, 
                mevcut_cizgi.transform.rotation.eulerAngles.y + transform.rotation.eulerAngles.y, 
                transform.rotation.eulerAngles.z);
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
                arka_konum= konumu+arka_cizgi.Length -aralik;
            }

      
    }
}
}
