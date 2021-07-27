using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SplineMesh{ 

    public class Tasi_ve_sekil_ver : MonoBehaviour
    {
        public GameObject Sonuna_eklenecek_cizgi;
        public GameObject kaynak;
        public cizgi_cesidi cizgi_Cesidi;
        public enum cizgi_cesidi { birinci, ikinci, ucuncu, dorduncu, besinci, altinci, yedinci, sekizinci, dokuzuncu, onuncu, onbirinci };
        private Spline cizgi;
        private int sayi;
        public float ilerleme_miktari;
        public void Sona_tak()
        {
            cizgi = Sonuna_eklenecek_cizgi.GetComponent<Spline>();
            float rate = cizgi.nodes.Count - 1;
            CurveSample sample = cizgi.GetSample(rate);

            transform.position =Sonuna_eklenecek_cizgi.transform.position;
            transform.position += Sonuna_eklenecek_cizgi.transform.forward * sample.location.z;
            transform.position += Sonuna_eklenecek_cizgi.transform.right * sample.location.x;
            transform.position += Sonuna_eklenecek_cizgi.transform.up * sample.location.y;
            transform.rotation = sample.Rotation;
            transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, Sonuna_eklenecek_cizgi.transform.rotation.eulerAngles.y + transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }

        public void Sekil_ver()
        {
            if (cizgi_Cesidi == cizgi_cesidi.birinci) sayi = 0;
            if (cizgi_Cesidi == cizgi_cesidi.ikinci) sayi = 1;
            if (cizgi_Cesidi == cizgi_cesidi.ucuncu) sayi = 2;
            if (cizgi_Cesidi == cizgi_cesidi.dorduncu) sayi = 3;
            if (cizgi_Cesidi == cizgi_cesidi.besinci) sayi = 4;
            if (cizgi_Cesidi == cizgi_cesidi.altinci) sayi = 5;
            if (cizgi_Cesidi == cizgi_cesidi.yedinci) sayi = 6;
            if (cizgi_Cesidi == cizgi_cesidi.sekizinci) sayi = 7;
            if (cizgi_Cesidi == cizgi_cesidi.dokuzuncu) sayi = 8;
            if (cizgi_Cesidi == cizgi_cesidi.onuncu) sayi = 9;
            if (cizgi_Cesidi == cizgi_cesidi.onbirinci) sayi = 10;

            UnityEditorInternal.ComponentUtility.CopyComponent(kaynak.GetComponent<Cizgi_kopyalama>().cizgiler[sayi]);
            UnityEditorInternal.ComponentUtility.PasteComponentValues(transform.GetComponent<Spline>());



        }

        public void ilerle()
        {
            transform.position += transform.forward * ilerleme_miktari;


        }

        public void gerile()
        {

            transform.position += transform.forward * ilerleme_miktari * (-1);

        }







    }
}