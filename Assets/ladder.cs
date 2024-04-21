using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ladder : MonoBehaviour
{
    // Start is called before the first frame update
    MeshRenderer rend;
    BoxCollider collider;
    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        rend.enabled = false;
        collider = GetComponent<BoxCollider>();
        collider.enabled = false; 
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void moveLadder()
    {
        //transform.position = new Vector3(-3.52f, 0.98f, -3.33f);
        rend.enabled = true;
        collider.enabled = true;
    }
}
