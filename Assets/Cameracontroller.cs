using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameracontroller : MonoBehaviour {

    public Transform car;

    public float distance;
    public float height;
    public float rotationdamping;
    public float heightdamping;
    public float zoomratio;
    public float defaultPOV;
    public float rotation_vector;
    [SerializeField] private float ileriye_alma;
   // public Transform on_kısım;


    private void FixedUpdate()
    {
        Vector3 local_velocity = car.InverseTransformDirection(car.GetComponent<Rigidbody>().velocity);
        if (local_velocity.z < -0.5f)
        {
            rotation_vector = car.eulerAngles.y+100;
        }
        else
        {
            rotation_vector = car.eulerAngles.y;

        }
        float accelaration = car.GetComponent<Rigidbody>().velocity.magnitude;


       // on_kısım.position = car.position+car.forward * ileriye_alma;
        //on_kısım.transform.eulerAngles.y = car.transform.eulerAngles.y;

        //Camera.main.fieldOfView = defaultPOV + accelaration * zoomratio * Time.deltaTime;

       /* if(car.transform.localRotation.x < -0.05f || car.transform.localRotation.x > 0.05f){

            height = 1.1f;
           // heightdamping=1.5f;

        }
        else
        {

            height = 1.2f;
        }*/



    }
    private void LateUpdate()
    {
        float wantedangle = rotation_vector;
        float wantedheight = car.position.y + height;
        float myangle = transform.eulerAngles.y;
        float myheight = transform.position.y;

        myangle = Mathf.LerpAngle(myangle, wantedangle, rotationdamping * Time.deltaTime);
        myheight = Mathf.LerpAngle(myheight, wantedheight, heightdamping * Time.deltaTime);


    
        Quaternion currentrotation = Quaternion.Euler(0, myangle, 0);
        transform.position = car.position; //new Vector3(car.transform.position.x, car.transform.position.y, car.transform.position.z+ileriye_alma);
        transform.position -= currentrotation * Vector3.forward * distance;
        Vector3 temp = transform.position;
        temp.y = myheight;
        transform.position = temp;
        transform.LookAt(car);
        transform.Rotate(ileriye_alma,0 , 0);

    }
}
