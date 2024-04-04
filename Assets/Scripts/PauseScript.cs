using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{

    public bool paused;

    //// Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        paused = false;
    }

    //// Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.paused) {
            if (Input.GetKeyUp(KeyCode.Escape)) {
                Time.timeScale = 1;
                GameManager.Instance.paused = false;
                GameManager.Instance.GameUI.gameObject.SetActive(true);
            }
        } else {
            if (Input.GetKeyUp(KeyCode.Escape)) {
                Time.timeScale = 0;
                GameManager.Instance.paused = true;
                GameManager.Instance.GameUI.gameObject.SetActive(false);
            }
        }
    }
}
