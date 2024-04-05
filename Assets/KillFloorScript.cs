using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFloorScript : MonoBehaviour
{
    private GameObject FindEntityWithStats(GameObject obj)
    {
        CharacterStats stats = obj.GetComponent<CharacterStats>();
        if (stats != null)
        {
            return obj;
        }
        else
        {
            return FindEntityWithStats(obj.transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player has entered the kill floor");
            GameObject playerObj = FindEntityWithStats(other.gameObject);
            CharacterStats stats = playerObj.GetComponent<CharacterStats>();
            stats.TakeDamage(1000);
        }
    }
}
