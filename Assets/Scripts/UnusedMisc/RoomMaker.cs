using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMaker : MonoBehaviour
{
    Transform nw,sw,ew,ww;  //4 walls
    Transform wh,ph;  //holders

    // Start is called before the first frame update
    void Start()
    {
        wh = FindDescendant("WallHolder");
        ph = FindDescendant("PlaceHolder");
        ph.gameObject.SetActive(false); //turn off placeholder
        wh.gameObject.SetActive(true);  //turn on walls
        //get my scale
        Vector3 scale = transform.localScale;
        transform.localScale = new Vector3(1f,1f,1f);


        //move and scale walls
        nw = FindDescendant("Wall_N");
        sw = FindDescendant("Wall_S");
        ew = FindDescendant("Wall_E");
        ww = FindDescendant("Wall_W");

        nw.localPosition = new Vector3(0f,0f,0f);
        sw.localPosition = new Vector3(scale.x,0f,-scale.z);
        ew.localPosition = new Vector3(scale.x,0f,0f);
        ww.localPosition = new Vector3(0f,0f,-scale.z);

        nw.localScale = new Vector3(scale.x,1f,1f);
        sw.localScale = new Vector3(scale.x,1f,1f);
        ew.localScale = new Vector3(scale.z,1f,1f);
        ww.localScale = new Vector3(scale.z,1f,1f);
        
    }

    //Look through all the object's children and grandchildren etc. for a particular name 
    public Transform FindDescendant(string name)
    {
      Transform[] children = transform.GetComponentsInChildren<Transform> (true);
      foreach (var child in children) {
        //print("childname= "+child.name);
        if (child.name == name) {
            //print("Found!");
            return child;  
        }        
      } 
      //print("Not Found!");
  
      return null;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
