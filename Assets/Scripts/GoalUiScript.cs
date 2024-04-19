using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalUiScript : MonoBehaviour
{
    public GameObject goal1;
    public GameObject goal2;
    public mikeAi mike;
    // Start is called before the first frame update
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
        if (mike.currHP <= 0)
        {
            goal1.gameObject.SetActive(false);
            goal2.gameObject.SetActive(true);
        }
        else
        {
            goal1.gameObject.SetActive(true);
        }
    }
}
