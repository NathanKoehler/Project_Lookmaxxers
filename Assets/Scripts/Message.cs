using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Message : MonoBehaviour
{
    public string messageText;
    public TMP_Text UIText;


    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter() {
        UIText.text = messageText;
    }

    void OnTriggerExit() {
        UIText.text = "";
    }
}
