using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpot : MonoBehaviour
{
    private bool triggered = false;
    private Renderer rend;
    private float c_a;
    private bool reduce_alpha; 
    // Start is called before the first frame update
    void Start()
    {
        triggered = false;
        rend = GetComponent<Renderer>();
        c_a = .3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!triggered)
        {
            Color c = rend.material.color;
            if (reduce_alpha)
            {
                c_a -= .4f * Time.deltaTime;
                reduce_alpha = c_a > .3f; 
            } else
            {
                c_a += .4f * Time.deltaTime;
                reduce_alpha = c_a > .8f; 
            }
            rend.material.color = new Color(1, 0, 0, c_a);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CubeTrigger"))
        {
            triggered = true;
            Color c = rend.material.color;
            rend.material.color = new Color(0.5f, 0.5f, 0.5f, 0.8f);
            rend.material.SetColor("_EmissionColor", Color.black);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CubeTrigger"))
        {
            triggered = false;
            rend.material.SetColor("_EmissionColor", new Color(0.2f, .14f, .14f));
        }
    }
}
