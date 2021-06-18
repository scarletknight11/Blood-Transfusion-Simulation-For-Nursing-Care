using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFPSControl : MonoBehaviour
{
    public float moveSpeed = 3;
    public float lookSpeed = 3;
    private Vector3 forwardMotion = Vector3.zero;
    private Vector3 sideMotion = Vector3.zero;
    private Vector2 rotation = Vector2.zero;
    private Rigidbody myRB;
    private Camera myCam;

    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myCam = GetComponentInChildren<Camera>();
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }

        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");
        rotation.x = Mathf.Clamp(rotation.x, -15f, 15f);
        transform.eulerAngles = new Vector2(0, rotation.y) * lookSpeed;
        myCam.transform.localRotation = Quaternion.Euler(rotation.x * lookSpeed, 0, 0);

        forwardMotion = transform.forward * moveSpeed * -(Input.GetAxisRaw("Vertical"));
        sideMotion = transform.right * moveSpeed * (Input.GetAxisRaw("Horizontal"));

        myRB.velocity = forwardMotion + sideMotion;
    }

    public void QuitGame()
    {
        Cursor.visible = true;
        Debug.Log("USER QUIT THE GAME");
        Application.Quit();
    }

}