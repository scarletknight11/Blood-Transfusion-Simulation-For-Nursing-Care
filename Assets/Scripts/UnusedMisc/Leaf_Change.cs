using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Leaf_Change : MonoBehaviour
{
 
  

    // Use this for initialization
    void Start()
    {

       var theColor = this.GetComponentInChildren<Renderer>();

        theColor.material.SetColor("_Color", Color.green);

        Debug.Log(theColor.material.color);

        


    }

    // Update is called once per frame
    void Update()
    {
     
    }
}
