﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SplineMesh {
    /// <summary>
    /// Example of component to places assets along a spline. This component can be used as-is but will most likely be a base for your own component.
    /// 
    /// In this example, the user gives the prefab to place, a spacing value between two placements, the prefab scale and an horizontal offset to the spline.
    /// These three last values have an additional range, allowing to add some randomness. for each placement, the computed value will be between value and value+range.
    /// 
    /// Prefabs are placed from the start of the spline at computed spacing, unitl there is no lentgh remaining. Prefabs are stored, destroyed
    /// and built again each time the spline or one of its curves change.
    /// 
    /// A random seed is used to obtain the same random numbers at each update. The user can specify the seed to test some other random number set.
    /// 
    /// Place prefab along a spline and deform it easily have a lot of usages if you have some imagination : 
    ///  - place trees along a road
    ///  - create a rocky bridge
    ///  - create a footstep track with decals
    ///  - create a path of firefly in the dark
    ///  - create a natural wall with overlapping rocks
    ///  - etc.
    /// </summary>
    [ExecuteInEditMode]
    [SelectionBase]
    [DisallowMultipleComponent]
    public class ExampleSower : MonoBehaviour {
        private GameObject generated;
        private Spline spline = null;
        private bool toUpdate = true;

        public GameObject prefab = null;
        public float scale = 1, scaleRange = 0;
        public float spacing = 1, spacingRange = 0;
        public float offset = 0, offsetRange = 0;
        public bool isRandomYaw = false;
        public int randomSeed = 0;
        public float baslangic =0, son=1;
        public bool dagit,sona_kadar, dik_dur;
        public bool iki_yana_da_koy;
        public float x_offset, z_offset;
        public bool iki_tarafa_da;
        private float baslat;

        private void OnEnable() {
            string generatedName = "generated by "+GetType().Name;
            var generatedTranform = transform.Find(generatedName);
            generated = generatedTranform != null ? generatedTranform.gameObject : UOUtility.Create(generatedName, gameObject);
           /* generated.AddComponent(typeof(MeshFilter));
            generated.AddComponent(typeof(MeshRenderer));
            generated.AddComponent(typeof(MeshCollider));
            generated.AddComponent(typeof(Mesh_combiner));*/
            spline = GetComponentInParent<Spline>();
            spline.NodeListChanged += (s, e) => {
                toUpdate = true;
                foreach (CubicBezierCurve curve in spline.GetCurves()) {
                    curve.Changed.AddListener(() => toUpdate = true);
                }
            };
            foreach (CubicBezierCurve curve in spline.GetCurves()) {
                curve.Changed.AddListener(() => toUpdate = true);
            }
        }

        private void OnValidate() {
            toUpdate = true;
        }

        private void Update() {
            if (toUpdate) {
                Sow();
                toUpdate = false;

            baslat = Time.time + 0.5f;
            }



/*

            if (baslat<Time.time)
            {
                Debug.Log("pasif hale gelmesi hazım");
                transform.GetComponent<ExampleSower>().enabled = false;
            }*/

        }

        public void Sow() {
            UOUtility.DestroyChildren(generated);

            UnityEngine.Random.InitState(randomSeed);
            if (spacing + spacingRange <= 0 ||
                prefab == null)
                return;
           
            if (sona_kadar)
            {
                baslangic = 0;
                son = spline.Length;

            }
                int taraf=1;
            float distance = baslangic;
            while (distance <= son) {
                CurveSample sample = spline.GetSampleAtDistance(distance);


                if (iki_tarafa_da)
                {
                    if (taraf==1)
                    {

                        taraf = -1;
                    }
                    else {
                        taraf = 1;
                    }
                }
                else
                {
                    taraf = 1;
                }

                GameObject go;
                go = Instantiate(prefab, generated.transform);
                go.transform.localRotation = Quaternion.identity;
                go.transform.localPosition = Vector3.zero;
                go.transform.localScale = Vector3.one;
                go.isStatic= true;

                // move along spline, according to spacing + random
                if (dagit)
                {
                    Vector3 kaydir;
                    kaydir.x = UnityEngine.Random.Range(-x_offset, x_offset);
                    kaydir.z =UnityEngine.Random.Range(-z_offset, z_offset);
                    go.transform.localPosition = sample.location;
                    go.transform.localPosition = new Vector3(go.transform.localPosition.x + kaydir.x, go.transform.localPosition.y, go.transform.localPosition.z + kaydir.z);
                }
                else if (iki_yana_da_koy)
                {
                    Vector3 kaydir;
                    kaydir.x = x_offset;
                    kaydir.z =z_offset;
                    go.transform.localPosition = sample.location;
                    go.transform.localPosition = new Vector3(go.transform.localPosition.x + kaydir.x*taraf, go.transform.localPosition.y, go.transform.localPosition.z + kaydir.z);
                }
                else
                {

                go.transform.localPosition = sample.location;
                }


                // apply scale + random
                float rangedScale = scale + UnityEngine.Random.Range(0, scaleRange);
                go.transform.localScale = new Vector3(rangedScale, rangedScale, rangedScale);
                // rotate with random yaw
                if (isRandomYaw) {
                    go.transform.Rotate(0,UnityEngine.Random.Range(-180, 180) ,UnityEngine.Random.Range(-180, 180)  );/*UnityEngine.Random.Range(-180, 180)*/
                }else if (dik_dur)
                {
                    go.transform.Rotate(0,UnityEngine.Random.Range(-180, 180) , 0);

                } else {
                    go.transform.rotation = sample.Rotation;
                }
                // move orthogonaly to the spline, according to offset + random
                var binormal = (Quaternion.LookRotation(sample.tangent, sample.up) * Vector3.right).normalized;
                var localOffset = offset*taraf + UnityEngine.Random.Range(0, offsetRange * Math.Sign(offset*taraf));
                localOffset *=  sample.scale.x;

                binormal *= localOffset;
                go.transform.position += binormal;
                distance += spacing + UnityEngine.Random.Range(0, spacingRange);
            }
        }
    }
}