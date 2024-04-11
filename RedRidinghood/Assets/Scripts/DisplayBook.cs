using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class DisplayBook : MonoBehaviour
{
    public PlayerInventory inv;
    static Dictionary<int, GameObject> itemsDisplayed = new Dictionary<int, GameObject>();
    private bool transparencyToggled;
    private Image parentImage;

    private int currentPage;

    private GameObject leftNoteImage;
    private GameObject rightNoteImage;

    private GameObject leftButton;
    private GameObject rightButton;

    // Start is called before the first frame update
    void Start()
    {
        itemsDisplayed.Clear();
        transparencyToggled = false;
        parentImage = GetComponent<Image>();
        currentPage = 0;
        leftNoteImage = null;
        rightNoteImage = null;

        //getbuttons
        // Access the parent GameObject's transform
        Transform parentTransform = this.transform;

        // Accessing child GameObjects by index
        if (parentTransform != null)
        {
            if (parentTransform.childCount >= 2)
            {
                // Access the first child GameObject (right object)
                leftButton = parentTransform.GetChild(0).gameObject;
                // Access the second child GameObject (left object)
                rightButton = parentTransform.GetChild(1).gameObject;                     
            } 
        }
        

        //leave this here for testing for now
        updateDisplay();

        ToggleTransparency();     
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleTransparency();
            updateDisplay();
        }
    }

    private void updateDisplay()
{
    // Destroy previous note images
    if (leftNoteImage != null)
    {
        Destroy(leftNoteImage);
    }
    if (rightNoteImage != null)
    {
        Destroy(rightNoteImage);
    }

    // Instantiate new note images
    if (inv.notesList[currentPage].hasBeenFound)
    {
        Vector3 position = new Vector3(-116, 10, 0f);
        // Instantiate the prefab at the specified position
        leftNoteImage = Instantiate(inv.notesList[currentPage].prefab, this.transform);
        leftNoteImage.GetComponent<RectTransform>().localPosition = position;
    }
    if (inv.notesList.Count > currentPage + 1 && inv.notesList[currentPage + 1].hasBeenFound)
    {
        Vector3 position2 = new Vector3(116, 10, 0f);
        // Instantiate the prefab at the specified position
        rightNoteImage = Instantiate(inv.notesList[currentPage + 1].prefab, this.transform);
        rightNoteImage.GetComponent<RectTransform>().localPosition = position2;
    }

    if(currentPage <= 1){
        rightButton.SetActive(false);

    }else{
        rightButton.SetActive(true);
    }

    if(currentPage>=inv.notesList.Count-2){
        leftButton.SetActive(false);
    }else{
        leftButton.SetActive(true);
    }
}

    public void PageLeft(){
        currentPage -= 2;
        if(currentPage<0){
            currentPage = 0;
        }
        updateDisplay();
    }

    public void PageRight(){
        //unsure if this logic is correct...
        currentPage += 2;
        if(currentPage>=inv.notesList.Count-2){
            if(inv.notesList.Count%2 == 0){
                currentPage = inv.notesList.Count-2;

            }else{
                currentPage = inv.notesList.Count-1;
            }
        }
        updateDisplay();
    }

    public void ToggleTransparency()
    {
        if(transparencyToggled == true){
            updateDisplay();
        }
        transparencyToggled = !transparencyToggled;

        // Set transparency for the parent image
        if (parentImage != null)
        {
            Color imageColor = parentImage.color;
            imageColor.a = transparencyToggled ? 0.0f : 1.0f;
            parentImage.color = imageColor;
        }

        // Disable UI components in children
        foreach (Transform child in transform)
        {
            // Disable Image component if exists
            Image imageComponent = child.GetComponent<Image>();
            if (imageComponent != null)
            {
                imageComponent.enabled = !transparencyToggled;
            }

            // Disable TextMeshProUGUI component if exists
            TextMeshProUGUI textComponent = child.GetComponent<TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent.enabled = !transparencyToggled;
            }
        }
    }

    
}
