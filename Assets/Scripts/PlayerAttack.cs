using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    private CharacterStats characterStats;

    public GameObject weaponRoot;
    private WeaponScript weaponScript;
    private PlayerMetrics playerMetrics;
    void Start()
    {
        animator = GetComponent<Animator>();
        characterStats = GetComponent<CharacterStats>();

        weaponScript = weaponRoot.GetComponent<WeaponScript>();
        playerMetrics = GetComponent<PlayerMetrics>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && characterStats.CanAttack())
        {
            Attack();
            playerMetrics.weaponUsed[weaponScript.currSelectedWeapon]++;
        }
    }

    public void Attack()
    {
        // Set the boolean parameter to false
        animator.SetTrigger("Attack");
    }
}

