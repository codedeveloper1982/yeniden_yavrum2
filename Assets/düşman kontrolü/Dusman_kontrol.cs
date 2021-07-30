using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SplineMesh { 
public class Dusman_kontrol : MonoBehaviour
{
        public Spline[] cizgiler;
        public float konumu;
        public int mevcut_cizginin_sirasi;
        private Spline mevcut_cizgi;
        private GameObject player;
        private bool ileri_git=false;


    // Start is called before the first frame update
    void Start()
    {
            player = GameObject.FindGameObjectWithTag("Player");
            mevcut_cizginin_sirasi = 0;
            konumu = 0;
            mevcut_cizgi = cizgiler[mevcut_cizginin_sirasi];
            CurveSample sample = mevcut_cizgi.GetSampleAtDistance(konumu);
            transform.position = mevcut_cizgi.transform.position;
            transform.position += mevcut_cizgi.transform.forward * sample.location.z;
            transform.position += mevcut_cizgi.transform.right * sample.location.x;
            transform.position += mevcut_cizgi.transform.up * sample.location.y;
            transform.rotation = sample.Rotation;
            transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, mevcut_cizgi.transform.rotation.eulerAngles.y + transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
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


            }



            if(ileri_git)
            {
                konumu += 0.5f;
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
            }


            float fark = (mevcut_cizgi.Length) - konumu;
            if (fark < 10.0f)
            {
                mevcut_cizginin_sirasi++;
                mevcut_cizgi = cizgiler[mevcut_cizginin_sirasi];
                konumu = 0;

            }
      
    }
}
}
