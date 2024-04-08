using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    public GameObject[] waypoints;

    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("I am Patroling!!");
        
    }

    public override void ExitState(EnemyStateManager enemy)
    {
        throw new System.NotImplementedException();
    }

    public override void OnTriggerEnter(EnemyStateManager enemy)
    {
        throw new System.NotImplementedException();
    }
}
