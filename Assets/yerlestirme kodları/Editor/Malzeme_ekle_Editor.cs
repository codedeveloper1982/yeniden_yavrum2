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


            if (GUILayout.Button("A�a� ekle"))
            {

                ekle.agac_ekle();

            }
            if (GUILayout.Button("Da� ekle"))
            {

                ekle.dag_ekle();

            }



            if (GUILayout.Button("sona yama� ekle"))
            {

                ekle.Sona_yamac_ekle();

            }

            if (GUILayout.Button("sona k�r�k k�pr� ekle"))
            {

                ekle.Sona_kirik_ekle();

            }
            if (GUILayout.Button("ba�a k�r�k k�pr� ekle"))
            {

                ekle.basa_kirik_ekle();

            }

            if (GUILayout.Button("t�nel ekle"))
            {

                ekle.tunel_ekle();

            }
            if (GUILayout.Button("k�pr� ekle"))
            {

                ekle.kopru_ekle();

            }
            if (GUILayout.Button("ba�a yama� ekle"))
            {

                ekle.basa_yamac_ekle();

            }
            if (GUILayout.Button("ba�a dere k�y�s�"))
            {

                ekle.basa_dere();

            }
            if (GUILayout.Button("sona dere k�y�s�"))
            {

                ekle.sona_dere();

            }
            if (GUILayout.Button("ba�a kasis ekle"))
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
