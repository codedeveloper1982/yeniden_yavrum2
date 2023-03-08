using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace SplineMesh
{
    [CustomEditor(typeof(Malzeme_ekle))]

    public class Malzeme_ekle_Editor : Editor
    {
        public override void OnInspectorGUI()
        {

            base.OnInspectorGUI();
            Malzeme_ekle ekle = (Malzeme_ekle)target;


            if (GUILayout.Button("Aðaç ekle"))
            {

                ekle.agac_ekle();

            }
            if (GUILayout.Button("Dað ekle"))
            {

                ekle.dag_ekle();

            }



            if (GUILayout.Button("sona yamaç ekle"))
            {

                ekle.Sona_yamac_ekle();

            }

            if (GUILayout.Button("sona kýrýk köprü ekle"))
            {

                ekle.Sona_kirik_ekle();

            }
            if (GUILayout.Button("baþa kýrýk köprü ekle"))
            {

                ekle.basa_kirik_ekle();

            }

            if (GUILayout.Button("tünel ekle"))
            {

                ekle.tunel_ekle();

            }
            if (GUILayout.Button("köprü ekle"))
            {

                ekle.kopru_ekle();

            }
            if (GUILayout.Button("baþa yamaç ekle"))
            {

                ekle.basa_yamac_ekle();

            }
            if (GUILayout.Button("baþa dere kýyýsý"))
            {

                ekle.basa_dere();

            }
            if (GUILayout.Button("sona dere kýyýsý"))
            {

                ekle.sona_dere();

            }
            if (GUILayout.Button("baþa kasis ekle"))
            {
                ekle.b_kasis_ekle();
            }

            if (GUILayout.Button("sona kasis ekle"))
            {
                ekle.s_kasis_ekle();
            }
            if (GUILayout.Button("combiner ekle"))
            {
                ekle.Combiner_ekle();
            }


        }
    }
}
