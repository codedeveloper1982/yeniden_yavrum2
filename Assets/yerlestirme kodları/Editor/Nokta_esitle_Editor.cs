using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SplineMesh
{
    [CustomEditor(typeof(Noktalar_esitleme))]

    public class Nokta_esitle_Editor : Editor
    {

        public override void OnInspectorGUI()
        {





            base.OnInspectorGUI();
            Noktalar_esitleme kopya = (Noktalar_esitleme)target;

            if (GUILayout.Button("Eþitle"))
            {

                kopya.esitle();
            }




        }



    }
}
