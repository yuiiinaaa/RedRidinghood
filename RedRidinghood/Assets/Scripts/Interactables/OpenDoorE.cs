using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorE : MonoBehaviour,  IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    public string openSceneName;
    public AudioClip interactSound; // Sound effect to play when interacted with
    // Start is called before the first frame update
     public bool Interact(Interactor interactor)
    {
        StartCoroutine(DoorInteract());
        // Play sound effect if assigned
        // if (interactSound != null)
        // {
        //     AudioSource.PlayClipAtPoint(interactSound, transform.position);
        // }
        //GameManager.Instance.OpenScene(openSceneName);
        return true;
    }

        IEnumerator DoorInteract(){    
        // Play sound effect if assigned
        if (interactSound != null)
        {
            AudioSource.PlayClipAtPoint(interactSound, transform.position);
        }
            yield return new WaitForSeconds(.8f);
            GameManager.Instance.OpenScene(openSceneName);
        }

    
    
}
