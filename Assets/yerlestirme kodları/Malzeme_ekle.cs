using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SplineMesh
{
    public class Malzeme_ekle : MonoBehaviour
    {
        private GameObject hedef_cizgi;
        private GameObject prefab;
        private Mesh yol;
        private Material mat;
        public GameObject[] combiner_eklenecek;
        private GameObject[] gecici;


        public void agac_ekle()
        {
            Spline cizgi = gameObject.transform.GetComponent<Spline>();
            gecici = new GameObject[20];
            Debug.Log(cizgi.Length);
            float toplam = 10, kalan;

            for (int i = 0; i < 10; i++)
            {



                if (toplam < cizgi.Length)
                {
                    kalan = toplam + 70;

                    if (kalan < cizgi.Length)
                    {

                        GameObject newObj = new GameObject("agac" + i);

                        newObj.transform.SetParent(gameObject.transform);
                        newObj.transform.localPosition = Vector3.zero;
                        newObj.transform.localRotation = Quaternion.identity;
                        newObj.AddComponent(typeof(ExampleSower));
                        var ozellikler = newObj.GetComponent<ExampleSower>();
                        prefab = GameObject.Find("_gac3");
                        ozellikler.prefab = prefab;
                        ozellikler.scale = 25;
                        ozellikler.spacing = 10.29f;
                        ozellikler.offset = 4.9f;

                        ozellikler.baslangic = toplam;
                        ozellikler.son = kalan;
                        ozellikler.dik_dur = true;
                        ozellikler.iki_tarafa_da = true;

                        toplam += 70;

                        gecici[i] = newObj;


                    }
                    else
                    {
                        Debug.Log(toplam + "   " + cizgi.Length);
                        Debug.Log(toplam + "   " + (kalan));
                        GameObject newObj = new GameObject("agac" + i);

                        newObj.transform.SetParent(gameObject.transform);
                        newObj.transform.localPosition = Vector3.zero;
                        newObj.transform.localRotation = Quaternion.identity;
                        newObj.AddComponent(typeof(ExampleSower));
                        var ozellikler = newObj.GetComponent<ExampleSower>();
                        prefab = GameObject.Find("_gac3");
                        ozellikler.prefab = prefab;
                        ozellikler.scale = 25;
                        ozellikler.spacing = 10.29f;
                        ozellikler.offset = 4.9f;

                        ozellikler.baslangic = toplam;
                        ozellikler.son = cizgi.Length;
                        ozellikler.dik_dur = true;
                        ozellikler.iki_tarafa_da = true;
                        toplam += 70;


                        gecici[i] = newObj;
                        combiner_eklenecek = new GameObject[i+1];

                    }
                }





            }
                for (int i = 0; i < combiner_eklenecek.Length; i++)
                {
                    combiner_eklenecek[i] = gecici[i];

                }


            /*
            GameObject newObj = new GameObject("agac");
            hedef_cizgi = gameObject;
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

*/
        }
        public void dag_ekle()
        {
            Spline cizgi = gameObject.transform.GetComponent<Spline>();
            gecici = new GameObject[20];
            Debug.Log(cizgi.Length);
            float toplam=0,kalan;

            for(int i = 0; i < 10; i++)
            {



                if(toplam < cizgi.Length) {
                kalan = toplam + 70;
                
                if (kalan < cizgi.Length)
                {
                    Debug.Log(toplam + "   " + (kalan));
                    GameObject newObj = new GameObject("kaya" + i);

                    newObj.transform.SetParent(gameObject.transform);
                    newObj.transform.localPosition = Vector3.zero;
                    newObj.transform.localRotation = Quaternion.identity;
                    newObj.AddComponent(typeof(ExampleSower));
                    var ozellikler = newObj.GetComponent<ExampleSower>();
                    prefab = GameObject.Find("atlas_kaya3");
                    ozellikler.prefab = prefab;
                    ozellikler.scale = 40;
                    ozellikler.spacing = 1.9f;
                    ozellikler.offset = 14.7f;

                    ozellikler.baslangic = toplam;
                    ozellikler.son = kalan;
                    ozellikler.dik_dur = true;
                    ozellikler.iki_tarafa_da = true;


                        gecici[i] = newObj;
                        toplam += 70;
                }
                else
                {
                    Debug.Log(toplam + "   " + cizgi.Length);
                    Debug.Log(toplam + "   " + (kalan));
                    GameObject newObj = new GameObject("kaya" + i);

                    newObj.transform.SetParent(gameObject.transform);
                    newObj.transform.localPosition = Vector3.zero;
                    newObj.transform.localRotation = Quaternion.identity;
                    newObj.AddComponent(typeof(ExampleSower));
                    var ozellikler = newObj.GetComponent<ExampleSower>();
                    prefab = GameObject.Find("atlas_kaya3");
                    ozellikler.prefab = prefab;
                    ozellikler.scale = 40;
                    ozellikler.spacing = 1.9f;
                    ozellikler.offset = 14.7f;

                    ozellikler.baslangic = toplam;
                    ozellikler.son =cizgi.Length;
                    ozellikler.dik_dur = true;
                    ozellikler.iki_tarafa_da = true;
                    toplam += 70;

                        gecici[i] = newObj;
                        combiner_eklenecek = new GameObject[i + 1];

                    }
                }


            }

            for (int i = 0; i < combiner_eklenecek.Length; i++)
            {
                combiner_eklenecek[i] = gecici[i];

            }

            /*
            GameObject newObj = new GameObject("kaya"+i);

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

*/
        }

        public void Sona_yamac_ekle()
        {

            GameObject newObj = new GameObject("yamaç");

            newObj.transform.SetParent(gameObject.transform);
            newObj.transform.localPosition = Vector3.zero;
            newObj.transform.localRotation = Quaternion.identity;
            newObj.AddComponent(typeof(ExampleFollowSpline));
            var ozellikler = newObj.GetComponent<ExampleFollowSpline>();
            prefab = GameObject.Find("yamac");
            ozellikler.spline = gameObject.GetComponent<Spline>();
            ozellikler.Follower = prefab;
            ozellikler.sona_tak = true;
            ozellikler.ters_cevir = true;
        }


        public void basa_yamac_ekle()
        {

            GameObject newObj = new GameObject("baþ kýsým");

            newObj.transform.SetParent(gameObject.transform);
            newObj.transform.localPosition = Vector3.zero;
            newObj.transform.localRotation = Quaternion.identity;
            newObj.AddComponent(typeof(ExampleFollowSpline));
            var ozellikler = newObj.GetComponent<ExampleFollowSpline>();
            prefab = GameObject.Find("yamac");
            ozellikler.spline = gameObject.GetComponent<Spline>();
            ozellikler.Follower = prefab;
            ozellikler.sona_tak = false;
            ozellikler.ters_cevir = false;
        }

        public void Sona_kirik_ekle()
        {

            GameObject newObj = new GameObject("kýrýk köprü");

            newObj.transform.SetParent(gameObject.transform);
            newObj.transform.localPosition = Vector3.zero;
            newObj.transform.localRotation = Quaternion.identity;
            newObj.AddComponent(typeof(ExampleFollowSpline));
            var ozellikler = newObj.GetComponent<ExampleFollowSpline>();
            prefab = GameObject.Find("kirik_kopru");
            ozellikler.spline = gameObject.GetComponent<Spline>();
            ozellikler.Follower = prefab;
            ozellikler.sona_tak = true;
            ozellikler.ters_cevir = true;
        }
        public void basa_kirik_ekle()
        {

            GameObject newObj = new GameObject("kýrýk köprü");

            newObj.transform.SetParent(gameObject.transform);
            newObj.transform.localPosition = Vector3.zero;
            newObj.transform.localRotation = Quaternion.identity;
            newObj.AddComponent(typeof(ExampleFollowSpline));
            var ozellikler = newObj.GetComponent<ExampleFollowSpline>();
            prefab = GameObject.Find("kirik_kopru");
            ozellikler.spline = gameObject.GetComponent<Spline>();
            ozellikler.Follower = prefab;
            ozellikler.sona_tak = false;
            ozellikler.ters_cevir = false;
        }



        public void kopru_ekle()
        {

            GameObject newObj = new GameObject("köprü");
            hedef_cizgi = gameObject;
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



        public void tunel_ekle()
        {
            GameObject newObj = new GameObject("tunel");
            hedef_cizgi = gameObject;
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
            hedef_cizgi = gameObject;
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
            hedef_cizgi = gameObject;
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
        public void b_kasis_ekle()
        {

            GameObject newObj = new GameObject("baþ kasis");
            hedef_cizgi = gameObject;
            newObj.transform.SetParent(hedef_cizgi.transform);
            newObj.transform.localPosition = Vector3.zero;
            newObj.transform.localRotation = Quaternion.identity;
            newObj.AddComponent(typeof(ExampleFollowSpline));
            var ozellikler = newObj.GetComponent<ExampleFollowSpline>();
            prefab = GameObject.Find("kasis");
            ozellikler.spline = hedef_cizgi.GetComponent<Spline>();
            ozellikler.Follower = prefab;
            ozellikler.sona_tak = false;
            ozellikler.ters_cevir = false;
        }

        public void s_kasis_ekle()
        {

            GameObject newObj = new GameObject("son kasis");
            hedef_cizgi = gameObject;
            newObj.transform.SetParent(hedef_cizgi.transform);
            newObj.transform.localPosition = Vector3.zero;
            newObj.transform.localRotation = Quaternion.identity;
            newObj.AddComponent(typeof(ExampleFollowSpline));
            var ozellikler = newObj.GetComponent<ExampleFollowSpline>();
            prefab = GameObject.Find("kasis");
            ozellikler.spline = hedef_cizgi.GetComponent<Spline>();
            ozellikler.Follower = prefab;
            ozellikler.sona_tak = true;
            ozellikler.ters_cevir = true;
        }

        public void Combiner_ekle() 
        {
            for (int i = 0; i < combiner_eklenecek.Length; i++)
            {
                combiner_eklenecek[i].transform.GetChild(0).gameObject.AddComponent(typeof(MeshFilter));
                combiner_eklenecek[i].transform.GetChild(0).gameObject.AddComponent(typeof(MeshRenderer));
                combiner_eklenecek[i].transform.GetChild(0).gameObject.AddComponent(typeof(MeshCollider));
                combiner_eklenecek[i].transform.GetChild(0).gameObject.AddComponent(typeof(Mesh_combiner));

                Transform ogul = combiner_eklenecek[i].transform.GetChild(0).gameObject.transform;

                UnityEditorInternal.ComponentUtility.CopyComponent(ogul.GetComponentInParent<ExampleSower>().prefab.GetComponent<MeshFilter>());
                UnityEditorInternal.ComponentUtility.PasteComponentValues(ogul.GetComponent<MeshFilter>());

                UnityEditorInternal.ComponentUtility.CopyComponent(ogul.GetComponentInParent<ExampleSower>().prefab.GetComponent<MeshRenderer>());
                UnityEditorInternal.ComponentUtility.PasteComponentValues(ogul.GetComponent<MeshRenderer>());


            }
            combiner_eklenecek = new GameObject[0];

        }


        public void tunel_sonu_ekle()
        {

            GameObject newObj = new GameObject("tünel_sonu");

            newObj.transform.SetParent(gameObject.transform);
            newObj.transform.localPosition = Vector3.zero;
            newObj.transform.localRotation = Quaternion.identity;
            newObj.AddComponent(typeof(ExampleFollowSpline));
            var ozellikler = newObj.GetComponent<ExampleFollowSpline>();
            prefab = GameObject.Find("tünel_ucu_tepelik");
            ozellikler.spline = gameObject.GetComponent<Spline>();
            ozellikler.Follower = prefab;
            ozellikler.sona_tak = true;
            ozellikler.ters_cevir = false;
        }
        public void tunel_basi_ekle()
        {

            GameObject newObj = new GameObject("tünel_baþý");

            newObj.transform.SetParent(gameObject.transform);
            newObj.transform.localPosition = Vector3.zero;
            newObj.transform.localRotation = Quaternion.identity;
            newObj.AddComponent(typeof(ExampleFollowSpline));
            var ozellikler = newObj.GetComponent<ExampleFollowSpline>();
            prefab = GameObject.Find("tünel_ucu_tepelik");
            ozellikler.spline = gameObject.GetComponent<Spline>();
            ozellikler.Follower = prefab;
            ozellikler.sona_tak = false;
            ozellikler.ters_cevir = true;
        }

    }
}