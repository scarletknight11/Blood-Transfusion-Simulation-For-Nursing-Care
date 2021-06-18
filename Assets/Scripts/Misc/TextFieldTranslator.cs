using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFieldTranslator : MonoBehaviour
{
    // Start is called before the first frame update
    public ObjectMessageHandler handler;
    TMP_InputField myText;
    void Start()
    {
        myText = transform.GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        handler.inputText = myText.text;
    }
}
