using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBreak : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject brokenWall;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Break()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Renderer>().enabled = false;
        float[] i_pos = { -0.5f, 0f, .5f };
        float[] j_pos = { -0.5f, 0f, .5f };
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            Instantiate(brokenWall, transform.TransformPoint(i_pos[i],j_pos[j],0), this.transform.rotation, this.transform);
        }
        foreach (Transform t in transform)
        {
            t.localScale = new Vector3(0.3f, 0.3f, 0.1f);
            t.transform.rotation = Quaternion.Euler(Random.Range(-120f, 240f), Random.Range(-120f, 240f), Random.Range(-120f, 240f));
            t.GetComponent<Rigidbody>().useGravity = true;
            //t.GetComponent<Rigidbody>().Trig
            t.GetComponent<Collider>().enabled = true;
            t.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(0, 90), Random.Range(0, 90), Random.Range(0, 90)));
        }
    }
}
