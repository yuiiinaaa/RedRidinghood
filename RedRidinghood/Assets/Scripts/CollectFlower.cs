using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectFlower : MonoBehaviour
{
    public PlayerInventory inv;

    private void OnTriggerEnter(Collider other){
        if(other.transform.tag == "GlowFlower"){
            inv.AddFlowerAmount(1);
            Debug.Log("Flower Amount" + inv.flowerAmount);
            Destroy(other.gameObject);

        }
    }
}
