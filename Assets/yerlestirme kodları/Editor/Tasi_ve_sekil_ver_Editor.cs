using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



namespace SplineMesh
{
    [CustomEditor(typeof(Tasi_ve_sekil_ver))]

    public class Tasi_ve_sekil_ver_Editor : Editor
    {


        public override void OnInspectorGUI()
        {





            base.OnInspectorGUI();
            Tasi_ve_sekil_ver tasi_ve_sekil = (Tasi_ve_sekil_ver)target;

            if (GUILayout.Button("cizgiyi sona baðla"))
            {
                tasi_ve_sekil.Sona_tak();
            }
            if (GUILayout.Button("cizgiye þekil ver"))
            {
                tasi_ve_sekil.Sekil_ver();
            }
            if (GUILayout.Button("ilerlet"))
            {
                tasi_ve_sekil.ilerle();
            }
            if (GUILayout.Button("gerilet"))
            {
                tasi_ve_sekil.gerile();
            }
            if (GUILayout.Button("cizginin baþýna git"))
            {
                tasi_ve_sekil.cizginin_basina_bagla();
            }
            if (GUILayout.Button("yol ekle"))
            {
                tasi_ve_sekil.yol_ekle();
            }

        }






    }
}
