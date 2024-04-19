using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextDisplayHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text UIText;
    public static TextDisplayHandler instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        UIText = GetComponent<TMP_Text>();
        Debug.Log("TEXT" + UIText.text);
    }

    public void updateText(string t)
    {
        UIText.text = t;
    }
}
