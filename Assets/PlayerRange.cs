using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRange : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isNearEntity;
    public int gameGoal = -1;
    public IEntityStats player;


    void Start()
    {
        isNearEntity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isNearEntity && player.isDead)
        {
            isNearEntity = false;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!isNearEntity && collider.tag == "Player")
        {
            isNearEntity = true;
            if (gameGoal != -1)
            {
                GameManager.Instance.HandleChangeGoal(gameGoal);
            }
            player = FindEntityWithStats(collider.gameObject).GetComponent<IEntityStats>();
        }
    }

    private GameObject FindEntityWithStats(GameObject obj)
    {
        IEntityStats stats = obj.GetComponent<IEntityStats>();
        if (stats != null)
        {
            return obj;
        }
        else
        {
            return FindEntityWithStats(obj.transform.parent.gameObject);
        }
    }
}
