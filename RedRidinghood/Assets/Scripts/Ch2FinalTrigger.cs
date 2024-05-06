using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ch2FinalTrigger : MonoBehaviour
{
    private GameManager gm;

    private void Start()
    {
        gm = null;
        if (!GameObject.FindGameObjectWithTag("GameController").IsUnityNull())
        {
            gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        }
        else
        {
            Debug.Log("NO GAME MANAGER - Ch2FinalTrigger");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (gm == null) { Debug.Log("NO GAME MANAGER SCRIPT - Ch2FinalTrigger"); }
        else
        {
            gm.OpenScene("Ch3");
        }
    }
}
