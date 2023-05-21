using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace SplineMesh
{
    [CustomEditor(typeof(Kamera_denet))]

    public class Kamera_denet_editor : Editor
    {

        public override void OnInspectorGUI()
        {





            base.OnInspectorGUI();
            Kamera_denet denetle = (Kamera_denet)target;
/*
            if (GUILayout.Button("baþla"))
            {

                denetle.Basla();

            }
            
            if (GUILayout.Button("dur"))
            {

                denetle.dur();

            }


*/


        }





    }
}
