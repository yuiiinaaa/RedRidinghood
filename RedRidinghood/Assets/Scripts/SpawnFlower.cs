using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFlower : MonoBehaviour
{
    public GameObject prefabToSpawn; // The prefab to spawn
     public PlayerInventory inv;

    void Update(){
        // Check if the player presses the P key
        if (Input.GetKeyDown(KeyCode.P)){
            Spawn();
            inv.RemoveFlowerAmount(1);
        }
    }

    void Spawn(){
        // Ensure that the prefab to spawn is assigned
        if (prefabToSpawn != null){
            // Instantiate the prefab at the current position and rotation of this GameObject
            GameObject spawnedObject = Instantiate(prefabToSpawn, transform.position, transform.rotation);
            // spawnedObject.transform.parent = transform;
        }
        else{
            Debug.LogWarning("Prefab to spawn is not assigned!");
        }
    }
}
