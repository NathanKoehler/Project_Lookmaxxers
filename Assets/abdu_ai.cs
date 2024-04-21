using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.AI;

public class abdu_ai : MonoBehaviour
{
    public enum AIState
    {
        PATROL,
        SEEK,
        STRAFE,
        ATTACK,
        BACKUP,
    }

    private NavMeshAgent agent;
    private Animator animator;
    public bool ikActive = false;

    public float maxLookAheadTime;
    public GameObject[] waypoints;
    public GameObject target;
    public Vector3 targetFuturePosition;

    private Rigidbody rb;
    private EnemyStats stats;
    private bool hasSearched = true;
    private bool isWaiting = false;
    private float patrolWaitTime = 5f;
    private IEntityStats targetStats;

    [SerializeField]
    AIState aiState;
    int currentWaypoint = -1;

    private Vector2 Velocity;
    private Vector2 SmoothDeltaPosition;

    public Transform player;


    private void Awake()
    {
        if (waypoints.Length == 0)
        {
            GameObject waypoint = new GameObject();
            waypoint.transform.position = transform.position;

            waypoints = new GameObject[1];
            waypoints[0] = waypoint;
        }

        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        animator.applyRootMotion = true;
        agent.updatePosition = false;
        agent.updateRotation = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        stats = GetComponent<EnemyStats>();
        rb = GetComponent<Rigidbody>();

        aiState = AIState.PATROL;
        setNextWayPoint();

        if (target != null)
        {
            targetStats = target.GetComponent<IEntityStats>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        SynchronizeAnimatorAndAgent();


        switch (aiState)
        {
            case AIState.PATROL:
                animator.SetBool("aggressive", false);
                if (!isWaiting && agent.remainingDistance < 1f && !agent.pathPending)
                {
                    isWaiting = true;
                    StartCoroutine(PatrolWait());
                }
                if (hasSearched)
                {
                    hasSearched = false;
                    StartCoroutine(SearchForTarget());
                }
                break;
            case AIState.SEEK:
                setDestinationToPredicted();

                if (stats.curStamina > stats.GetWeaponScript().staminaCost)
                {
                    aiState = AIState.ATTACK;
                    stats.isAttacking = true;
                    animator.SetTrigger("attack1");
                }
                break;
            case AIState.BACKUP:
                setDestinationToPredicted();
                if ((stats.isStaggered && UnityEngine.Random.Range(0, 3) > 0))
                {
                    animator.SetBool("retreat", false);
                    aiState = AIState.SEEK;
                }
                Vector3 backward = -transform.forward;

                Vector3 rayOrigin = transform.position;

                RaycastHit hit;

                if (Physics.Raycast(rayOrigin, backward, out hit, 1.2f))
                {
                    if (hit.transform.gameObject.layer == 6)
                    {
                        animator.SetBool("retreat", false);
                        aiState = AIState.SEEK;
                    }
                }
                break;
            //case AIState.ATTACK:
            //    if (!stats.isAttacking)
            //    {
            //        stats.RerollRetreatThreshold();
            //        if (stats.curStamina < stats.retreatThreshold)
            //        {
            //            aiState = AIState.RETREAT;
            //            animator.SetBool("retreat", true);
            //        }
            //        else
            //        {
            //            aiState = AIState.SEEK;
            //        }
            //    }
            //    break;
            default:
                break;
        }
    }

    private void OnAnimatorMove()
    {
        Vector3 rootPosition = animator.rootPosition;
        rootPosition.y = agent.nextPosition.y;
        transform.position = rootPosition;
        agent.nextPosition = rootPosition;
    }

    private void SynchronizeAnimatorAndAgent()
    {
        Vector3 worldDeltaPosition = agent.nextPosition - transform.position;
        worldDeltaPosition.y = 0;

        float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2(dx, dy);

        float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.1f);
        SmoothDeltaPosition = Vector2.Lerp(SmoothDeltaPosition, deltaPosition, smooth);

        Velocity = SmoothDeltaPosition / Time.deltaTime;
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Velocity = Vector2.Lerp(
                Vector2.zero,
                Velocity,
                agent.remainingDistance / agent.stoppingDistance
            );
        }

        bool shouldMove = Velocity.magnitude > 0.5f && agent.remainingDistance > agent.stoppingDistance;

