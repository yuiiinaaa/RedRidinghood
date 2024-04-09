using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RangeText : MonoBehaviour
{
    public PlayerInventory inv;
    private string playerTag = "Player";
    public float detectionRange = 5f;
    private bool playerInRange = false;
    private GameObject textObject;
    private static TextMeshProUGUI textComponent; // Static text component WHY YOU NOT CHANGING BRO

    void Start()
    {
        Transform parentTransform = this.transform;
        // Assuming the text object is the 4th child
        textObject = parentTransform.GetChild(3).gameObject;
        //textComponent = textObject.GetComponent<TextMeshProUGUI>();
        textComponent = GetComponentInChildren<TextMeshProUGUI>();
        //textComponent.text = string.Empty;

        UpdateTextVisibility();
        
    }

    void Update(){
        if (textComponent != null)
        {
            textComponent.text = string.Empty;
            textComponent.text =  inv.flowerAmount.ToString()+"/5";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInRange = true;
            UpdateTextVisibility();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInRange = false;
            UpdateTextVisibility();
            Debug.Log("Checkpoint stat: " + playerInRange);
        }
    }
   private void UpdateTextVisibility()
    {
        if (textObject != null)
        {
            textObject.SetActive(playerInRange);
        }
    }
}
