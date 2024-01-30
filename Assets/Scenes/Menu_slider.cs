using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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
    private Button[] dugme;
    public bool sahne=false;

    public static string[][] dusman_listeleri;
    public static int[][] dusman_sayilari;
    public int episode;
    private int drone5_sayisi , drone6_sayisi , dronex_sayisi , drone11c_sayisi , drone1b_sayisi, mayinci_sayisi, fuzeci_sayisi, sonuncu_sayisi;
    private int cesit=0;
    private Vector2 avatar_buyuklugu;
    private Vector3 avatar_baslama_yeri;
    private float satir,sutun;


    /// <summary>
    /// //////////GÖSTERGE ÝÇÝ YAZILAR BURAYA
    /// </summary>
    private Sprite drone5_avatar, drone6_avatar,mayinci_avatar, drone11c_avatar, drone1b_avatar, dronex_avatar, fuzeci_avatar, sonuncu_avatar;



    private void Start()
        {
             drone5_avatar= Resources.Load<Sprite>("dusman_avatarlarý/drone5");
            drone6_avatar= Resources.Load<Sprite>("dusman_avatarlarý/drone6");
            mayinci_avatar= Resources.Load<Sprite>("dusman_avatarlarý/mayinci");
            drone11c_avatar= Resources.Load<Sprite>("dusman_avatarlarý/drone11c");
            drone1b_avatar= Resources.Load<Sprite>("dusman_avatarlarý/drone1b");
            dronex_avatar= Resources.Load<Sprite>("dusman_avatarlarý/dronex");
            fuzeci_avatar = Resources.Load<Sprite>("dusman_avatarlarý/fuzeci");
            sonuncu_avatar= Resources.Load<Sprite>("dusman_avatarlarý/sonuncu");


        avatar_buyuklugu = new Vector2(23, 23);
        avatar_baslama_yeri = new Vector3(21, -30,0);


         dusman_listeleri = new string[1][];
        dusman_sayilari= new int[1][];


        dusman_listeleri[0] = new string[] { "drone5", "drone6", "dronex", "mayinci", "drone11c", "drone1b", "drone5", "drone5", "drone5" };
        dusman_sayilari[0]= new int[] { 1, 2, 1, 1, 2, 2 };






        basma_zamani = Time.time + basim_bekleme;

           gosterge= GameObject.Find("gosterge");

            kalan_noktalar=line[sira].Length%gosterge_araliklari;
            gosterge_sayisi = (line[sira].Length - kalan_noktalar) / gosterge_araliklari;

            gostergeler = new GameObject[(int)gosterge_sayisi];
            dugme=new Button[(int)gosterge_sayisi];
            gosterge_konum = new float[(int)gosterge_sayisi];
            sabit = new Vector3[(int)gosterge_sayisi];
            for (int i = 0; i < gosterge_sayisi; i++)
            {
             // Vector3 konum =line[sira].transform.position+ line[sira].nodes[gosterge_araliklari * (i +1) ].Position+new Vector3(0,4,0);



                gostergeler[i]= Instantiate(gosterge, gosterge.transform.position, Quaternion.identity);
                gostergeler[i].name = (i + 1).ToString();
                gosterge_konum[i] = (i + 1) * gosterge_araliklari;
                Hareket_et(gostergeler[i], line[sira], kavis,gosterge_konum[i]);
                sabit[i] =gostergeler[i].transform.position+new Vector3(0, 4, 0);
                gostergeler[i].transform.position = sabit[i];
                dugme[i]= gostergeler[i].GetComponentInChildren<Button>();

                dugme[i].onClick.AddListener(sahne_gec);
            dugme[i].transform.gameObject.SetActive(false);







        }





       


            bas = 0;
            toplam_yol = 0;
            renk_sirasi = 0;
        renk_araligi = 0.1f;
        renk_zamani = Time.time + renk_araligi;





        for (int j = 0; j < dusman_listeleri.Length; j++)
        {
            drone5_sayisi = 0;
            drone6_sayisi = 0; 
            dronex_sayisi = 0; 
            drone11c_sayisi = 0; 
            drone1b_sayisi = 0; 
            mayinci_sayisi = 0; 
            fuzeci_sayisi = 0; 
            sonuncu_sayisi = 0;


            for (int i = 0; i < dusman_listeleri[j].Length; i++)
        {


            if (dusman_listeleri[j][i] == "drone5")
            {

                    drone5_sayisi++;

            }
            else if (dusman_listeleri[j][i] == "drone6")
            {

                    drone6_sayisi++;


            }
            else if (dusman_listeleri[j][i] == "mayinci")
            {


                    drone1b_sayisi++;

            }
            else if (dusman_listeleri[j][i] == "drone11c")
            {

                    drone11c_sayisi++;


            }
            else if (dusman_listeleri[j][i] == "drone1b")
            {


                    mayinci_sayisi++;


            }
            else if (dusman_listeleri[j][i] == "fuzeci")
            {

                    fuzeci_sayisi++;


            }
            else if (dusman_listeleri[j][i] == "dronex")
            {

                 dronex_sayisi++;

            }
            else if (dusman_listeleri[j][i] == "sonuncu")
            {

                 sonuncu_sayisi++; 

            }



           }


                if (drone5_sayisi != 0)
                {

                cesit++;
                Transform kanvas = gostergeler[j].transform.Find("yazý_canvasý");
                GameObject avatar = new GameObject("avatar");
                avatar.AddComponent<Image>();
                Image resim = avatar.GetComponent<Image>();
                avatar.transform.SetParent(kanvas);
                resim.rectTransform.anchorMin=new Vector2(0, 1);
                resim.rectTransform.anchorMax=new Vector2(0, 1);
                resim.rectTransform.anchoredPosition3D = new Vector3(0, 0, 0);
                resim.rectTransform.sizeDelta = avatar_buyuklugu;
                resim.rectTransform.localRotation = Quaternion.identity;
                resim.rectTransform.localScale=new Vector3(1,1,1);
                resim.sprite = drone5_avatar;



                }
                if(drone6_sayisi != 0)
                {
                cesit++;
                Transform kanvas = gostergeler[j].transform.Find("yazý_canvasý");
                GameObject avatar = new GameObject("avatar");
                avatar.AddComponent<Image>();
                Image resim = avatar.GetComponent<Image>();
                avatar.transform.SetParent(kanvas);
                resim.rectTransform.anchorMin=new Vector2(0, 1);
                resim.rectTransform.anchorMax=new Vector2(0, 1);
                resim.rectTransform.anchoredPosition3D = new Vector3(0, 0, 0);
                resim.rectTransform.sizeDelta = avatar_buyuklugu;
                resim.rectTransform.localRotation = Quaternion.identity;
                resim.rectTransform.localScale=new Vector3(1,1,1);
                resim.sprite = drone6_avatar;

                }
             if(dronex_sayisi != 0)
                {
                cesit++;
                Transform kanvas = gostergeler[j].transform.Find("yazý_canvasý");
                GameObject avatar = new GameObject("avatar");
                avatar.AddComponent<Image>();
                Image resim = avatar.GetComponent<Image>();
                avatar.transform.SetParent(kanvas);
                resim.rectTransform.anchorMin=new Vector2(0, 1);
                resim.rectTransform.anchorMax=new Vector2(0, 1);
                resim.rectTransform.anchoredPosition3D = new Vector3(0, 0, 0);
                resim.rectTransform.sizeDelta = avatar_buyuklugu;
                resim.rectTransform.localRotation = Quaternion.identity;
                resim.rectTransform.localScale=new Vector3(1,1,1);
                resim.sprite = dronex_avatar;




                }
            if(drone1b_sayisi != 0)
                {
                cesit++;

                Transform kanvas = gostergeler[j].transform.Find("yazý_canvasý");
                GameObject avatar = new GameObject("avatar");
                avatar.AddComponent<Image>();
                Image resim = avatar.GetComponent<Image>();
                avatar.transform.SetParent(kanvas);
                resim.rectTransform.anchorMin=new Vector2(0, 1);
                resim.rectTransform.anchorMax=new Vector2(0, 1);
                resim.rectTransform.anchoredPosition3D = new Vector3(0, 0, 0);
                resim.rectTransform.sizeDelta = avatar_buyuklugu;
                resim.rectTransform.localRotation = Quaternion.identity;
                resim.rectTransform.localScale=new Vector3(1,1,1);
                resim.sprite = drone1b_avatar;



                }
            if(drone11c_sayisi != 0)
                {
                cesit++;

                Transform kanvas = gostergeler[j].transform.Find("yazý_canvasý");
                GameObject avatar = new GameObject("avatar");
                avatar.AddComponent<Image>();
                Image resim = avatar.GetComponent<Image>();
                avatar.transform.SetParent(kanvas);
                resim.rectTransform.anchorMin=new Vector2(0, 1);
                resim.rectTransform.anchorMax=new Vector2(0, 1);
                resim.rectTransform.anchoredPosition3D = new Vector3(0, 0, 0);
                resim.rectTransform.sizeDelta = avatar_buyuklugu;
                resim.rectTransform.localRotation = Quaternion.identity;
                resim.rectTransform.localScale=new Vector3(1,1,1);
                resim.sprite = drone11c_avatar;



                }
            if(fuzeci_sayisi != 0)
                {
                cesit++;

                Transform kanvas = gostergeler[j].transform.Find("yazý_canvasý");
                GameObject avatar = new GameObject("avatar");
                avatar.AddComponent<Image>();
                Image resim = avatar.GetComponent<Image>();
                avatar.transform.SetParent(kanvas);
                resim.rectTransform.anchorMin=new Vector2(0, 1);
                resim.rectTransform.anchorMax=new Vector2(0, 1);
                resim.rectTransform.anchoredPosition3D = new Vector3(0, 0, 0);
                resim.rectTransform.sizeDelta = avatar_buyuklugu;
                resim.rectTransform.localRotation = Quaternion.identity;
                resim.rectTransform.localScale=new Vector3(1,1,1);
                resim.sprite = fuzeci_avatar;


                }
            if(mayinci_sayisi != 0)
                {
                cesit++;

                Transform kanvas = gostergeler[j].transform.Find("yazý_canvasý");
                GameObject avatar = new GameObject("avatar");
                avatar.AddComponent<Image>();
                Image resim = avatar.GetComponent<Image>();
                avatar.transform.SetParent(kanvas);
                resim.rectTransform.anchorMin=new Vector2(0, 1);
                resim.rectTransform.anchorMax=new Vector2(0, 1);
                resim.rectTransform.anchoredPosition3D = new Vector3(0, 0, 0);
                resim.rectTransform.sizeDelta = avatar_buyuklugu;
                resim.rectTransform.localRotation = Quaternion.identity;
                resim.rectTransform.localScale=new Vector3(1,1,1);
                resim.sprite = mayinci_avatar;



                }
            if(sonuncu_sayisi != 0)
                {
                cesit++;

                Transform kanvas = gostergeler[j].transform.Find("yazý_canvasý");
                GameObject avatar = new GameObject("avatar");
                avatar.AddComponent<Image>();
                Image resim = avatar.GetComponent<Image>();
                avatar.transform.SetParent(kanvas);
                resim.rectTransform.anchorMin=new Vector2(0, 1);
                resim.rectTransform.anchorMax=new Vector2(0, 1);
                resim.rectTransform.anchoredPosition3D = new Vector3(0, 0, 0);
                resim.rectTransform.sizeDelta = avatar_buyuklugu;
                resim.rectTransform.localRotation = Quaternion.identity;
                resim.rectTransform.localScale=new Vector3(1,1,1);
                resim.sprite = sonuncu_avatar;



                }

            satir = 0;
            sutun = 0;
            for (int i = 0; i < cesit; i++)
            {

                Transform avatar= gostergeler[j].transform.Find("yazý_canvasý");
                Image resim=  avatar.transform.GetChild(i).GetComponent<Image>();
                resim.rectTransform.anchoredPosition3D = new Vector3(sutun*35, satir * (-20),0 ) + avatar_baslama_yeri;

                sutun++;
                if (sutun==2)
                {
                    satir++;
                    sutun = 0;

                }

            }




           
        }








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
                float kat = 1 + 2.5f * (1 - (yakinlik / gosterge_araliklari));
                gostergeler[i].transform.localScale = new Vector3(0.08f * kat, 0.08f * kat, 0.08f * kat);
                gostergeler[i].transform.position = sabit[i] + new Vector3(0, (kat - 1) * 0.08f * gostergeler[i].GetComponentInChildren<RectTransform>().rect.width / 2, 0);
    






                //Debug.Log(gostergeler[i].GetComponentInChildren<RectTransform>().rect.width);renkler[renk_sirasi];



            }


                gostergeler[i].transform.GetChild(2).transform.GetChild(renk_sirasi).gameObject.SetActive(false);

            int sonraki = renk_sirasi + 1;

            if (sonraki==gosterge.transform.GetChild(2).transform.childCount)
            {
                sonraki = 0;
            }


                gostergeler[i].transform.GetChild(2).transform.GetChild(sonraki).gameObject.SetActive(true);



            if (yakinlik < gosterge_araliklari / 8)
            {
            dugme[i].transform.gameObject.SetActive(true);

                if (sahne)
                {
                    sahne = false;
                    episode = i;
                    PlayerPrefs.SetInt("episode", episode);





                    SceneManager.LoadScene((i+1).ToString());

                }


            }
            else
            {
                dugme[i].transform.gameObject.SetActive(false);

            }





        }


        if (renk_zamani < Time.time)
        {
            renk_zamani = Time.time + renk_araligi;
            renk_sirasi++; 
            if (renk_sirasi == gosterge.transform.GetChild(2).transform.childCount)
                {
                    renk_sirasi = 0;
                }
        }


        if (toplam_yol < 0) toplam_yol = 0;
        else if (toplam_yol > line[sira].Length) toplam_yol = line[sira].Length;

        Hareket_et(transform.gameObject, line[sira], kavis, toplam_yol);









        

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

         public void sahne_gec()
        {
        sahne = true;
        }





    }

