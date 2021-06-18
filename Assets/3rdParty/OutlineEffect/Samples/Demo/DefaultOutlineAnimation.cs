using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

namespace cakeslice
{
    public class DefaultOutlineAnimation : MonoBehaviour
    {
        bool pingPong = false;
        Color origLineColor2;
        // Use this for initialization
        void Start()
        {
            origLineColor2 = GetComponent<OutlineEffect>().lineColor0;
        }

        // Update is called once per frame
        void Update()
        {
            Color c = GetComponent<OutlineEffect>().lineColor0;

            if(pingPong)
            {
                c.a += Time.deltaTime;

                if(c.a >= 1)
                    pingPong = false;
            }
            else
            {
                c.a -= Time.deltaTime;

                if(c.a <= 0)
                    pingPong = true;
            }

            c.a = Mathf.Clamp01(c.a);
            GetComponent<OutlineEffect>().lineColor0 = c;
            GetComponent<OutlineEffect>().UpdateMaterialsPublicProperties();
        }
    }
}