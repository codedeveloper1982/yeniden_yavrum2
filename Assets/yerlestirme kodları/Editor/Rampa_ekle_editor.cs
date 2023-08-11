using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Rampa_ekle))]

public class Rampa_ekle_Editor : Editor
{
    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();
        Rampa_ekle ekle = (Rampa_ekle)target;


        if (GUILayout.Button("rampa ekle"))
        {

            ekle.rampa();

        }

        if (GUILayout.Button("rampayý ters çevir"))
        {

            ekle.rampa_cevir();

        }
        if (GUILayout.Button("rampa sil"))
        {

            ekle.rampa_sil();

        }

    }
}

