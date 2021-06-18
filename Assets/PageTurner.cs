using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageTurner : MonoBehaviour
{
    public GameObject [] pages;
    public int currentPage=0;

    // Start is called before the first frame update
    void Start()
    {
        currentPage = 0;
        ActivePage();
    }

    void ActivePage()
    {
        for (int i=0; i< pages.Length; i++)
        {
//            pages[i].SetActive(i==currentPage);
//            pages[i].transform.GetChild(0).gameObject.SetActive(i==currentPage);
            pages[i].SetActive(i==currentPage);
        }
    }
    public void NextPage()
    {
        print("Next page");
        currentPage++;
        if (currentPage >= pages.Length)
            currentPage = 0;
        ActivePage();
    }

    public void PreviousPage()
    {
        print("Previous page");
        currentPage--;
        if (currentPage< 0) 
            currentPage = pages.Length - 1;
        ActivePage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
