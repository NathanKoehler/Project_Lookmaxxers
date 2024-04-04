using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClick()
    {
        Debug.Log("Button Clicked");
        GameManager.Instance.controls.interactable = true;
        GameManager.Instance.controls.alpha = 1.0f;
        GameManager.Instance.controls.gameObject.SetActive(true);
        GameManager.Instance.pauseMenu.interactable = false;
        GameManager.Instance.pauseMenu.alpha = 0.0f;
        //canvas.gameObject.SetActive(true);
    }
}
