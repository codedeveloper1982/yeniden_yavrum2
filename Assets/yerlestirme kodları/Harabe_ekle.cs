using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SplineMesh
{
    public class Harabe_ekle : MonoBehaviour
    {
        private GameObject prefab;
        private float yukseklik = 1.7f;


        public void mavi_ekle()
        {

            GameObject newObj = new GameObject("mavi");

            newObj.transform.SetParent(gameObject.transform);
            newObj.transform.localPosition = Vector3.zero;
            newObj.transform.localRotation = Quaternion.identity;
            newObj.AddComponent(typeof(Examle_benim));
            var ozellikler = newObj.GetComponent<Examle_benim>();
            ozellikler.spline = gameObject.GetComponent<Spline>();
            prefab = GameObject.Find("mavi");
            ozellikler.Follower = prefab;
            ozellikler.yukseklik = yukseklik;
        }


        public void kimizi_ekle()
        {

            GameObject newObj = new GameObject("kimizi");

            newObj.transform.SetParent(gameObject.transform);
            newObj.transform.localPosition = Vector3.zero;
            newObj.transform.localRotation = Quaternion.identity;
            newObj.AddComponent(typeof(Examle_benim));
            var ozellikler = newObj.GetComponent<Examle_benim>();
            ozellikler.spline = gameObject.GetComponent<Spline>();
            prefab = GameObject.Find("kimizi");
            ozellikler.Follower = prefab;
            ozellikler.yukseklik = yukseklik;
        }
        public void beyaz_ekle()
        {

            GameObject newObj = new GameObject("beyaz");

            newObj.transform.SetParent(gameObject.transform);
            newObj.transform.localPosition = Vector3.zero;
            newObj.transform.localRotation = Quaternion.identity;
            newObj.AddComponent(typeof(Examle_benim));
            var ozellikler = newObj.GetComponent<Examle_benim>();
            ozellikler.spline = gameObject.GetComponent<Spline>();
            prefab = GameObject.Find("beyaz");
            ozellikler.Follower = prefab;
            ozellikler.yukseklik = yukseklik;
        }
        public void pasli_ekle()
        {

            GameObject newObj = new GameObject("pasli");

            newObj.transform.SetParent(gameObject.transform);
            newObj.transform.localPosition = Vector3.zero;
            newObj.transform.localRotation = Quaternion.identity;
            newObj.AddComponent(typeof(Examle_benim));
            var ozellikler = newObj.GetComponent<Examle_benim>();
            ozellikler.spline = gameObject.GetComponent<Spline>();
            prefab = GameObject.Find("pasli");
            ozellikler.Follower = prefab;
            ozellikler.yukseklik = yukseklik;
        }
        public void dortlu_ekle()
        {
            Random.seed = System.DateTime.Now.Millisecond;
            string[] harabeler = { "beyaz", "kimizi", "mavi", "pasli" };


            string siralama = "";
            int tekrar = harabeler.Length;



                        GameObject newObj = new GameObject("dortluler");

                        newObj.transform.SetParent(gameObject.transform);
                        newObj.transform.localPosition = Vector3.zero;
                        newObj.transform.localRotation = Quaternion.identity;
                        newObj.AddComponent(typeof(Dortlu_kontrol));




            for (int i = 0; i < tekrar; i++)
            { ;
                int rast = UnityEngine.Random.Range(0, harabeler.Length);



                siralama += harabeler[rast] + " ";




                    GameObject araba = new GameObject(harabeler[rast]);
                        araba.transform.SetParent(newObj.transform);
                        araba.transform.localPosition = Vector3.zero;
                        araba.transform.localRotation = Quaternion.identity;
                    araba.AddComponent(typeof(Examle_benim));
                    var ozellikler = araba.GetComponent<Examle_benim>();
                    ozellikler.spline = gameObject.GetComponent<Spline>();
                    prefab = GameObject.Find(harabeler[rast]);
                    ozellikler.Follower = prefab;
                    ozellikler.yukseklik = yukseklik;
                     int us = (int)Mathf.Pow(-1, UnityEngine.Random.Range(0, 100));

                Debug.Log(us);
                    ozellikler.donus=90*us;

                     us=(int) Mathf.Pow(-1, i);

                    if (i == 0 || i == 3) {

                        ozellikler.kayma = 3.96f *us ;
                       }
                    else
                    {

                        ozellikler.kayma = 1.36f *us ;
                    }



                string[] gecici_harebeler = new string[harabeler.Length - 1];
                int gecici_int = 0;
                for (int j = 0; j < harabeler.Length; j++) { 
                    if (harabeler[j] == harabeler[rast])
                    {

                    }
                    else
                    {
                        gecici_harebeler[gecici_int] = harabeler[j];
                        gecici_int++;
                    }

            }
                harabeler = gecici_harebeler;

        }


            


            



        }



    }
}
