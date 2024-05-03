using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BridgeTrigger : MonoBehaviour
{
    [SerializeField] private PlayerInventory inv;
    [SerializeField] private GameObject bridge;
    [SerializeField] private GameObject enemyPartOne;
    [SerializeField] private GameObject enemyPartTwo;

    private void Start()
    {
        bridge.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (inv.flowerAmount >= 5)
            {
                inv.RemoveFlowerAmount(5);
                bridge.SetActive(true);

                if (enemyPartOne != null)
                {
                    Destroy(enemyPartOne);
                }
                if (enemyPartTwo != null)
                {
                    enemyPartTwo.SetActive(true);
                }
                Destroy(gameObject);
            }
        }
    }
}
