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
        public bool control_degis, ileriye_git,geriye_git,saga_don,sola_don;
        private AudioSource ses;
        private float en_alt_ses, en_ust_ses;




        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();

            ses = GetComponent<AudioSource>();
            en_alt_ses = .6f;
            en_ust_ses = 2;
        }


        private void FixedUpdate()
        {
            // pass the input to the car!

            if (control_degis == false)
            {
                h = CrossPlatformInputManager.GetAxis("Horizontal");
                v = CrossPlatformInputManager.GetAxis("Vertical");
            }
            else
            {
                if (m_Car.CurrentSpeed > m_Car.MaxSpeed - 5 && geriye_git == false)
                {
                    v = 1;
                }
                else { 

                if (ileriye_git == true)
                {
                    v = 1;

                }
                else if (geriye_git == true) v = -1;
                else
                {
                    v = 0;
                }
            }
                if (saga_don == true) h = 1;
                else if (sola_don == true) h = -1;
                else h = 0;
            }

            speed = m_Car.CurrentSpeed;

            eksilen_donme = 22 * (m_Car.CurrentSpeed / m_Car.MaxSpeed);

            donme = 30 - eksilen_donme;

            ses.pitch=en_alt_ses+(en_ust_ses-en_alt_ses)*(m_Car.CurrentSpeed / m_Car.MaxSpeed);


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
