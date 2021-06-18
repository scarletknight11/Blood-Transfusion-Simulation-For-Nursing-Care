using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeMeUnique : MonoBehaviour
{
    // Start is called before the first frame update
		Transform head, rightLeg, leftLeg;
    public      float speed = 2f;
    public     float stepsize = .5f;

    void Start()
    {
      //Random Rotation
      Vector3 eul = new Vector3(0f,Random.Range(-180f,180f),0f);
      this.transform.Rotate(eul);

      //Randomly scale the right leg
      rightLeg = FindDescendant("RLeg");
      if (rightLeg!=null){
        float scale = Random.Range(0.2f,1.1f);
        rightLeg.GetChild(0).localScale = new Vector3(scale,1f,scale);
            // Replacing deprecated RotateAroundLocal() - TAR 1/6/2021
            //rightLeg.RotateAroundLocal(Vector3.forward, Random.Range(-1f, 1f));
            rightLeg.Rotate(Vector3.forward * Random.Range(-1f, 1f), Space.Self);
        }
        leftLeg = FindDescendant("LLeg");

      //Randomly make some people have only one leg
      int isactive = Random.Range(0,1); 
      //print("isactive="+isactive);
      if (isactive > 0)
        leftLeg.gameObject.SetActive(false);
      speed = Random.Range(0f,3f);
		
    }

    //Look through all the object's children and grandchildren etc. for a particular name 
    public Transform FindDescendant(string name)
    {
      Transform[] children = transform.GetComponentsInChildren<Transform> ();
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
    //Swing the legs back and forth as if walking
    void Update()
    {
      float ang;

      //angle oscillates between -stepsize and + stepsize based on the time and speed variable
      ang = stepsize * Mathf.Sin(speed * Time.time);
//      print("ang="+ang);

      //get right leg rotation and set x-axis rot to ang
      var rrot = rightLeg.transform.rotation;
      rrot.x = ang;
      rightLeg.transform.rotation = rrot;

      //get left leg rotation and set x-axis rot to -ang (so it moves opposite right leg)
      var lrot = leftLeg.transform.rotation;
      lrot.x = -ang;
      leftLeg.transform.rotation = lrot;

        
    }
}
