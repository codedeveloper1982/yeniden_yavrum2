using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;
using UnityEngine.UI;


    public class Menu_slider : MonoBehaviour
    {
        public Spline[] line;
        private CurveSample kavis;
        private int sira = 0,gosterge_araliklari=40,bas,son, renk_sirasi;

        public float ilerleme, toplam_yol,v,eski_v,kalan_noktalar,gosterge_sayisi;
        private float basma_zamani, basim_bekleme=0.1f;
        private GameObject gosterge;
        private GameObject[] gostergeler;
        private float[] gosterge_konum;
        private Vector3[] sabit;
    private float renk_zamani, renk_araligi;




    private void Start()
        {
            basma_zamani = Time.time + basim_bekleme;

           gosterge= GameObject.Find("gosterge");

            kalan_noktalar=line[sira].Length%gosterge_araliklari;
            gosterge_sayisi = (line[sira].Length - kalan_noktalar) / gosterge_araliklari;

            gostergeler = new GameObject[(int)gosterge_sayisi];
            gosterge_konum = new float[(int)gosterge_sayisi];
            sabit = new Vector3[(int)gosterge_sayisi];
            for (int i = 0; i < gosterge_sayisi; i++)
            {
             // Vector3 konum =line[sira].transform.position+ line[sira].nodes[gosterge_araliklari * (i +1) ].Position+new Vector3(0,4,0);



                gostergeler[i]= Instantiate(gosterge, gosterge.transform.position, Quaternion.identity);
                gosterge_konum[i] = (i + 1) * gosterge_araliklari;
                Hareket_et(gostergeler[i], line[sira], kavis,gosterge_konum[i]);
                sabit[i] =gostergeler[i].transform.position+new Vector3(0, 4, 0);
                gostergeler[i].transform.position = sabit[i];
            }


            bas = 0;
            toplam_yol = 0;
            renk_sirasi = 0;
        renk_araligi = 0.1f;
        renk_zamani = Time.time + renk_araligi;
    }





    private void Update()
    {

        /////////////////////// BURASI DOUKDUÐUNDA VE BIRAKTIÐINDA YAPILACAKLARLA ÝLGÝLÝ////////////////////

        if (Input.touchCount > 0)
        {

            if (Time.time >= basma_zamani)
            {

                basma_zamani = Time.time + basim_bekleme;
                eski_v = Input.GetTouch(0).position.y;


            }



            v = Input.GetTouch(0).position.y;

            v = v - eski_v;

            toplam_yol += (-1) * v / 30;


        } else if (Input.GetMouseButton(0))
        {
            v = Input.GetAxis("Mouse Y");

            toplam_yol += (-1) * v;


        }
        else
        {
            for (int i = 0; i < gosterge_sayisi; i++)
            {
                //line[sira].nodes.Count
                if ((toplam_yol > i * gosterge_araliklari || toplam_yol == i * gosterge_araliklari) && toplam_yol < (i + 1) * gosterge_araliklari)
                {
                    bas = i * gosterge_araliklari;
                    son = bas + gosterge_araliklari;






                }

                if ((toplam_yol - bas) < (2 * gosterge_araliklari / 5)) {

                    toplam_yol = Mathf.Lerp(toplam_yol, bas, Time.deltaTime);

                }
                else
                {
                    toplam_yol = Mathf.Lerp(toplam_yol, son, Time.deltaTime);
                }





            }




        }
        /////////////////////// BURASI DOUKDUÐUNDA VE BIRAKTIÐINDA YAPILACAKLARLA ÝLGÝLÝ////////////////////




        for (int i = 0; i < gosterge_sayisi; i++) {

            float yakinlik = Mathf.Abs(gosterge_konum[i] - toplam_yol);


            if (yakinlik < gosterge_araliklari)
            {
                float kat = 1 + 2 * (1 - (yakinlik / gosterge_araliklari));
                gostergeler[i].transform.localScale = new Vector3(0.08f * kat, 0.08f * kat, 0.08f * kat);
                gostergeler[i].transform.position = sabit[i] + new Vector3(0, (kat - 1) * 0.08f * gostergeler[i].GetComponentInChildren<RectTransform>().rect.width / 2, 0);



                //Debug.Log(gostergeler[i].GetComponentInChildren<RectTransform>().rect.width);renkler[renk_sirasi];
            }

                gostergeler[i].transform.GetChild(1).transform.GetChild(renk_sirasi).gameObject.SetActive(false);

            int sonraki = renk_sirasi + 1;

            if (sonraki==gosterge.transform.GetChild(1).transform.childCount)
            {
                sonraki = 0;
            }


                gostergeler[i].transform.GetChild(1).transform.GetChild(sonraki).gameObject.SetActive(true);
            
      

        }


        if (renk_zamani < Time.time)
        {
            renk_zamani = Time.time + renk_araligi;
            renk_sirasi++; 
            if (renk_sirasi == gosterge.transform.GetChild(1).transform.childCount)
                {
                    renk_sirasi = 0;
                }
        }


        if (toplam_yol < 0) toplam_yol = 0;
        else if (toplam_yol > line[sira].Length) toplam_yol = line[sira].Length;

        Hareket_et(transform.gameObject, line[sira], kavis, toplam_yol);



        Debug.Log(renk_sirasi);

    }

        private void Hareket_et(GameObject obje, Spline cizgi, CurveSample egri, float konumu)
        {
            egri = cizgi.GetSampleAtDistance(konumu);
            obje.transform.position = cizgi.transform.position;
            obje.transform.position += cizgi.transform.forward * egri.location.z;
            obje.transform.position += cizgi.transform.right * egri.location.x;
            obje.transform.position += cizgi.transform.up * egri.location.y + new Vector3(0, 3, 0);
           /* obje.transform.rotation = egri.Rotation;
            obje.transform.eulerAngles = new Vector3(obje.transform.rotation.eulerAngles.x,
                cizgi.transform.rotation.eulerAngles.y + obje.transform.rotation.eulerAngles.y + toplam_yol,
                obje.transform.rotation.eulerAngles.z);
*/
        }
    }
