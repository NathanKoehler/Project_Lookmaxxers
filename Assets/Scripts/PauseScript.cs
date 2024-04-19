using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{

    bool paused = false;

    //// Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        GameManager.Instance.paused = false;
        GameManager.Instance.pauseMenu.interactable = false;
        GameManager.Instance.pauseMenu.blocksRaycasts = false;
        GameManager.Instance.pauseMenu.alpha = 0;
    }

    public void handlePause()
    {
        Time.timeScale = 0;
        GameManager.Instance.paused = true;
        GameManager.Instance.GameUI.gameObject.SetActive(false);
        GameManager.Instance.pauseMenu.interactable = true;
        GameManager.Instance.pauseMenu.blocksRaycasts = true;
        GameManager.Instance.pauseMenu.alpha = 1;

        Cursor.lockState = CursorLockMode.None;
    }

    public void handleUnpause()
    {
        Time.timeScale = 1;
        GameManager.Instance.paused = false;
        GameManager.Instance.GameUI.gameObject.SetActive(true);
        GameManager.Instance.controls.gameObject.SetActive(false);
        GameManager.Instance.pauseMenu.interactable = false;
        GameManager.Instance.pauseMenu.blocksRaycasts = false;
        GameManager.Instance.pauseMenu.alpha = 0;

        Cursor.lockState = CursorLockMode.Locked;
    }

    //// Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.paused) {
            if (Input.GetKeyUp(KeyCode.Escape)) {
                handleUnpause();
            }
        } else {
            if (Input.GetKeyUp(KeyCode.Escape)) {
                handlePause();
            }
        }
    }
}
