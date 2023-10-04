using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;

public class Dortlu_kontrol : MonoBehaviour
{
    public float konum;

    public void gonder()
    {
        int cocuk = transform.childCount;


        for (int i = 0; i < cocuk; i++)
        {

            Transform cocuklar = transform.GetChild(i);

            cocuklar.GetComponent<Examle_benim>().rate = konum;

        }




    }
}
