using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SplineMesh
{


    public class Noktalar_esitleme : MonoBehaviour
    {

        public GameObject kopyalanacak;
        private Spline bizim_cizgi, esitlenecek;
        public bool tersten_al;

        public void esitle()
        {
            esitlenecek = kopyalanacak.GetComponent<Spline>();
            bizim_cizgi = transform.GetComponent<Spline>();
            int son = bizim_cizgi.nodes.Count - 1;



            if (tersten_al)
            {
                int diger_son = esitlenecek.nodes.Count - 1;
                Vector3 farkal = esitlenecek.nodes[diger_son - son].Position - esitlenecek.nodes[diger_son - son + 1].Position;
                Vector3 yonal = esitlenecek.nodes[diger_son - son].Direction - esitlenecek.nodes[diger_son - son].Position;

                bizim_cizgi.nodes[son].Position = bizim_cizgi.nodes[son - 1].Position + farkal;
                bizim_cizgi.nodes[son].Direction = bizim_cizgi.nodes[son].Position - yonal;



            }
            else
            {

                Vector3 farkal = esitlenecek.nodes[son].Position - esitlenecek.nodes[son - 1].Position;
                Vector3 yonal = esitlenecek.nodes[son].Direction - esitlenecek.nodes[son].Position;



                bizim_cizgi.nodes[son].Position = bizim_cizgi.nodes[son - 1].Position + farkal;
                bizim_cizgi.nodes[son].Direction = bizim_cizgi.nodes[son].Position + yonal;



            }

        }
    }
}