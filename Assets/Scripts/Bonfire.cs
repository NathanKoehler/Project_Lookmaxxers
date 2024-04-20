using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    private void Start()
    {
    }

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

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            TextDisplayHandler.instance.updateText("");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject player = FindPlayerWithStats(other.gameObject);
            TextDisplayHandler.instance.updateText("Press [F] to activate checkpoint");

            if (Input.GetKeyDown(KeyCode.F))
            {
                player.GetComponent<CharacterStats>().Rest(this);
            }
        }
    }
}
