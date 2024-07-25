using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace SplineMesh
{
    [CustomEditor(typeof(Harabe_ekle))]

    public class Harabe_ekle_Editor : Editor
    {
        public override void OnInspectorGUI()
        {

            base.OnInspectorGUI();
            Harabe_ekle ekle = (Harabe_ekle)target;

            if (GUILayout.Button("Küp ekle"))
            {

                ekle.kup_ekle();

            }








            if (GUILayout.Button("Mavi ekle"))
            {

                ekle.mavi_ekle();

            }
            if (GUILayout.Button("beyaz ekle"))
            {

                ekle.beyaz_ekle();

            }

            if (GUILayout.Button("kimizi ekle"))
            {

                ekle.kimizi_ekle();

            }
            if (GUILayout.Button("pasli ekle"))
            {

                ekle.pasli_ekle();

            }


            if (GUILayout.Button("dortlu ekle"))
            {

                ekle.dortlu_ekle();

            }









        }
    }
}
