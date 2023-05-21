using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace SplineMesh {

    public class Kamera_denet : MonoBehaviour
    {

        public Spline[] line;
        private CurveSample kavis;
        private int sira=0;
        public bool sag = true;
        public float ilerleme,aci;


       





        private void Update()
        {

           ilerleme +=0.25f;

            if (ilerleme > line[sira].Length && sag==true)
            {
                sag = false;
                ilerleme = 0;

            }else  if(ilerleme>line[sira].Length && sag == false)
            {
                sag = true;
                sira++;
                ilerleme = 0;

            }

            if (sag)
            {

                aci = 90;
            }
            else aci = -90;

            Hareket_et(transform.gameObject, line[sira], kavis,ilerleme);
            
        }



        private void Hareket_et(GameObject obje, Spline cizgi, CurveSample egri, float konumu)
        {
            egri = cizgi.GetSampleAtDistance(konumu);
            obje.transform.position = cizgi.transform.position;
            obje.transform.position += cizgi.transform.forward * egri.location.z;
            obje.transform.position += cizgi.transform.right * egri.location.x;
            obje.transform.position += cizgi.transform.up * egri.location.y+new Vector3(0,3,0);
            obje.transform.rotation = egri.Rotation;
            obje.transform.eulerAngles = new Vector3(obje.transform.rotation.eulerAngles.x,
                cizgi.transform.rotation.eulerAngles.y + obje.transform.rotation.eulerAngles.y+aci,
                obje.transform.rotation.eulerAngles.z);

        }





    }
}
