using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace SplineMesh
{
[CustomEditor(typeof(Yol_bitisleri))]
    public class Yol_bitisleri_editor : Editor
    {
        

        public override void OnInspectorGUI()
        {





            base.OnInspectorGUI();
            Yol_bitisleri bitis_yol = (Yol_bitisleri)target;

            if (GUILayout.Button("sona su ekle"))
            {
                bitis_yol.deniz_ekle();
            }
            if (GUILayout.Button("sona dað ekle"))
            {
                bitis_yol.dag_ekle();
            }
        }




    }
}
