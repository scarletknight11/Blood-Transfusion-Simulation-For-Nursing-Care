using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPanels : MonoBehaviour
{
    public GameObject GXM, consent;
    public void Switch()
    {
        if(GXM.activeInHierarchy)
        {
            GXM.SetActive(false);
            consent.SetActive(true);
        }
        else if(consent.activeInHierarchy)
        {
            consent.SetActive(false);
            GXM.SetActive(true);
        }
    }
}
