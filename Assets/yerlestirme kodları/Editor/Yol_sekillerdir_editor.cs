using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SplineMesh
{
    [CustomEditor(typeof(Yol_sekillendirme))]

    public class Yol_sekillerdir_editor: Editor
    {

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Yol_sekillendirme kopya = (Yol_sekillendirme)target;

            /*
           for (int a = 0; a < kopya.kopya_cizgiler.Length; a++) { 

                GUILayout.Label(kopya.kopya_cizgiler[a].name);
            GUILayout.BeginHorizontal();
           if (kopya.kopya_cizgiler[a]!= null) { 
            for(int i = 0; i <kopya.kopya_cizgiler[a].GetComponent<Spline>().nodes.Count-1 ; i++) { 
            if (GUILayout.Button("E�itle"+i))
            {
               kopya.alinan_cizgi = a;
               kopya.alinan_nokta = i;
               kopya.esitle();
            }
            }
            }

            GUILayout.EndHorizontal();

              



}  */


            if (GUILayout.Button("ileri nokta ekle"))
            {

                kopya.ileri_nokta_ekle();

            }
            GUILayout.Label(" son nokta ayarlar�");
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("a�a�� �ek"))
            {

                kopya.son_noktayi_asagi_indir();


            }
            if (GUILayout.Button("yukar� �ek"))
            {

                kopya.son_noktayi_yukari_cikar();


            }

            if (GUILayout.Button("sola �ek"))
            {

                kopya.son_noktayi_sola_cek();


            }            
            if (GUILayout.Button("sa� �ek"))
            {

                kopya.son_noktayi_saga_cek();


            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("sola d�nd�r"))
            {

                kopya.son_noktayi_sola_dondur();


            }
            if (GUILayout.Button("sa�a d�nd�r"))
            {

                kopya.son_noktayi_saga_dondur();


            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("ileri it"))
            {

                kopya.son_noktayi_ileri_it();


            }
            if (GUILayout.Button("geri it"))
            {

                kopya.son_noktayi_geri_it();


            }
            GUILayout.EndHorizontal();

            GUILayout.Label(" d�nen yol ayarlar�");
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("sola d�nen yol ekle"))
            {

                kopya.ileri_nokta_ekle();
                for (int i = 0; i <12; i++)
                {
                    kopya.son_noktayi_sola_cek();

                }
                for (int i = 0; i < 3; i++)
                {
                    kopya.son_noktayi_sola_dondur();

                }
            }
            if (GUILayout.Button("sa�a d�nen yol ekle"))
            {

                kopya.ileri_nokta_ekle();
                for (int i = 0; i < 12; i++)
                {
                    kopya.son_noktayi_saga_cek();

                }
                for (int i = 0; i < 3; i++)
                {
                    kopya.son_noktayi_saga_dondur();

                }
            }
            GUILayout.EndHorizontal();

            GUILayout.Label(" kavisli yol ekleme");
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("sola kavisli yol ekle"))
            {

                kopya.ileri_nokta_ekle();
                kopya.sola_kavis_ver();

            }
            if (GUILayout.Button("saga kavisli yol ekle"))
            {
                kopya.ileri_nokta_ekle();
                kopya.saga_kavis_ver();
            }
            GUILayout.EndHorizontal();

            GUILayout.Label("yolu e�im y�n�nde e�me veya d�zleme");
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("e�im y�n�ne d�n"))
            {

                kopya.egim_yone_don();

            }
            if (GUILayout.Button("d�zle"))
            {
                kopya.duzle();
            }
            GUILayout.EndHorizontal();



        }



    }
}