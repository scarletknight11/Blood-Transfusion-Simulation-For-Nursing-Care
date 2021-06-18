using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{

    public float speed = 1.0f;
    public GameObject selectionCan, monitorCan, paperCan, phoneCan, paper, phone;
    Transform camPos, paperPos, phonePos;
    Transform target, phoneTarget, paperTarget;
    bool camMoving = false, phoneMoving = false, paperMoving = false;
    bool sendCamBack = false, sendPaperBack = false, sendPhoneBack = false;
    // Start is called before the first frame update
    void Start()
    {
        selectionCan.SetActive(true);
        monitorCan.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RunChecks();
    }

    void RunChecks()
    {
        float step = speed * Time.deltaTime;
        if (camMoving)
        {
            selectionCan.SetActive(false);
            Camera.main.transform.position = Vector3.MoveTowards(camPos.position, target.position, step);
            if (Vector3.Distance(Camera.main.transform.position, target.position) < .001f)
            {
                camMoving = false;
                monitorCan.SetActive(true);
            }
        }

        if (sendCamBack)
        {
            monitorCan.SetActive(false);
            Camera.main.transform.position = Vector3.MoveTowards(camPos.position, target.position, step);
            if (Vector3.Distance(Camera.main.transform.position, target.position) < .001f)
            {
                sendCamBack = false;
                selectionCan.SetActive(true);
            }
        }

        if (paperMoving)
        {
            selectionCan.SetActive(false);
            paper.transform.rotation = paperTarget.rotation;
            paper.transform.position = Vector3.MoveTowards(paperPos.position, paperTarget.position, step);
            if (Vector3.Distance(paper.transform.position, paperTarget.position) < .001f)
            {
                paperMoving = false;
                paperCan.SetActive(true);
            }
        }

        if (sendPaperBack)
        {
            paperCan.SetActive(false);
            paper.transform.rotation = paperTarget.rotation;
            paper.transform.position = Vector3.MoveTowards(paperPos.position, paperTarget.position, step);
            if (Vector3.Distance(paper.transform.position, paperTarget.position) < .001f)
            {
                sendPaperBack = false;
                selectionCan.SetActive(true);
            }
        }

        if (phoneMoving)
        {
            selectionCan.SetActive(false);
            phone.transform.rotation = phoneTarget.rotation;
            phone.transform.position = Vector3.MoveTowards(phonePos.position, phoneTarget.position, step);
            if (Vector3.Distance(phone.transform.position, phoneTarget.position) < .001f)
            {
                phoneMoving = false;
                phoneCan.SetActive(true);
            }
        }

        if (sendPhoneBack)
        {
            phoneCan.SetActive(false);
            phone.transform.rotation = phoneTarget.rotation;
            phone.transform.position = Vector3.MoveTowards(phonePos.position, phoneTarget.position, step);
            if (Vector3.Distance(phone.transform.position, phoneTarget.position) < .001f)
            {
                sendPhoneBack = false;
                selectionCan.SetActive(true);
            }
        }
    }

    public void SetTargetandMove(Transform t)
    {
        camPos = Camera.main.transform;
        target = t;
        camMoving = true;
    }

    public void SendBackCamera(Transform t)
    {
        camPos = Camera.main.transform;
        target = t;
        sendCamBack = true;
    }

    public void MovePapers(Transform t)
    {
        paperPos = paper.transform;
        paperTarget = t;
        paperMoving = true;
    }

    public void SendBackPapers(Transform t)
    {
        paperPos = paper.transform;
        paperTarget = t;
        sendPaperBack = true;
    }

    public void MovePhone(Transform t)
    {
        phonePos = phone.transform;
        phoneTarget = t;
        phoneMoving = true;
    }

    public void SendBackPhone(Transform t)
    {
        phonePos = phone.transform;
        phoneTarget = t;
        sendPhoneBack = true;
    }
}
