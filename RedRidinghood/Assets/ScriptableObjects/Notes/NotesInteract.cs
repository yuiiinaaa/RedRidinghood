using UnityEngine;

public class NoteInteract : MonoBehaviour, IInteractable
{
    public PlayerInventory inv;
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    public int noteID;
    public GameObject prefab;

    public AudioClip interactSound; // Sound effect to play when interacted with

    private GameObject instantiatedCanvas; // Reference to the instantiated canvas

    public bool Interact(Interactor interactor)
    {
        // Play sound effect if assigned
        if (interactSound != null)
        {
            AudioSource.PlayClipAtPoint(interactSound, transform.position);
        }

        // Instantiate the canvas prefab
         instantiatedCanvas = Instantiate(prefab);
        GameManager.Instance.setCanvasNote(instantiatedCanvas);

        //add note to inv
        inv.FoundNote(noteID);
        Destroy(gameObject);

        // Return true indicating the interaction was successful
        return true;
    }

    void Update()
    {
        
    }
}
