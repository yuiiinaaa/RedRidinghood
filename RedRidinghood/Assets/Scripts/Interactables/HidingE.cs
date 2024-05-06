using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HidingE : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    public float hidingDelay;

    public AudioClip hideAudio;
    //public GameObject playerActive;
    private GameObject player;
    public bool playerToggle;
    //Rigidbody2D rb;


    void Start()
    {
        playerToggle = false;
        player = GameObject.FindGameObjectWithTag("Player");
        //PlayerMovement player = GetComponent<PlayerMovement>();
    }

    public bool Interact(Interactor interactor){
        // Play sound effect if assigned

        StartCoroutine(Hide());

        //if (hideAudio != null){
        //    AudioSource.PlayClipAtPoint(hideAudio, transform.position);
        //}

        //if (playerToggle == true)
        //{
        //    player.SetActive(false);
        //    playerToggle = false;
        //} else
        //{
        //    player.SetActive(true);
        //}

        return true;
    }

    IEnumerator Hide()
    {
        AudioSource.PlayClipAtPoint(hideAudio, transform.position);
        player.SetActive(false);
        yield return new WaitForSeconds(hidingDelay);

        AudioSource.PlayClipAtPoint(hideAudio, transform.position);
        player.SetActive(true);

    }


}
