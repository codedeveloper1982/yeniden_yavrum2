using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SplineMesh
{
    public class Cizgi_kopyalama : MonoBehaviour
    {
        public GameObject spline1;
        public cizgi_cesidi cizgi_Cesidi;
        public enum cizgi_cesidi { birinci, ikinci, ucuncu, dorduncu,besinci,altinci,yedinci,sekizinci,dokuzuncu,onuncu,onbirinci };
        public Spline[] cizgiler;
        private int sayi;

        public void cizgi_copy()
        {
            if (cizgi_Cesidi == cizgi_cesidi.birinci) sayi = 0;
            if (cizgi_Cesidi == cizgi_cesidi.ikinci) sayi = 1;
            if (cizgi_Cesidi == cizgi_cesidi.ucuncu) sayi = 2;
            if (cizgi_Cesidi == cizgi_cesidi.dorduncu) sayi = 3;
            if (cizgi_Cesidi == cizgi_cesidi.besinci) sayi =4 ;
            if (cizgi_Cesidi == cizgi_cesidi.altinci) sayi =5 ;
            if (cizgi_Cesidi == cizgi_cesidi.yedinci) sayi =6 ;
            if (cizgi_Cesidi == cizgi_cesidi.sekizinci) sayi =7 ;
            if (cizgi_Cesidi == cizgi_cesidi.dokuzuncu) sayi =8 ;
            if (cizgi_Cesidi == cizgi_cesidi.onuncu) sayi =9 ;
            if (cizgi_Cesidi == cizgi_cesidi.onbirinci) sayi =10 ;

            UnityEditorInternal.ComponentUtility.CopyComponent(spline1.GetComponent<Spline>());
            UnityEditorInternal.ComponentUtility.PasteComponentValues(cizgiler[sayi]);
        }

    }
}
