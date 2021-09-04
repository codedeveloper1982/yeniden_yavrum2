using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patlama_drone : MonoBehaviour
{
    public float minforce;
    public float maxforce;
    public float radius,sure,kaldir;



    // Start is called before the first frame update
    void Start()
    {
        sure = 5;
        kaldir = Time.time + sure;

        Explode();
    }

    private void Explode()
    {
        foreach(Transform t in transform)
        {
            var rb= t.GetComponent<Rigidbody>();

            if(rb != null)
            {   
                Vector3 patlama_noktasi = transform.position ;//- new Vector3(0, 0, 0)
                rb.AddExplosionForce(Random.Range(minforce, maxforce), patlama_noktasi, radius);
            }


        }

    }


}
