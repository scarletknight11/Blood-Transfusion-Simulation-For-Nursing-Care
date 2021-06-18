using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class local : MonoBehaviour
{
    public GameObject grass;
    bool touch = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (touch == true)
        {
            Debug.Log("touched");
            grass.transform.localScale = new Vector3(transform.localScale.x * 2, transform.localScale.y * 2, transform.localScale.z * 2);

        }
    }


    private void OnCollisionEnter3D(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            touch = true;

        }

    }
}
