using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDetector : MonoBehaviour
{
    public float detectionRange = 5f; // Range within which to detect Interactable objects
    public GameObject replacementPrefab; // Prefab to instantiate when an Interactable object is destroyed
    public bool flowerPlaced;

    void Start(){
        flowerPlaced = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GlowFlower"))
        {
            Debug.Log("Hi");
            if(!flowerPlaced){
            // Destroy the detected Interactable object
            Destroy(other.gameObject);

            // Instantiate a new object as replacement
            if (replacementPrefab != null){
                Instantiate(replacementPrefab, other.transform.position, other.transform.rotation);
            }
            else{
                Debug.LogWarning("Replacement prefab is not assigned.");
            }

            flowerPlaced = true;

            }
            
        }
    }

    

    void OnDrawGizmosSelected()
    {
        // Draw a wire sphere to visualize the detection range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
