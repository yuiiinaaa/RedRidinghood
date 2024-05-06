using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDetector : MonoBehaviour
{
    public float detectionRange = 5f; // Range within which to detect Interactable objects
    public GameObject replacementPrefab; // Prefab to instantiate when an Interactable object is destroyed
    public bool flowerPlaced;
    public AudioClip interactSound;
    public PlayerInventory inv;
    public int noteID;

    private GameObject instantiatedCanvas; // Reference to the instantiated canvas

    void Start(){
        flowerPlaced = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GlowFlower")
        {
            if(!flowerPlaced){
            // Destroy the detected Interactable object
                //Destroy(other.gameObject);
                if (interactSound != null){
                    AudioSource.PlayClipAtPoint(interactSound, transform.position);
                }
                // Instantiate the canvas prefab
                instantiatedCanvas = Instantiate(replacementPrefab);
                GameManager.Instance.setCanvasNote(instantiatedCanvas);
                //add note to inv
                inv.FoundNote(noteID);

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
