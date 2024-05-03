using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowFlower2 : MonoBehaviour, IInteractable
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

        inv.AddFlowerAmount(1);
        Debug.Log("Flower Amount" + inv.flowerAmount);

        // Destroy the GameObject
        Destroy(gameObject);

        // Return true indicating the interaction was successful
        return true;
    }
}
