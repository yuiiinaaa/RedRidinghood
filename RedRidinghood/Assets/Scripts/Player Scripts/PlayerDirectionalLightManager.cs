using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirectionalLightManager : MonoBehaviour
{
    private Transform player;
    private GameObject enemy;
    private Light lightControl;
    private float enemyVision;
    public float offset = 20f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        lightControl = GetComponent<Light>();
        enemyVision = SimpleStateMachine.visionDistanceRange;
    }

    private void Update()
    {
        Vector3 enemyPos = enemy.transform.position;
        Vector3 playerToEnemy = player.transform.position - enemyPos;


        if (Physics.Raycast(enemyPos, playerToEnemy, enemyVision + offset))
        {
            if (Physics.Raycast(enemyPos, playerToEnemy, enemyVision))
            {
                lightControl.color = Color.red;
            }
            else
            {
                float distancePlayerToEnemy = playerToEnemy.magnitude;
                lightControl.color = new Color((distancePlayerToEnemy - enemyVision) / offset, 0f, 0f);
                Debug.Log("Red Color = " + (distancePlayerToEnemy - enemyVision) / offset);
            }
        }
        else
        {
            Debug.Log("Normal Color");
            lightControl.color = Color.white;
        }

    }
}
