using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UITextShower : MonoBehaviour
{
    public GameObject scoreTracker;
    public Text scoreText/*, scoreText2*/;
    public Text missedText/*, missedText2*/;
    public Text CompleteorFail;
    public int totalMissed;
    public bool perfect, good, bad;
    public bool missesCounted = false;
   
    void Start()
    {
        
    }

    void Update()
    {
        if (!perfect && !good && !bad)
            return;

        if(perfect)
        {
            PerfectEnd();
        }
        else if(good)
        {
            GoodEnd();
        }
        else if(bad)
        {
            BadEnd();
        }
        else
        {
            return;
        }
        perfect = good = bad = false;
    }

    public void DisplayText()
    { 
            missedText.text += "You missed " + totalMissed + " possible check(s)!";
    }  
    
    public void CommonEnd()
    {
        var scorer = scoreTracker.GetComponent<ObjectMessageHandler>();
        if (!missesCounted)
        {
            for (int i = 0; i < scoreTracker.GetComponent<ObjectMessageHandler>().stages.Count; i++)
            {
                if (scoreTracker.GetComponent<ObjectMessageHandler>().stages[i] <0) //== false)
                {
                    totalMissed++;
                }
            }
            missesCounted = true;
        }
        scoreText.text = "ACCURACY: " + (int)scorer.percentComplete + "%";
        missedText.text = "You  answered " + (int)scorer.pointTotal 
        + " out of " + scorer.stages.Count + " correct"
        + " and missed answering " + totalMissed + " possible check(s)!";

    }
    public void PerfectEnd()
    {
        CommonEnd();
        CompleteorFail.text = "SUCCESS";
        CompleteorFail.color = Color.green;
    }

    public void GoodEnd()
    {
        CommonEnd();
        CompleteorFail.text = "SUCCESS";
        CompleteorFail.color = Color.green;
    }

    public void BadEnd()
    {
        CommonEnd();
        CompleteorFail.text = "FAILURE";
        CompleteorFail.color = Color.red;
    }
}
