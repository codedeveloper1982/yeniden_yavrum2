using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Renk_degisimi : MonoBehaviour
{


    public Color[] renkler;
    private int renk_sirasi;
    private float renk_zamani, renk_araligi;

    void Start()
    {
        renk_sirasi = 1;
        renk_araligi = 5;
        renk_zamani = Time.time + renk_araligi;

    }

    // Update is called once per frame
    void Update()
    {
        if (renk_zamani<Time.time)
        {
        renk_zamani = Time.time + renk_araligi;
            Color renk = GetComponent<Image>().color;

        renk.a =1/(float)renk_sirasi ;
        renk_sirasi++;

            Debug.Log(renk_sirasi);
        }



        if (renk_sirasi==5)
        {
            renk_sirasi = 1;
        }




    }
}
