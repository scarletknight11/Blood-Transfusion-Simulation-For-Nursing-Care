using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenu : MonoBehaviour
{
    public  Vector2 center = new Vector2(500,500); // position of center button
 public int radius = 125;  // pixels radius to center of button;
 public Texture centerButton;  
 public Texture [] normalButtons;// : Texture[];
 public Texture [] selectedButtons;// : Texture[];
 public string [] choices;
 

 
 private int ringCount;// : int; 
 private Rect centerRect;// : Rect;
 private Rect[] ringRects;// : Rect[];
 private float angle;// : float;
 private bool showButtons = false;
 private int index;// : int;
 
 void Start () {
     ringCount = normalButtons.Length;
     angle = 360.0f / ringCount;
     
     centerRect.x = center.x - centerButton.width  * 0.5f;
     centerRect.y = center.y - centerButton.height * 0.5f;
     centerRect.width = centerButton.width;
     centerRect.height = centerButton.height;
     
     ringRects = new Rect[ringCount];
     
     var w = normalButtons[0].width;
     var h = normalButtons[0].height;
     var rect = new Rect(0,0,w, h);
     
     var v = new Vector2(radius,0);
     
     for (var i = 0; i < ringCount; i++) {
         rect.x = center.x + v.x - w * 0.5f;
         rect.y = center.y + v.y - h * 0.5f;
         ringRects[i] = rect;
         v = Quaternion.AngleAxis(angle, Vector3.forward) * v;
     }
 }
 
    void OnGUI() {
        var e = Event.current;
        
        if (e.type == EventType.MouseDown && centerRect.Contains(e.mousePosition)) {
            showButtons = true;
            index = -1;
        }    
                
        if (e.type == EventType.MouseUp) {
            if (showButtons) {
                Debug.Log("User selected #"+index + ", " + choices[index]);
            }
            showButtons = false;
        }
            
        if (e.type == EventType.MouseDrag) {
            var v = e.mousePosition - center;
            var a = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            a += angle / 2.0f;
            if (a < 0) a = a + 360.0f;
    
            index = (int) (a / angle);
        }
          GUI.Box(centerRect, "Click Me");
        //GUI.DrawTexture(centerRect, centerButton);
    
        if (showButtons) {
            for (var i = 0; i < normalButtons.Length; i++) {
                if (i != index) 
                    GUI.Box(ringRects[i], choices[i]);
                    //GUI.DrawTexture(ringRects[i], normalButtons[i]);
                else{
                    GUI.DrawTexture(ringRects[i], selectedButtons[i]);
                    GUI.Box(ringRects[i], choices[i]);
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
