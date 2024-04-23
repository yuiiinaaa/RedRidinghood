using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HidingE : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    public AudioClip hideAudio;
    //public GameObject playerActive;
    public GameObject player;
    public bool playerToggle;
    //Rigidbody2D rb;


    public bool Interact(Interactor interactor){
        // Play sound effect if assigned
        if (hideAudio != null){
            AudioSource.PlayClipAtPoint(hideAudio, transform.position);
        }

        if (playerToggle == true)
        {
            player.SetActive(false);
            playerToggle = false;
        } else
        {
            player.SetActive(true);
        }

        return true;
    }
    // Start is called before the first frame update
    void Start()
    {
        playerToggle = true;
        PlayerMovement player = GetComponent<PlayerMovement>();
     

    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
