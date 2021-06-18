using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Instantiates an object a number of times and offsets each by a random ammount
public class ObjectSpawner : MonoBehaviour
{
    public int NumberOfObjects=1;
    public GameObject [] objects;
    // Start is called before the first frame update
    void Start()
    {
        for (int x=0; x<NumberOfObjects;x++){
            Vector3 pos = new Vector3(Random.Range(-10f,10f),0f,Random.Range(-10f,10f));
            GameObject obj= GameObject.Instantiate(objects[Random.Range(0,objects.Length)],pos,Quaternion.identity);
            obj.name = obj.name+x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
