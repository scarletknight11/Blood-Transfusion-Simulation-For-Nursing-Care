using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsAndDoor  : MonoBehaviour
{
    public float doorWidth = 1f;
    public GameObject [] doors;

    // Start is called before the first frame update
    void Start()
    {
        Transform myChild;
        //if no current door, and we have door prefabs, make a door.
        if (this.transform.childCount==1)
            Destroy(transform.GetChild(0).gameObject);
        if (doors.Length>0)
            GameObject.Instantiate(doors[Random.Range(0,doors.Length)],transform);    
        myChild = transform.GetChild(0);
        myChild.localScale = new Vector3(doorWidth,1f,1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
