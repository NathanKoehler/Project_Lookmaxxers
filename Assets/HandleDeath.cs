using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleDeath : MonoBehaviour
{

    private CanvasGroup cg;
    public GameObject player;
    private CharacterStats playerStats;
    private float displayTimer = 4f;
    private float time = 0f;
    private bool displayText;

    [SerializeField] private AudioClip deathSFX;

    //private bool showDeathScreen; 

    // Start is called before the first frame update
    void Start()
    {
        cg = GetComponent<CanvasGroup>();
        cg.alpha = 0.0f;
        displayText = false;
        playerStats = player.GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {

        if (time > 0f)
        {
            time -= Time.deltaTime;
            displayText = true;
            Debug.Log("running here"+ displayText);
        } else
        {
            displayText = false;
        }
        

        if (displayText)
        {
            cg.alpha = 1.0f;
        } else
        {
            cg.alpha = 0.0f;
        }
    }

    public void ShowDeathScreen()
    {
        time = displayTimer;
        SoundManager.instance.PlaySoundClip(deathSFX, transform, 1f);
    }

}
