/** CameraRaycastTest.cs
 * <summary>
 * Test script for raycasting. Place on main camera object. Debug draw raycast will appear in
 * Scene view in editor wherever the mouse is pointing in the Game view. Clicking on an object
 * with collider will print to the console the name of the object clicked on.
 * </summary>
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycastTest : MonoBehaviour
{
    // Keep track of where the player clicks in the game world
    private RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        // Shoot a raycast from the camera to where player is pointing
        Ray tempRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        // If that raycast hit something...
        if (Physics.Raycast(tempRay, out hit, 10000))
        {
            // Draw a line in the game that follows the ray
            Debug.DrawLine(tempRay.origin, hit.point, Color.cyan);
            // Did the player click the left mouse button?
            if (Input.GetMouseButtonDown(0))
            {
                // If the spawn list is empty, stop
                if (hit.transform.gameObject != null)
                {
                    Debug.Log("-=-CAMERA RAYCAST TEST-=- CLICKED ON OBJECT: " + hit.transform.gameObject.name);
                }
            }
        }
    }
}
