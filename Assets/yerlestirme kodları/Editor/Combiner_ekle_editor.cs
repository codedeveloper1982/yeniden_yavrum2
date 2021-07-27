using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SplineMesh
{
    [CustomEditor(typeof(Combiner_ekle))]

    public class Combiner_ekle_editor : Editor
{



        public override void OnInspectorGUI()
        {





            base.OnInspectorGUI();
            Combiner_ekle kopya = (Combiner_ekle)target;

            if (GUILayout.Button("cizden kopya al"))
            {

                kopya.birlestirme_ekle();
            }




        }








    }
}
