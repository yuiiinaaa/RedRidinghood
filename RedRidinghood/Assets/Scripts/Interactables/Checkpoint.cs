using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour, IInteractable
{
    private GameObject glowObject;
    private GameObject darkObject;
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    public bool Interact(Interactor interactor){
        if (glowObject != null && darkObject != null)
        {
            // Toggle the active state of the objects
            glowObject.SetActive(true);
            darkObject.SetActive(false);

            Debug.Log("Toggle Checkpoint!");
            return true;
        }
        else
        {
            Debug.LogWarning("Glow or Dark object is not assigned!");
            return false;
        }

    }

    private void Start()
    {
        // Access the parent GameObject's transform
        Transform parentTransform = this.transform;

        // Accessing child GameObjects by index
        if (parentTransform != null)
        {
            if (parentTransform.childCount >= 2)
            {
                // Access the first child GameObject (glow object)
                glowObject = parentTransform.GetChild(0).gameObject;

                Debug.Log("Glow:" + glowObject);
                // Access the second child GameObject (dark object)
                darkObject = parentTransform.GetChild(1).gameObject;
                Debug.Log("Glow:" + darkObject);

                glowObject.SetActive(false);
            
            }
            else
            {
                Debug.Log("Parent GameObject doesn't have enough child objects!");
            }
        }
        else
        {
            Debug.Log("Parent GameObject not found!");
        }
}
}
