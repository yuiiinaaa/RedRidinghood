using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class SimpleStateMachine : MonoBehaviour
{
    private AudioSource sound;
    private SpriteRenderer sr;

    //  State Machine Variables
    public enum State { Patrol, Attack, Search }
    public State currentState;

    //  AI Variables
    private NavMeshAgent agent;
    private float clockTime;
    public float searchingDelay = 10f;
    private GameObject target;
    private PlayerMovement targetMovementScript;
    public Transform[] patrolPoints;
    public static float visionDistanceRange = 10f;
    public static float visionAngleRange = 180f; // SINCE WE CHANGE THIS ONE IN THE SCRIPT, IDK IF IT WORKS AS STATIC

    // Enemy abilities
    public float patrolSpeed;
    public float attackSpeed;
    public float searchSpeed;

    public float patrolAngularSpeed;
    public float attackAngularSpeed;
    public float searchAngularSpeed;

    public float patrolAcceleration;
    public float attackAcceleration;
    public float searchAcceleration;

    public float stopDistance = 0.5f;

    private void Start()
    {
        currentState = State.Patrol;
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
        targetMovementScript = target.GetComponent<PlayerMovement>();
        sr = GetComponent<SpriteRenderer>();
        sound = GetComponent<AudioSource>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;
    }

    void Update()
    {
        //if (targetMovementScript.playerFrozen)
        //{
        //    gameObject.SetActive(false);
        //}
        //else
        //{
        //    StateMachine();
        //}
        StateMachine();
        sr.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        
        PlayWalkingSound();

    }

    private void PlayWalkingSound()
    {
        if (!sound.isPlaying)
        {
            sound.Play();
        }
    }


    /*
     * StateMachine controls which state the enemy will be
     */
    private void StateMachine()
    {
        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Attack:
                Attack();
                break;
            case State.Search:
                Search();
                break;
            default:
                break;
        }
    }

    /*
     * GoToState changes the current state of the enemy
     */
    public void GoToState(State state)
    {
        currentState = state;
    }

    void Patrol()
    {
        //Debug.Log("Patrol :)");

        // Check if we have patrol points
        if (patrolPoints == null)
        {
            Debug.Log("No Patrol Points");
            return;
        }

        // Get a random patrol point to follow
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            agent.destination = patrolPoints[Random.Range(0, patrolPoints.Length)].position;

        // Attack State
        if (SeesPlayer())
        {
            SetAgentMovement(attackSpeed, attackAngularSpeed, attackAcceleration, stopDistance);
            //visionAngleRange = 180f;
            GoToState(State.Attack);
        }
    }
    void Attack() {

        //Debug.Log("Attack >:(");

        agent.destination = target.transform.position;

        if (!SeesPlayer()) // Search State
        {
            SetAgentMovement(searchSpeed, searchAngularSpeed, searchAcceleration, stopDistance);
            //visionAngleRange = 90f;
            clockTime = Time.time;
            GoToState(State.Search);
        }
    }
    void Search() {

        //Debug.Log("Searching :|");

        Vector3 selfPos = transform.position;
        float offSetRange = 10f;

        // Follow next point
        if (!agent.pathPending && agent.remainingDistance < 0.5f) //If there's time, make it stop and look around
        {
            Vector3 randomPoint = FindRandomPointWithinRange(selfPos, offSetRange);
            //Debug.Log(randomPoint);
            agent.destination = FindRandomPointWithinRange(selfPos, offSetRange);
        }

        if (SeesPlayer()) // Attack State
        {
            SetAgentMovement(attackSpeed, attackAngularSpeed, attackAcceleration, stopDistance);
            //visionAngleRange = 180f;
            GoToState(State.Attack);
        }

        if (clockTime + searchingDelay < Time.time) // Patrol State
        {
            SetAgentMovement(patrolSpeed, patrolAngularSpeed, patrolAcceleration, stopDistance);
            //visionAngleRange = 90f;
            GoToState(State.Patrol);
        }

    }

    private void SetAgentMovement(float speed, float angularSpeed, float acceleration, float stoppingDistance)
    {
        agent.speed = speed;
        agent.angularSpeed = angularSpeed;
        agent.acceleration = acceleration;
        agent.stoppingDistance = stoppingDistance;
    }

    private Vector3 FindRandomPointWithinRange(Vector3 center, float range)
    {
        Vector3 newPoint = new Vector3(center.x + Random.Range(-range, range), 0f, center.z + Random.Range(-range, range));
        return newPoint;
    }

    private bool SeesPlayer()
    {
        Vector3 toTarget = target.transform.position - transform.position;

        bool isWithinRange = false;

        bool isWithinAngle = (Vector3.Angle(transform.forward, toTarget) <= visionAngleRange);
        //Debug.Log("Angle? = " + isWithinAngle);

        if (Physics.Raycast(transform.position, toTarget, out RaycastHit hit, visionDistanceRange))
        {
            isWithinRange = (hit.transform.root == target.transform);
            //Debug.Log("Range? = " + isWithinRange);
        }

        return isWithinAngle && isWithinRange;
    }
}
