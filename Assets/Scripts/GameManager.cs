using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public bool paused { get; set; }
    public bool toControlScreen { get; set; }
    public GameObject GameUI;
    public CanvasGroup controls;
    public CanvasGroup pauseMenu;

    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
            {
                Debug.Log("error");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        controls.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
