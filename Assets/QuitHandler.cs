using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
          
    }
}
