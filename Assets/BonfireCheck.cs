using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonfireCheck : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public int enemyNear;
    HashSet<GameObject> enemiesNearby; 
    void Start()
    {
        enemiesNearby = new HashSet<GameObject>();
        enemyNear = enemiesNearby.Count;
    }

    // Update is called once per frame
    void Update()
    {
        enemyNear = enemiesNearby.Count;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("FullEnemy"))
        {
            if (other.gameObject.GetComponent<EnemyStats>().curHealth > 0) enemiesNearby.Add(other.gameObject);
            else enemiesNearby.Remove(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FullEnemy"))
        {
            enemiesNearby.Remove(other.gameObject);
        }
    }
}
