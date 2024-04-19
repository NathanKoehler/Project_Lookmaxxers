using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    public GameObject wall;
    private bool timerStart;
    private float destroyTimer;
    // Start is called before the first frame update
    void Start()
    {
        destroyTimer = 10.0f;
        timerStart = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (timerStart)
        {
            destroyTimer -= 2.0f * Time.deltaTime;
            if (destroyTimer < 0f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attack"))
        {
            print("among us");
            timerStart = true; 
            GetComponent<Collider>().enabled = false;
            wall.GetComponent<WallBreak>().Break();
        }
    }
}
