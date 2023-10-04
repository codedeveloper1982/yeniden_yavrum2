using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace SplineMesh
{
    [CustomEditor(typeof(Dortlu_kontrol))]
    public class Dortlu_kontrol_editor : Editor
    {



        public override void OnInspectorGUI()
        {

            base.OnInspectorGUI();
            Dortlu_kontrol ekle = (Dortlu_kontrol)target;


            if (GUILayout.Button("gönder"))
            {

                ekle.gonder();

            }










        }



    }
}