        animator.SetBool("move", shouldMove);

        animator.SetFloat("locamotion", Velocity.magnitude);
        //if (aiState == AIState.RETREAT)
        //{
        //    animator.SetFloat("locamotion", -Velocity.magnitude);
        //} else
        //{
        //    animator.SetFloat("locamotion", Velocity.magnitude);
        //}

        float deltaMagnitude = worldDeltaPosition.magnitude;
        if (deltaMagnitude > agent.radius / 2f)
        {
            transform.position = Vector3.Lerp(animator.rootPosition, agent.nextPosition, smooth);
        }
    }

    void setDestinationToPredicted()
    {
        //float dist = Vector3.Distance(target.transform.position, transform.position);

        //NavMeshHit hit;
        //bool blocked = NavMesh.Raycast(transform.position, target.transform.position, out hit, NavMesh.AllAreas);

        //if (blocked)
        //{
        //    futureTarget = Vector3.Lerp(hit.position, target.transform.position, 0.29f);
        //}

        //targetFuturePosition = futureTarget;

        agent.SetDestination(target.transform.position);
    }

    void setNextWayPoint()
    {
        currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentWaypoint].transform.position);
            targetFuturePosition = waypoints[currentWaypoint].transform.position;
        }
        else
        {
            Debug.Log("No waypoints set as waypoints is of size 0");
        }
    }

    public void ChangeToAISeek()
    {
        aiState = AIState.SEEK;
    }

    IEnumerator PatrolWait()
    {
        yield return new WaitForSeconds(patrolWaitTime);
        isWaiting = false;
        if (aiState == AIState.PATROL)
        {
            setNextWayPoint();
        }
    }

    IEnumerator SearchForTarget()
    {
        Debug.Log("Searching for target");
        yield return new WaitForSeconds(0.5f);
        if (target != null)
        {
            NavMeshHit hit;

            bool blocked = NavMesh.Raycast(transform.position, target.transform.position, out hit, NavMesh.AllAreas);

            // draw the NavMesh raycast
            Debug.DrawRay(transform.position, target.transform.position - transform.position, blocked ? Color.red : Color.green, 1f);

            if (!blocked)
            {
                aiState = AIState.SEEK;
            }
        }
        hasSearched = true;
    }

    //private void update_anim_params()
    //{
    //    // Calculate velocity
    //    Vector3 currentPosition = transform.position;
    //    Vector3 velocity = (currentPosition - previousPosition) / Time.deltaTime;

    //    // Store current position as previous position for the next frame
    //    previousPosition = currentPosition;

    //    // Get the forward vector of the GameObject
    //    Vector3 forwardVector = transform.forward;

    //    // Define an arbitrary vector pointing in a different direction
    //    Vector3 arbitraryVector = Vector3.up; // You can use any vector that is not collinear with the forward vector

    //    // Calculate a vector perpendicular to the forward vector
    //    Vector3 prep_vec = Vector3.Cross(forwardVector, arbitraryVector);


    //    float velx = Vector3.Dot(prep_vec, velocity);
    //    float vely = Vector3.Dot(transform.forward, velocity);
    //    animator.SetFloat("voly", vely);
    //    animator.SetFloat("volx", velx);
    //}

    private void OnAnimatorIK(int layerIndex)
    {
        if (animator)
        {
            if (ikActive)
            {
                if (player)
                {
                    animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(player.position + new Vector3(0, 1f, 0));
                }
            }
            else
            {
                animator.SetLookAtWeight(0);
            }
        }
    }

    private void StrafeLeft()
    {
        Vector3 offsetPlayer = transform.position - player.position;

        Vector3 dir = Vector3.Cross(offsetPlayer, Vector3.up);
        agent.SetDestination(transform.position + dir);
        Vector3 lookPosition = player.position - transform.position;
        lookPosition.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPosition);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2f);
    }

    private void StrafeRight()
    {
        Vector3 offsetPlayer = player.position - transform.position;

        Vector3 dir = Vector3.Cross(offsetPlayer, Vector3.up);
        agent.SetDestination(transform.position + dir);
        Vector3 lookPosition = player.position - transform.position;
        lookPosition.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPosition);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2f);
    }
}
