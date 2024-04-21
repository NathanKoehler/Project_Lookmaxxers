using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ike_anim : MonoBehaviour
{
    private Animator animator;
    Vector3 previousPosition;


    // Start is called before the first frame update
    void Start()
    {
        // agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        update_anim_params();

    }

    private void update_anim_params()
    {
        // Calculate velocity
        Vector3 currentPosition = transform.position;
        Vector3 velocity = (currentPosition - previousPosition) / Time.deltaTime;

        // Store current position as previous position for the next frame
        previousPosition = currentPosition;

        // Get the forward vector of the GameObject
        Vector3 forwardVector = transform.forward;

        // Define an arbitrary vector pointing in a different direction
        Vector3 arbitraryVector = Vector3.up; // You can use any vector that is not collinear with the forward vector

        // Calculate a vector perpendicular to the forward vector
        Vector3 prep_vec = Vector3.Cross(forwardVector, arbitraryVector);


        float velx = Vector3.Dot(prep_vec, velocity);
        float vely = Vector3.Dot(transform.forward, velocity);
        animator.SetFloat("voly", vely);
        animator.SetFloat("volx", velx);
    }
}
