using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleStateMachine : MonoBehaviour
{
    //  State Machine Variables
    public enum State { Patrol, Attack, Search }
    public State currentState = State.Patrol;

    //  AI Variables
    private NavMeshAgent agent;
    public Transform[] patrolPoints;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;
    }

    void Update()
    {
        StateMachine();
    }
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
    public void GoToState(State state)
    {
        currentState = state;
    }
    void Patrol()
    {
        // Check if we have patrol points
        if (patrolPoints == null)
        {
            Debug.Log("No Patrol Points");
            return;
        }

        // Get a random patrol point to follow
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            agent.destination = patrolPoints[Random.Range(0, patrolPoints.Length)].position;

        //If find player, go to attack
    }
    void Attack() { }
    void Search() { }
}
