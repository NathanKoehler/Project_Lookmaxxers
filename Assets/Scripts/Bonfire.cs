using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    private GameObject FindPlayerWithStats(GameObject obj)
    {
        CharacterStats stats = obj.GetComponent<CharacterStats>();
        if (stats != null)
        {
            return obj;
        }
        else
        {
            return FindPlayerWithStats(obj.transform.parent.gameObject);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            GameObject player = FindPlayerWithStats(other.gameObject);
            player.GetComponent<CharacterStats>().Rest(this);
        }
    }

    private void Update()
    {
        
    }
}
