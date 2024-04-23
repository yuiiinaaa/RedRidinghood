using UnityEngine;

public class LockInteract : MonoBehaviour, IInteractable
{
    public PlayerInventory inv;
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    public int lockID;
    public GameObject prefabL;
    public GameObject prefabU;
    public AudioClip interactSoundE; // Sound effect to play when interacted with
    private GameObject instantiatedCanvas; // Reference to the instantiated canvas
    //private GameObject instantiatedChild; // Reference to the instantiated canvas

    private Padlock childScript;

    public bool Interact(Interactor interactor)
    {
        

        // Instantiate the canvas prefab
        if(!GameManager.Instance.GetGateUnlock(lockID)){
            if(prefabL != null){
                instantiatedCanvas = Instantiate(prefabL);
                //instantiatedChild = instantiatedCanvas.transform.GetChild(1).GetComponent<GameObject>();
                childScript = instantiatedCanvas.transform.GetChild(1).GetComponent<Padlock>();
                childScript.SetLockNum(lockID);

                GameManager.Instance.setCanvasNote(instantiatedCanvas);
            }
            
        }else{
            if(prefabU != null){
                instantiatedCanvas = Instantiate(prefabU);
                GameManager.Instance.setCanvasNote(instantiatedCanvas);
            }
            // Play sound effect if assigned
            if (interactSoundE != null)
            {
                AudioSource.PlayClipAtPoint(interactSoundE, transform.position);
            }
            GameManager.Instance.SetCutsceneTrigger(false);
            Destroy(gameObject);
        }
        
        return true;
    }

    void Update()
    {
        if (instantiatedCanvas != null){
            GameManager.Instance.SetCutsceneTrigger(true);

            if(GameManager.Instance.GetGateUnlock(lockID)){
            Destroy(instantiatedCanvas);
            GameManager.Instance.setCanvasNote(null);
            //Destroy(instantiatedChild);

            if (interactSoundE != null)
            {
                AudioSource.PlayClipAtPoint(interactSoundE, transform.position);
            }

            instantiatedCanvas = Instantiate(prefabU);
            GameManager.Instance.setCanvasNote(instantiatedCanvas);
            Destroy(gameObject);
        }
        }

        

        
    }
}
