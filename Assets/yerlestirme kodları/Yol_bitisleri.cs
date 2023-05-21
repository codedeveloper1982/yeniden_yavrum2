using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace SplineMesh
{

    public class Yol_bitisleri : MonoBehaviour
    {

        private GameObject cizgi;
        private GameObject su;
        private GameObject kayalik;

        private int sivisayisi = 0;
        private GameObject son_eklenen;






        public void deniz_ekle()
        {
            cizgi = gameObject;

            su = GameObject.FindGameObjectWithTag("su");
            Spline line = cizgi.GetComponent<Spline>();


            Vector3 konum = line.transform.position + line.nodes[line.nodes.Count-1].Position;
             konum.y = konum.y - 5;
            son_eklenen= Instantiate(su, konum, Quaternion.identity);
            son_eklenen.tag = "su";
        }

        public void dag_ekle()
        {
            cizgi = gameObject;

            kayalik = GameObject.Find("atlas_kaya3");
            Spline line = cizgi.GetComponent<Spline>();


            Vector3 konum = line.transform.position + line.nodes[line.nodes.Count-1].Position;
            konum.y = konum.y - 5;

            son_eklenen= Instantiate(kayalik, konum, Quaternion.identity);
            son_eklenen.transform.localScale = new Vector3(107,107,107);
       
        }







    }
}
