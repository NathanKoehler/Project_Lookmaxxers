using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAttackHeavy : MonoBehaviour
{
    private bool isMouseDown = false;
    private float mouseDownStartTime = 0f;
    public float holdDurationThreshold = 0.5f; // Time threshold for mouse hold (in seconds)
    private Animator animator;
    private CharacterStats characterStats;

    public GameObject weaponRoot;
    private WeaponScript weaponScript;
    private PlayerMetrics playerMetrics;

    void Start()
    {
        characterStats = GetComponent<CharacterStats>();
        animator = GetComponent<Animator>();

        weaponScript = weaponRoot.GetComponent<WeaponScript>();
        playerMetrics = GetComponent<PlayerMetrics>();
    }

    void Update()
    {
        // // Check if the left mouse button is pressed down
        if (Input.GetMouseButtonDown(1) && characterStats.CanAttack())
        {
            animator.SetTrigger("heavyAttack");
            playerMetrics.weaponUsed[weaponScript.currSelectedWeapon]++;
        }

        // // Check if the left mouse button is pressed down
        // if (Input.GetMouseButtonDown(0) && characterStats.CanAttack())
        // {
        //     isMouseDown = true;
        //     mouseDownStartTime = Time.time; // Record the time when the mouse button is pressed down
        // }

        // // Check if the left mouse button is released
        // if (Input.GetMouseButtonUp(0))
        // {
        //     isMouseDown = false;
        //     // Check if the hold duration exceeds the threshold
        //     if (Time.time - mouseDownStartTime >= holdDurationThreshold)
        //     {
        //         // Call a function when the left mouse button is released after holding for the threshold duration
        //         OnMouseHoldEnd();
        //     }
        // }

        // // Check if the left mouse button is being held down
        // if (isMouseDown)
        // {
        //     // Check if the hold duration exceeds the threshold
        //     if (Time.time - mouseDownStartTime >= holdDurationThreshold)
        //     {
        //         // Call a function while the left mouse button is being held down after holding for the threshold duration
        //         OnMouseHold();
        //     }
        // }
    }

    // Function called when the left mouse button is held down for the threshold duration
    //private void OnMouseHold()
    //{
    //    Debug.Log("Left mouse button held down for threshold duration");
    //    // Add your code here that you want to run when the left mouse button is held down for the threshold duration
    //}

    //// Function called when the left mouse button is released after holding for the threshold duration
    //private void OnMouseHoldEnd()
    //{
    //    Debug.Log("Left mouse button released after holding for threshold duration");
    //    // Add your code here that you want to run when the left mouse button is released after holding for the threshold duration
    //    animator.SetTrigger("heavyAttack");
}
