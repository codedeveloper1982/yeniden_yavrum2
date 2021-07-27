using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patla : MonoBehaviour
{
    public bool patla=false;


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "zemin")
        {

            patla = true;

        }
    }




}
