using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SalineDisplay : MonoBehaviour
{

    public TextMeshProUGUI saline;
    public ObjectMessageHandler tracker;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!tracker.trackTime)
        {
            saline.text = tracker.salineAmount.ToString();
        }
        else
        {
            saline.text = tracker.time.ToString();
        }

    }
}
