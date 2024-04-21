using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{ 
    public int pickupNo;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject GetPlayer(GameObject obj)
    {
        CharacterStats stats = obj.GetComponent<CharacterStats>();
        if (stats != null)
        {
            return obj;
        }
        else
        {
            return GetPlayer(obj.transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject player = GetPlayer(other.gameObject);
            player.GetComponent<CharacterStats>().EnableWeapon(pickupNo);
            Destroy(this.gameObject);
        }
    }
}
