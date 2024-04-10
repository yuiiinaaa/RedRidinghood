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

    // Start is called before the first frame update
    void Start()
    {
        itemsDisplayed.Clear();
        transparencyToggled = true;
        parentImage = GetComponent<Image>();
        currentPage = 0;
        leftNoteImage = null;
        rightNoteImage = null;

        //leave this here for testing for now
        updateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleTransparency();
        }
    }

    private void updateDisplay(){
        if(leftNoteImage != null){
            Destroy(leftNoteImage);
        }
        if(rightNoteImage != null){
            Destroy(rightNoteImage);
        }

        //for(int i = 0; i < inv.notesList.Count; i++){
            if(inv.notesList[currentPage].hasBeenFound == true){
                Vector3 position = new Vector3(-116, 10, 0f);
                // Instantiate the prefab at the specified position
                leftNoteImage = Instantiate(inv.notesList[currentPage].prefab, position, Quaternion.identity);   
            }
            if(inv.notesList[currentPage+1].hasBeenFound == true){
                Vector3 position = new Vector3(116, 10, 0f);
                // Instantiate the prefab at the specified position
                rightNoteImage = Instantiate(inv.notesList[currentPage+1].prefab, position, Quaternion.identity);   
            }   
    //}
    }

    private void PageLeft(){
        currentPage -= 2;
        if(currentPage<0){
            currentPage = 0;
        }
        updateDisplay();
    }

    private void PageRight(){
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
