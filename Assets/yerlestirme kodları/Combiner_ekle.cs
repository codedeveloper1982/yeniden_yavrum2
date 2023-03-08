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

            UnityEditorInternal.ComponentUtility.CopyComponent(transform.GetComponentInParent<ExampleSower>().prefab.GetComponent<MeshFilter>());
            UnityEditorInternal.ComponentUtility.PasteComponentValues(transform.GetComponent<MeshFilter>());

            UnityEditorInternal.ComponentUtility.CopyComponent(transform.GetComponentInParent<ExampleSower>().prefab.GetComponent<MeshRenderer>());
            UnityEditorInternal.ComponentUtility.PasteComponentValues(transform.GetComponent<MeshRenderer>());
    }
}
}