using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace SplineMesh
{
    public class Yol_sekillendirme : MonoBehaviour
    {
        public GameObject[] kopya_cizgiler;
        private Spline kopya_cizgi, sekilnenecek;
        public bool tersten_al;
        SplineNode selection;
      /*  public int alinan_cizgi;
        public int alinan_nokta;

        public void esitle()
        {
            sekilnenecek =transform.GetComponent<Spline>();
            kopya_cizgi =  kopya_cizgiler[alinan_cizgi].GetComponent<Spline>();
            int son = sekilnenecek.nodes.Count-1;
            selection = sekilnenecek.nodes[son];
            
            SplineNode newNode = new SplineNode(selection.Direction, selection.Direction + selection.Direction - selection.Position);
            var index = sekilnenecek.nodes.IndexOf(selection);
            if (index == sekilnenecek.nodes.Count - 1)
            {
                sekilnenecek.AddNode(newNode);
            }
            else
            {
                sekilnenecek.InsertNode(index + 1, newNode);
            }




                Vector3 farkal = kopya_cizgi.nodes[alinan_nokta+1].Position - kopya_cizgi.nodes[alinan_nokta].Position;
                Vector3 yonal = kopya_cizgi.nodes[alinan_nokta+1].Direction - kopya_cizgi.nodes[alinan_nokta+1].Position;



            sekilnenecek.nodes[son+1].Position = sekilnenecek.nodes[son].Position + farkal;
            sekilnenecek.nodes[son+1].Direction = sekilnenecek.nodes[son+1].Position + yonal;   

        }*/

        public void ileri_nokta_ekle()
        {
            sekilnenecek = transform.GetComponent<Spline>();
            int son_nokta = sekilnenecek.nodes.Count - 1;
            selection = sekilnenecek.nodes[son_nokta];

            SplineNode newNode = new SplineNode(selection.Direction, selection.Direction + selection.Direction - selection.Position);
            var index = sekilnenecek.nodes.IndexOf(selection);
            if (index == sekilnenecek.nodes.Count - 1)
            {
                sekilnenecek.AddNode(newNode);
            }
            else
            {
                sekilnenecek.InsertNode(index + 1, newNode);
            }





            Vector3 yonal = sekilnenecek.nodes[son_nokta].Direction - sekilnenecek.nodes[son_nokta].Position;
            yonal = yonal.normalized;

            sekilnenecek.nodes[son_nokta+1].Position = sekilnenecek.nodes[son_nokta].Position + yonal * 10;
            sekilnenecek.nodes[son_nokta+1].Direction = sekilnenecek.nodes[son_nokta+1].Position + yonal*3;

        }

        public void son_noktayi_asagi_indir()
        {
            sekilnenecek = transform.GetComponent<Spline>();
            int son_nokta = sekilnenecek.nodes.Count - 1;

            sekilnenecek.nodes[son_nokta].Position += new Vector3(0,-0.5f,0);
            sekilnenecek.nodes[son_nokta].Direction += new Vector3(0,-0.5f,0);

        }
        public void son_noktayi_yukari_cikar()
        {
            sekilnenecek = transform.GetComponent<Spline>();
            int son_nokta = sekilnenecek.nodes.Count - 1;

            sekilnenecek.nodes[son_nokta].Position += new Vector3(0, 0.5f, 0);
            sekilnenecek.nodes[son_nokta].Direction += new Vector3(0, 0.5f, 0);

        }
        public void son_noktayi_saga_cek()
        {
            sekilnenecek = transform.GetComponent<Spline>();
            int son_nokta = sekilnenecek.nodes.Count - 1;

            Vector3 yonal = sekilnenecek.nodes[son_nokta].Direction - sekilnenecek.nodes[son_nokta].Position;
            yonal = yonal.normalized;
            yonal = new Vector3(yonal.z, yonal.y, (-1) *yonal.x);

            sekilnenecek.nodes[son_nokta].Position +=yonal/4;
            sekilnenecek.nodes[son_nokta].Direction +=yonal/4;

        }
        public void son_noktayi_sola_cek()
        {
            sekilnenecek = transform.GetComponent<Spline>();
            int son_nokta = sekilnenecek.nodes.Count - 1;

            Vector3 yonal = sekilnenecek.nodes[son_nokta].Direction - sekilnenecek.nodes[son_nokta].Position;
            yonal = yonal.normalized;
            yonal = new Vector3((-1) *yonal.z, yonal.y,  yonal.x);

            sekilnenecek.nodes[son_nokta].Position += yonal/4;
            sekilnenecek.nodes[son_nokta].Direction += yonal/4;

        }
        public void son_noktayi_sola_dondur()
        {
            sekilnenecek = transform.GetComponent<Spline>();
            int son_nokta = sekilnenecek.nodes.Count - 1;

            Vector3 yonal = sekilnenecek.nodes[son_nokta].Direction - sekilnenecek.nodes[son_nokta].Position;

            yonal = new Vector3((-1) * yonal.z, yonal.y, yonal.x);
            float buyukluk = yonal.magnitude;
            sekilnenecek.nodes[son_nokta].Direction += yonal/6;
            Vector3 duzelt = sekilnenecek.nodes[son_nokta].Direction - sekilnenecek.nodes[son_nokta].Position;

            sekilnenecek.nodes[son_nokta].Direction = sekilnenecek.nodes[son_nokta].Position + duzelt.normalized * buyukluk;


        }
        public void son_noktayi_saga_dondur()
        {
            sekilnenecek = transform.GetComponent<Spline>();
            int son_nokta = sekilnenecek.nodes.Count - 1;

            Vector3 yonal = sekilnenecek.nodes[son_nokta].Direction - sekilnenecek.nodes[son_nokta].Position;
            float buyukluk = yonal.magnitude;
            yonal = new Vector3( yonal.z, yonal.y, (-1) *yonal.x);

            sekilnenecek.nodes[son_nokta].Direction += yonal/6;

            Vector3 duzelt = sekilnenecek.nodes[son_nokta].Direction - sekilnenecek.nodes[son_nokta].Position;

            sekilnenecek.nodes[son_nokta].Direction = sekilnenecek.nodes[son_nokta].Position + duzelt.normalized * buyukluk;

        }

        public void son_noktayi_ileri_it()
        {
            sekilnenecek = transform.GetComponent<Spline>();
            int son_nokta = sekilnenecek.nodes.Count - 1;

            Vector3 yonal = sekilnenecek.nodes[son_nokta].Direction - sekilnenecek.nodes[son_nokta].Position;
            


            sekilnenecek.nodes[son_nokta].Position += yonal/2;
            sekilnenecek.nodes[son_nokta].Direction += yonal/2;


        }
        public void son_noktayi_geri_it()
        {
            sekilnenecek = transform.GetComponent<Spline>();
            int son_nokta = sekilnenecek.nodes.Count - 1;

            Vector3 yonal = sekilnenecek.nodes[son_nokta].Direction - sekilnenecek.nodes[son_nokta].Position;


            sekilnenecek.nodes[son_nokta].Position -= yonal / 4;
            sekilnenecek.nodes[son_nokta].Direction -= yonal / 4;
        }

        public void sola_kavis_ver()
        {
            sekilnenecek = transform.GetComponent<Spline>();
            int son_nokta = sekilnenecek.nodes.Count - 1;

            Vector3 yonal = sekilnenecek.nodes[son_nokta].Direction - sekilnenecek.nodes[son_nokta].Position;

            yonal = new Vector3((-1) * yonal.z, yonal.y, yonal.x);

            sekilnenecek.nodes[son_nokta].Position += yonal/2;
            sekilnenecek.nodes[son_nokta].Direction += yonal/2;

        }
        public void saga_kavis_ver()
        {
            sekilnenecek = transform.GetComponent<Spline>();
            int son_nokta = sekilnenecek.nodes.Count - 1;

            Vector3 yonal = sekilnenecek.nodes[son_nokta].Direction - sekilnenecek.nodes[son_nokta].Position;

            yonal = new Vector3((-1) * yonal.z, yonal.y, yonal.x);

            sekilnenecek.nodes[son_nokta].Position -= yonal / 2;
            sekilnenecek.nodes[son_nokta].Direction -= yonal / 2;

        }
        public void egim_yone_don()
        {
            sekilnenecek = transform.GetComponent<Spline>();
            int son_nokta = sekilnenecek.nodes.Count - 1;
            Vector3 buyuklukal=sekilnenecek.nodes[son_nokta].Direction - sekilnenecek.nodes[son_nokta].Position;
            float x_al, z_al;
            x_al = sekilnenecek.nodes[son_nokta].Direction.x;
            z_al = sekilnenecek.nodes[son_nokta].Direction.z;
            Vector3 yonal = sekilnenecek.nodes[son_nokta].Position - sekilnenecek.nodes[son_nokta-1].Direction;
           // buyuklukal = new Vector3(buyuklukal.x, yonal.y,  buyuklukal.z);
            buyuklukal = new Vector3(yonal.x, yonal.y,  yonal.z);

          //Vector3 duzelt = sekilnenecek.nodes[son_nokta].Direction - sekilnenecek.nodes[son_nokta].Position;

           sekilnenecek.nodes[son_nokta].Direction = sekilnenecek.nodes[son_nokta].Position + buyuklukal.normalized * 4;

            sekilnenecek.nodes[son_nokta].Direction = new Vector3(x_al, sekilnenecek.nodes[son_nokta].Direction.y, z_al);


        }
        public void duzle()
        {
            sekilnenecek = transform.GetComponent<Spline>();
            int son_nokta = sekilnenecek.nodes.Count - 1;
            float y_al=sekilnenecek.nodes[son_nokta].Position.y;
            Vector3 buyuklukal = sekilnenecek.nodes[son_nokta].Direction - sekilnenecek.nodes[son_nokta].Position;
            sekilnenecek.nodes[son_nokta].Direction = new Vector3(sekilnenecek.nodes[son_nokta].Direction.x, y_al, sekilnenecek.nodes[son_nokta].Direction.z);
             Vector3 yeni_buyukluk = sekilnenecek.nodes[son_nokta].Direction - sekilnenecek.nodes[son_nokta].Position;
           sekilnenecek.nodes[son_nokta].Direction = sekilnenecek.nodes[son_nokta].Position + yeni_buyukluk.normalized * 4;            
        }


    }
}

