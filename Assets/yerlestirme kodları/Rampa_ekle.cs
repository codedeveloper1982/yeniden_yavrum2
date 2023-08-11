using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rampa_ekle : MonoBehaviour
{
    private GameObject prefab;


    void Start()
    {
        Destroy(this);

    }

    public void rampa()
    {
        GameObject newObj = new GameObject("rampa");
        prefab = GameObject.Find("levha");
        newObj = Instantiate(prefab);
        newObj.name = "rampa";
        newObj.transform.SetParent(gameObject.transform);
        newObj.transform.localPosition = Vector3.zero;
        newObj.transform.localRotation = Quaternion.identity;
        newObj.transform.localScale = new Vector3(1, 1, 1);

    }

    public void rampa_cevir()
    {
        int count = transform.childCount;

        if (count>0)
        {
        GameObject altobje = transform.GetChild(0).gameObject;
        float ters_duz =altobje.transform.localScale.x;
            if (ters_duz==1)
            {
                ters_duz = -1;
            }
            else
            {
                ters_duz = 1;
            }
            altobje.transform.localScale = new Vector3(ters_duz, 1, 1);
        }
        else
        {
            return;
        }


    }



    public void rampa_sil()
    {
        int count = transform.childCount;

        if (count > 0)
        {
            GameObject altobje = transform.GetChild(0).gameObject;
            DestroyImmediate(altobje);

        }
        else
        {
            return;
        }


    }

    /*
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}
