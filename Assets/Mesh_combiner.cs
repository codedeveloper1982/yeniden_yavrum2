using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class Mesh_combiner : MonoBehaviour
{
    // Start is called before the first frame update
    private float bekle;
    void Start()
    {
        bekle = Time.time + 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > bekle) { 
            CombineMesh();
            gameObject.isStatic = true;
            transform.GetComponent<Mesh_combiner>().enabled = false;

        }
    }

   private void CombineMesh()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);

            i++;
        }

        var meshfilter = transform.GetComponent<MeshFilter>();
        meshfilter.mesh = new Mesh();
        meshfilter.mesh.CombineMeshes(combine);
        GetComponent<MeshCollider>().sharedMesh = meshfilter.mesh;
        transform.gameObject.SetActive(true);

        transform.localScale = new Vector3(1, 1, 1);
        transform.rotation = Quaternion.identity;
        transform.position = new Vector3(0, -1, 0);



    }


}
