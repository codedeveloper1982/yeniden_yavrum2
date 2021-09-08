using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ates_denemesi : MonoBehaviour
{

    private GameObject ates;
    public GameObject obje;
    private float ates_vakti,ates_bekle=5,degisim_vakti,degistir=1;
    private int r = 1;
    private bool ates_et;




    // Start is called before the first frame update
    void Start()
    {
        ates = transform.GetChild(0).gameObject;
        ates.SetActive(false);
        ates_et = false;
        ates_vakti = Time.time + ates_bekle;
        degisim_vakti = Time.time + degistir;
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time > ates_vakti)
        {
            ates_et = true;

            if(Time.time>ates_vakti+ates_bekle) ates_vakti = Time.time + ates_bekle;

        }
        else { 
            ates_et = false;
            }
        if (ates_et)
        {
            if (Time.time > degisim_vakti)
            {
                degisim_vakti = Time.time + degistir;
                if (r == 1)
                {
                    r = -1;

                }
                else r = 1;

            }
            ates.SetActive(true);
            ates.transform.localPosition = new Vector3(3.89f * r, 0.27f, 6.32f);
        }
        else ates.SetActive(false);
        
    }
}
