using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class DisplayChoices : MonoBehaviour
{
    private bool transparencyToggled;
    private Image parentImage;

    public GameObject buttonPrefab;

    private GameObject leftButton;
    private GameObject rightButton;

    public string leftText;
    public string rightText;

    public bool leftBP;
    public bool rightBP;

    public int currentLeftID;
    public int currentRightID;

    public bool choicePressed;
    // Start is called before the first frame update
    void Start()
    {
        choicePressed = false;
        transparencyToggled = false;
        parentImage = GetComponent<Image>();

        //for testing reasons
        leftText = "Left button";
        rightText = "Right button";

        //getbuttons
        // Access the parent GameObject's transform
        //Transform parentTransform = this.transform;

        //test
        //updateDisplay();

        
    }

    // Update is called once per frame
    void Update()
    {
        if(leftButton!=null && rightButton!=null){
            leftBP = leftButton.GetComponent<ChoiceButtonScript>().isPressed;
            rightBP = rightButton.GetComponent<ChoiceButtonScript>().isPressed;

            if(leftBP){
                Debug.Log("lb pressed");
                GameManager.Instance.SetChoiceValue(currentLeftID, true);
                ToggleTransparency();
                
                //set back to false?
                Destroy(leftButton);
                Destroy(rightButton);
                choicePressed = false;

            }else if(rightBP){
                Debug.Log("lb pressed");
                GameManager.Instance.SetChoiceValue(currentRightID, true);
                ToggleTransparency();

                Destroy(leftButton);
                Destroy(rightButton);
                choicePressed = false;
            }
        }  
    }

    private void updateDisplay(){
    // Destroy previous note images
    if (leftButton != null)
    {
        Destroy(leftButton);
    }
    if (rightButton != null)
    {
        Destroy(rightButton);
    }
    // Instantiate left button
    if (true){
        Vector3 position = new Vector3(-150, 10, 0f);
        // Instantiate the prefab at the specified position
        leftButton = Instantiate(buttonPrefab, this.transform);
        leftButton.GetComponent<RectTransform>().localPosition = position;
        leftButton.GetComponentInChildren<TextMeshProUGUI>().text = leftText;
        choicePressed = true;
    }
    if (true){
        Vector3 position2 = new Vector3(150, 10, 0f);
        // Instantiate the prefab at the specified position
        rightButton = Instantiate(buttonPrefab, this.transform);
        rightButton.GetComponent<RectTransform>().localPosition = position2;
        rightButton.GetComponentInChildren<TextMeshProUGUI>().text = rightText;
        choicePressed = true;
    }
}

    public void ActivateChoice(bool b){
        
        //maybe??
        ToggleTransparency();

        if(b == true){
            updateDisplay();
        }
    }

    public void ToggleTransparency()
    {
        if(transparencyToggled == true){
            updateDisplay();
        }
        transparencyToggled = !transparencyToggled;

        // Set transparency for the parent image
        // if (parentImage != null)
        // {
        //     Color imageColor = parentImage.color;
        //     imageColor.a = transparencyToggled ? 0.0f : 1.0f;
        //     parentImage.color = imageColor;
        // }

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

//MAKE BUTTONS DISAPPEAR ON CLICK, AND UPDATE DISPLAYED TEXT
