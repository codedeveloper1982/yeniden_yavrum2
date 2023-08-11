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



    }
}
