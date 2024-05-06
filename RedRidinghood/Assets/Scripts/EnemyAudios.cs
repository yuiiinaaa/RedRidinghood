using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudios : MonoBehaviour
{
    private AudioSource audio;
    private SimpleStateMachine enemyStateMachine;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        enemyStateMachine = gameObject.GetComponentInParent<SimpleStateMachine>();
    }

    private void Update()
    {
        if (enemyStateMachine.currentState == SimpleStateMachine.State.Attack)
        {
            PlayLaughing();
        }
    }

    void PlayLaughing()
    {
        if(!audio.isPlaying)
        {
            audio.Play();
        }
    }
}
