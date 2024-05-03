using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : MonoBehaviour, IInteractable
{
    public PlayerInventory inv;
    public AudioClip interactSound;
    public GameObject fountain;

    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        if (fountain != null)
        {
            AudioSource.PlayClipAtPoint(interactSound, transform.position);
            Debug.Log("Toggle Fountain!");
            GameManager.Instance.SetTrigger(3, 4, true);
            return true;
        }
        else
        {
            Debug.Log("fountain not found");
            return false;
        }
    }


    public void Start()
    {
        // Access the parent GameObject's transform
        Transform parentTransform = this.transform;

        // Accessing child GameObjects by index
        if (parentTransform != null)
        {
            fountain = parentTransform.GetChild(0).gameObject;
            // Debug.Log("fountain: " + fountain != null);
        }
        else
        {
            Debug.Log("Parent GameObject not found!");
        }
    }
}