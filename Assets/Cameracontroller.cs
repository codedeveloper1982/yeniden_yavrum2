using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;


public class Cameracontroller : MonoBehaviour
    {

        private Transform car;
    private Transform takip;

        public float distance;
        public float height;
        public float rotationdamping;
        public float heightdamping;
        public float zoomratio;
        public float defaultPOV;
        public float rotation_vector;
        [SerializeField] private float ileriye_alma;
         private bool verial,arabapatladi,sola_degme_var=false,saga_degme_var=false;
    public bool ileriye_git;
    private RaycastHit saghit,arkahit,solhit;
    private float sola_donus=0,donus_hiz;
    private int sagsol;
    // public Transform on_kısım;
    private void Start()
    {
        car = GameObject.FindGameObjectWithTag("Player").transform;
        donus_hiz = .5f;
    }

    private void FixedUpdate()
        {
        verial = car.transform.GetComponent<CarController>().suya_dokundu;
        arabapatladi = car.transform.GetComponent<CarController>().fail;
        if (verial)
        {

            takip = car.transform.GetComponent<CarController>().sicratma.transform;
            height = 6;
        }else if (arabapatladi)
        {
            if (distance < 6) {
                distance += 0.02f;
                height += 0.01f;
            }
        }
        else
        {
            takip = car;
        Vector3 local_velocity = car.InverseTransformDirection(car.GetComponent<Rigidbody>().velocity);

            /*ısın_gonder(transform,transform.right*(-1),1f,saghit,sag_carpma);

             float uzaklık = Vector3.Distance(saghit.point, transform.position);
             if(sag_carpma)Debug.Log(saghit.collider.name);
 */


            // if (sola_donus > -100) sola_donus -= 1f;

           
            if (Physics.Raycast(transform.position, transform.right * (-1), out saghit, 3))
                {



                    sagsol = -1;
                
                saga_degme_var = true;
                
                }
                else if(Physics.Raycast(transform.position, transform.right , out solhit, 3))
                {
                


                    sagsol = 1;
                
                sola_degme_var= true;

                }
            else
            {
                saga_degme_var = false;
                sola_degme_var = false;
            }
                Debug.DrawRay(transform.position, transform.right * (-1) * 3, Color.green);
                Debug.DrawRay(transform.position, transform.right* 3, Color.green);
            if (Physics.Raycast(transform.position, transform.forward * (-1), out arkahit, 3))
            {
                if (distance > 0.3f) { distance -= 0.02f; }
            }
            else
            {
                if (distance < 3) distance += 0.02f;
            }
            Debug.DrawRay(transform.position, transform.forward * (-1) * 3, Color.green);



            if (local_velocity.z < -0.5f || sola_degme_var == true || saga_degme_var == true)
            {

                if (sola_donus < 100) sola_donus += donus_hiz;
                rotation_vector = car.eulerAngles.y+sola_donus*sagsol;
            }
            else
            {
                if (sola_donus > 0.5)
                {
                    if ((sola_degme_var == false && saga_degme_var == false))
                    {
                        sola_donus -= donus_hiz;
                    } 

                }
                else
                {
                    sola_donus = 0;
                }
                rotation_vector = car.eulerAngles.y+sola_donus * sagsol;

            }
        }







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
            float wantedheight = takip.position.y + height;
            float myangle = transform.eulerAngles.y;
            float myheight = transform.position.y;
             float mydistance = distance;

            myangle = Mathf.LerpAngle(myangle, wantedangle, rotationdamping * Time.deltaTime);
            myheight = Mathf.LerpAngle(myheight, wantedheight, heightdamping * Time.deltaTime);
            mydistance = Mathf.LerpAngle(mydistance, distance, heightdamping * Time.deltaTime);


            Quaternion currentrotation = Quaternion.Euler(0, myangle, 0);
            transform.position = takip.position; //new Vector3(car.transform.position.x, car.transform.position.y, car.transform.position.z+ileriye_alma);
            transform.position -= currentrotation * Vector3.forward * distance;
            Vector3 temp = transform.position;
            temp.y = myheight;
            transform.position = temp;
            transform.LookAt(takip);
            transform.Rotate(ileriye_alma, 0, 0);

        }



    public void ileri_down()
    {

        car.transform.GetComponent<CarUserControl>().ileriye_git = true;

    }
    public void ileri_up()
    {

        car.transform.GetComponent<CarUserControl>().ileriye_git = false;

    }
    public void geri_down()
    {

        car.transform.GetComponent<CarUserControl>().geriye_git = true;

    }
    public void geri_up()
    {

        car.transform.GetComponent<CarUserControl>().geriye_git = false;

    }
    public void saga_down()
    {

        car.transform.GetComponent<CarUserControl>().saga_don = true;

    }
    public void saga_up()
    {

        car.transform.GetComponent<CarUserControl>().saga_don = false;

    }
    public void sola_down()
    {

        car.transform.GetComponent<CarUserControl>().sola_don = true;

    }
    public void sola_up()
    {

        car.transform.GetComponent<CarUserControl>().sola_don = false;

    }

    public void ates_down()
    {

        car.transform.GetComponent<CarUserControl>().ates = true;

    }
    public void ates_up()
    {

         car.transform.GetComponent<CarUserControl>().ates= false;

    }

}

