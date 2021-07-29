using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SplineMesh
{
    public class Malzeme_ekle : MonoBehaviour
    {
        public GameObject hedef_cizgi;
        private GameObject prefab;
        private Mesh yol;
        private Material mat;


        public void agac_ekle()
        {
            GameObject newObj = new GameObject("agac");

            newObj.transform.SetParent(hedef_cizgi.transform);
            newObj.transform.localPosition = Vector3.zero;
            newObj.transform.localRotation = Quaternion.identity;
            newObj.AddComponent(typeof(ExampleSower));
            var ozellikler = newObj.GetComponent<ExampleSower>();
            prefab = GameObject.Find("_gac3");
            ozellikler.prefab = prefab;
            ozellikler.scale = 0.7f;
            ozellikler.spacing = 10.29f;
            ozellikler.offset = 4.9f;
            ozellikler.baslangic = 0;
            ozellikler.son = 100;
            ozellikler.dik_dur = true;
            ozellikler.iki_tarafa_da= true;


        }
        public void dag_ekle()
        {
            GameObject newObj = new GameObject("kayalýklar");

            newObj.transform.SetParent(hedef_cizgi.transform);
            newObj.transform.localPosition = Vector3.zero;
            newObj.transform.localRotation = Quaternion.identity;
            newObj.AddComponent(typeof(ExampleSower));
            var ozellikler = newObj.GetComponent<ExampleSower>();
            prefab = GameObject.Find("atlas_kaya");
            ozellikler.prefab = prefab;
            ozellikler.scale = 40;
            ozellikler.spacing = 1.9f;
            ozellikler.offset = 14.7f;
            ozellikler.baslangic = 0;
            ozellikler.son = 70;
            ozellikler.dik_dur = true;
            ozellikler.iki_tarafa_da= true;


        }

        public void Sona_yamac_ekle()
        {

            GameObject newObj = new GameObject("yamaç");

            newObj.transform.SetParent(hedef_cizgi.transform);
            newObj.transform.localPosition = Vector3.zero;
            newObj.transform.localRotation = Quaternion.identity;
            newObj.AddComponent(typeof(ExampleFollowSpline));
            var ozellikler = newObj.GetComponent<ExampleFollowSpline>();
            prefab = GameObject.Find("yamac");
            ozellikler.spline = hedef_cizgi.GetComponent<Spline>();
            ozellikler.Follower = prefab;
            ozellikler.sona_tak = true;
            ozellikler.ters_cevir = true;
        }


        public void basa_yamac_ekle()
        {

            GameObject newObj = new GameObject("baþ kýsým");

            newObj.transform.SetParent(hedef_cizgi.transform);
            newObj.transform.localPosition = Vector3.zero;
            newObj.transform.localRotation = Quaternion.identity;
            newObj.AddComponent(typeof(ExampleFollowSpline));
            var ozellikler = newObj.GetComponent<ExampleFollowSpline>();
            prefab = GameObject.Find("yamac");
            ozellikler.spline = hedef_cizgi.GetComponent<Spline>();
            ozellikler.Follower = prefab;
            ozellikler.sona_tak = false;
            ozellikler.ters_cevir = false;
        }

        public void Sona_kirik_ekle()
        {

            GameObject newObj = new GameObject("kýrýk köprü");

            newObj.transform.SetParent(hedef_cizgi.transform);
            newObj.transform.localPosition = Vector3.zero;
            newObj.transform.localRotation = Quaternion.identity;
            newObj.AddComponent(typeof(ExampleFollowSpline));
            var ozellikler = newObj.GetComponent<ExampleFollowSpline>();
            prefab = GameObject.Find("kirik_kopru");
            ozellikler.spline = hedef_cizgi.GetComponent<Spline>();
            ozellikler.Follower = prefab;
            ozellikler.sona_tak = true;
            ozellikler.ters_cevir = true;
        }
        public void basa_kirik_ekle()
        {

            GameObject newObj = new GameObject("kýrýk köprü");

            newObj.transform.SetParent(hedef_cizgi.transform);
            newObj.transform.localPosition = Vector3.zero;
            newObj.transform.localRotation = Quaternion.identity;
            newObj.AddComponent(typeof(ExampleFollowSpline));
            var ozellikler = newObj.GetComponent<ExampleFollowSpline>();
            prefab = GameObject.Find("kirik_kopru");
            ozellikler.spline = hedef_cizgi.GetComponent<Spline>();
            ozellikler.Follower = prefab;
            ozellikler.sona_tak = false;
            ozellikler.ters_cevir = false;
        }



        public void kopru_ekle()
        {

            GameObject newObj = new GameObject("köprü");

            newObj.transform.SetParent(hedef_cizgi.transform);
            newObj.transform.localPosition = Vector3.zero;
            newObj.transform.localRotation = Quaternion.identity;
            newObj.AddComponent(typeof(ExampleFollowSpline));
            var ozellikler = newObj.GetComponent<ExampleFollowSpline>();
            prefab = GameObject.Find("kopru");
            ozellikler.spline = hedef_cizgi.GetComponent<Spline>();
            ozellikler.Follower = prefab;
            ozellikler.sona_tak = true;
            ozellikler.ters_cevir = true;
        }




        public void yol_ekle()
        {
            hedef_cizgi.AddComponent(typeof(SplineMeshTiling));
            var ozellikler = hedef_cizgi.GetComponent<SplineMeshTiling>();
            yol = GameObject.Find("atlas_yol").GetComponent<MeshFilter>().mesh;
            mat = GameObject.Find("atlas_yol").GetComponent<MeshRenderer>().material;
            ozellikler.mesh = yol;
            ozellikler.material = mat;
            ozellikler.rotation.y = 90;
            ozellikler.curveSpace = true;
            ozellikler.mode = MeshBender.FillingMode.StretchToInterval;


        }

        public void tunel_ekle()
        {
            GameObject newObj = new GameObject("tunel");

            newObj.transform.SetParent(hedef_cizgi.transform);
            newObj.transform.localPosition = Vector3.zero;
            newObj.transform.localRotation = Quaternion.identity;
            newObj.AddComponent(typeof(ExampleContortAlong));
            yol = GameObject.Find("tunnel").GetComponent<MeshFilter>().mesh;
            mat = GameObject.Find("tunnel").GetComponent<MeshRenderer>().material;
            var ozellikler = newObj.GetComponent<ExampleContortAlong>();
            ozellikler.obje_sayisi = 2;
            ozellikler.aralik = 9.38f;
            ozellikler.kaynak = hedef_cizgi;
            ozellikler.mesh = yol;
            ozellikler.material = mat;
            ozellikler.scale.x = ozellikler.scale.y = ozellikler.scale.z = 1;
            ozellikler.rotation.y = 90;


        }

        public void basa_dere()
        {

            GameObject newObj = new GameObject("baþ dere kýyýsý");

            newObj.transform.SetParent(hedef_cizgi.transform);
            newObj.transform.localPosition = Vector3.zero;
            newObj.transform.localRotation = Quaternion.identity;
            newObj.AddComponent(typeof(ExampleFollowSpline));
            var ozellikler = newObj.GetComponent<ExampleFollowSpline>();
            prefab = GameObject.Find("dere_basi");
            ozellikler.spline = hedef_cizgi.GetComponent<Spline>();
            ozellikler.Follower = prefab;
            ozellikler.sona_tak = false;
            ozellikler.ters_cevir = false;
        }

        public void sona_dere()
        {

            GameObject newObj = new GameObject("sona dere kýyýsý");

            newObj.transform.SetParent(hedef_cizgi.transform);
            newObj.transform.localPosition = Vector3.zero;
            newObj.transform.localRotation = Quaternion.identity;
            newObj.AddComponent(typeof(ExampleFollowSpline));
            var ozellikler = newObj.GetComponent<ExampleFollowSpline>();
            prefab = GameObject.Find("dere_basi");
            ozellikler.spline = hedef_cizgi.GetComponent<Spline>();
            ozellikler.Follower = prefab;
            ozellikler.sona_tak = true;
            ozellikler.ters_cevir = true;
        }



    }
}