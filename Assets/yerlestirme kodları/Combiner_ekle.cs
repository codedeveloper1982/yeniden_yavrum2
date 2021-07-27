using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SplineMesh { 
public class Combiner_ekle : MonoBehaviour
{

public void birlestirme_ekle()
    {

        gameObject.AddComponent(typeof(MeshFilter));
        gameObject.AddComponent(typeof(MeshRenderer));
        gameObject.AddComponent(typeof(MeshCollider));
        gameObject.AddComponent(typeof(Mesh_combiner));

            transform.GetComponent<MeshFilter>().mesh = transform.GetComponentInParent<ExampleSower>().prefab.GetComponent<MeshFilter>().mesh;
            transform.GetComponent<MeshRenderer>().material = transform.GetComponentInParent<ExampleSower>().prefab.GetComponent<MeshRenderer>().material;
    }
}
}