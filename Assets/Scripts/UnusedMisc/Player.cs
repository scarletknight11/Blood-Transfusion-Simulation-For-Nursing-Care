using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Joystick joystick;
    public float rotateSpeed = 15f;

    private void Update()
    {
        Vector3 rotation = transform.eulerAngles;

        rotation.x += joystick.Vertical * -rotateSpeed * Time.deltaTime;
        rotation.y += joystick.Horizontal * rotateSpeed * Time.deltaTime;

        transform.eulerAngles = rotation;
    }

}
