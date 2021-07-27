using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SplineMesh
{
    [CustomEditor(typeof(Cizgi_kopyalama))]

    public class Cizgi_kopyalama_Editor : Editor
    {




        public override void OnInspectorGUI()
        {





            base.OnInspectorGUI();
            Cizgi_kopyalama kopya = (Cizgi_kopyalama)target;

            if (GUILayout.Button("cizden kopya al"))
            {

                kopya.cizgi_copy();
            }




        }








    }
}
