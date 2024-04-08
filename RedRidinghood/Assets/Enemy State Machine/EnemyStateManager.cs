using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    public EnemyBaseState currentState;
    public Dictionary<string, EnemyBaseState> enemyStates = new Dictionary<string, EnemyBaseState>();

    private void Start()
    {
        InitializeStates();

        currentState.EnterState(this);
    }

    private void InitializeStates()
    {
        enemyStates.Clear();
        enemyStates.Add("Patrol", new EnemyPatrolState());
        enemyStates.Add("Attack", new EnemyAttackState());
        enemyStates.Add("Searching", new EnemySearchingState());

        currentState = enemyStates["Patrol"];
    }


}
