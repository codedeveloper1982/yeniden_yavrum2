using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof(CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use
        public GameObject car;
        public float speed_tork, reverse_tork;
        public float speed;
        public float donme,eksilen_donme,h,v,p;


        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();


        }


        private void FixedUpdate()
        {
            // pass the input to the car!
             h = CrossPlatformInputManager.GetAxis("Horizontal");
             v = CrossPlatformInputManager.GetAxis("Vertical");


            speed = m_Car.CurrentSpeed;

            eksilen_donme = 22 * (m_Car.CurrentSpeed / m_Car.MaxSpeed);

            donme = 30 - eksilen_donme;


            /*
            if (m_Car.CurrentSpeed > 9 && m_Car.CurrentSpeed < 14)
            {


                donme = 8;
            }
            else if (m_Car.CurrentSpeed > 14 && m_Car.CurrentSpeed < 25)
            {
                donme = 4;

            }
            else
            {
                donme = 30;

            }*/





#if !MOBILE_INPUT

            m_Car.Move(h, v, v);
#else
            m_Car.Move(h, v, v);
#endif
        }
    }
}
