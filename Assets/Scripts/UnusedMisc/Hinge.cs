using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hinge : MonoBehaviour
{
    public float doorWidth = 1f;
    public GameObject [] doors;
    public bool swing=false;
    public      float speed = 2f;
    public     float stepsize = .5f;


    // Start is called before the first frame update
    void Start()
    {
        Transform myChild;
        //if no current door, and we have door prefabs, make a door.
        if (doors.Length>0)
            GameObject.Instantiate(doors[Random.Range(0,doors.Length)],transform);    
        myChild = transform.GetChild(1); 
        myChild.localScale = new Vector3(doorWidth,1f,1f);
        if (this.transform.childCount>1) //destroy placeholder door
            Destroy(transform.GetChild(0).gameObject); 
    }

    // Update is called once per frame
    void Update()
    {   
        if (swing)
        {
            
            var ang = stepsize * Mathf.Sin(speed * Time.time);
            var rrot = transform.rotation;
            rrot.y = ang;
            transform.rotation = rrot;

        }
    }
}
