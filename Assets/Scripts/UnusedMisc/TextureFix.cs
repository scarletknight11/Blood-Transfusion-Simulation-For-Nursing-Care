using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureFix : MonoBehaviour
{
   // Scroll main texture based on time

    // Commenting out float scrollSpeed that is never used - TAR 1/6/2021
    //float scrollSpeed = 0.5f;
    Renderer rend;
    public GameObject scaleObject;

    void Start()
    {
        rend = GetComponent<Renderer> ();
    }

    void Update()
    {
        Vector3 s;

        s = scaleObject.transform.localScale;


        // Animates main texture scale in a funky way!
        //float scaleX = Mathf.Cos(Time.time) * 0.5f + 1;
        //float scaleY = Mathf.Sin(Time.time) * 0.5f + 1;
        float scaleX = s.x;
        float scaleY = s.y;
        rend.material.SetTextureScale("_MainTex", new Vector2(scaleX, scaleY));
    }
}
