using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenFlower : MonoBehaviour, IInteractable
{
    public PlayerInventory inv;
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    public AudioClip interactSound; // Sound effect to play when interacted with

    public bool Interact(Interactor interactor)
    {
        // Play sound effect if assigned
        if (interactSound != null)
        {
            AudioSource.PlayClipAtPoint(interactSound, transform.position);
        }

        // Add # of flowers to respective type of flower

        if (gameObject.CompareTag("Lavender"))
        {
            Debug.Log("Lavender found");
            inv.AddLavenderAmount(1);
            Debug.Log("Lavender Amount" + inv.lavenderAmount);

        }

        else if (gameObject.CompareTag("Tulip"))
        {
            Debug.Log("Tulip found");
            inv.AddTulipAmount(1);
            Debug.Log("Tulip Amount" + inv.tulipAmount);

        }

        else if (gameObject.CompareTag("Lily"))
        {
            Debug.Log("Lily found");
            inv.AddLilyAmount(1);
            Debug.Log("Lily Amount" + inv.lilyAmount);

        }


        // Destroy the GameObject
        Destroy(gameObject);

        // Return true indicating the interaction was successful
        return true;
    }
}
