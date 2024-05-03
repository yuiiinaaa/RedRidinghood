using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class SimpleStateMachine : MonoBehaviour
{
    public Vector3 enemyInitialPosition = new Vector3(-26,0,45);
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
    public static float visionDistanceRange = 20f;
    public static float visionAngleRange = 180f; // SINCE WE CHANGE THIS ONE IN THE SCRIPT, IDK IF IT WORKS AS STATIC

    // Enemy abilities
    public float patrolSpeed = 7f;
    public float attackSpeed = 10f;
    public float searchSpeed = 5f;

    public float patrolAngularSpeed = 240f;
    public float attackAngularSpeed = 200f;
    public float searchAngularSpeed = 240f;

    public float patrolAcceleration = 12f;
    public float attackAcceleration = 8f;
    public float searchAcceleration = 16f;

    public float stopDistance = 0.5f;

    private void Start()
    {
        currentState = State.Patrol;
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
        targetMovementScript = target.GetComponent<PlayerMovement>();
        sr = GetComponent<SpriteRenderer>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;
    }

    void Update()
    {
        if (targetMovementScript.playerFrozen)
        {
            transform.position = enemyInitialPosition;
        }
        else
        {
            StateMachine();
        }
        sr.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

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
            SetAgentMovement(10f, 200f, 8f, 0.5f);
            //visionAngleRange = 180f;
            GoToState(State.Attack);
        }
    }
    void Attack() {

        //Debug.Log("Attack >:(");

        agent.destination = target.transform.position;

        if (!SeesPlayer()) // Search State
        {
            SetAgentMovement(5f, 240f, 16f, 0.5f);
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
            SetAgentMovement(10f, 200f, 8f, 0.5f);
            //visionAngleRange = 180f;
            GoToState(State.Attack);
        }

        if (clockTime + searchingDelay < Time.time) // Patrol State
        {
            SetAgentMovement(7f, 240f, 12f, 0.5f);
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
