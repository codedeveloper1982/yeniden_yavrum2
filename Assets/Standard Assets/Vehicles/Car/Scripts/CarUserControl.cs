using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
        public bool control_degis, ileriye_git,geriye_git,saga_don,sola_don,carpat;
        public AudioSource  ses2;//,ses3;ses1,
        private float  en_alt_ses2, en_ust_ses2;//, en_alt_ses3, en_ust_ses3;en_alt_ses1, en_ust_ses1,
        private float revs;
        private float  picth2,hedef_pic, pic_miktar;//picth1,






        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();


           // en_alt_ses1 = 0.6f;
           // en_ust_ses1 = 1.0f;
            en_alt_ses2 = 0.8f;
            en_ust_ses2 = 1.3f;
           // en_alt_ses3 = .6f;
           // en_ust_ses3 = 1.0f;
            carpat = false;
            revs = 20;
            //picth1 = en_alt_ses1 + (en_ust_ses1 - en_alt_ses1) * (revs);
            picth2 = 1;

        }


        private void FixedUpdate()
        {
            // pass the input to the car!,

            if (carpat==true)
            {
                h = 0;
                v = 0;

            }
            else
            {

            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
            }
            revs = m_Car.CurrentSpeed / m_Car.MaxSpeed;

            /* if (control_degis == false)
             {
                 //h = CrossPlatformInputManager.GetAxis("Horizontal");
                 //v = CrossPlatformInputManager.GetAxis("Vertical");
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
             }*/

            eksilen_donme = 21 * (m_Car.CurrentSpeed / m_Car.MaxSpeed);

            donme = 40 - eksilen_donme;
            speed = m_Car.CurrentSpeed;


           


 

            if (revs < 1)
            {


            }
            else if (revs>1)
            {

                   if(Mathf.Abs(picth2-hedef_pic)<0.05f)
            {
                float sayi = UnityEngine.Random.Range(0.0f, 0.4f);
                picth2 = hedef_pic - sayi;
                en_ust_ses2 = UnityEngine.Random.Range(0.5f, 0.8f);
                en_ust_ses2 = en_alt_ses2 + en_ust_ses2;



            }        
            }

           //   picth1= en_alt_ses1+(en_ust_ses1-en_alt_ses1)*(revs);
            hedef_pic =en_alt_ses2+(en_ust_ses2-en_alt_ses2)*(revs) ;         


           // ses1.pitch=Mathf.Lerp(ses1.pitch, picth1, 0.05f);
           // ses1.volume = 1 - revs;
            if (1 - revs > 0.005f) pic_miktar = 1 - revs;
            else if (1 - revs < 0.01f) pic_miktar = 0.003f;

            picth2=Mathf.Lerp(picth2, hedef_pic, pic_miktar);
            ses2.pitch = picth2;
            ses2.volume = 1;


            //ses3.pitch=en_alt_ses3+(en_ust_ses3-en_alt_ses3)*(m_Car.CurrentSpeed / m_Car.MaxSpeed);
            //ses3.volume = 1 - ((15-m_Car.CurrentSpeed)*(15-m_Car.CurrentSpeed)/40);


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
