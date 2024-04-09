using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class DisplayBook : MonoBehaviour
{
    static Dictionary<int, GameObject> itemsDisplayed = new Dictionary<int, GameObject>();
    private bool transparencyToggled;
     private Image parentImage;

    // Start is called before the first frame update
    void Start()
    {
        itemsDisplayed.Clear();
        transparencyToggled = true;
         parentImage = GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleTransparency();
        }
        
    }

    public void ToggleTransparency()
    {
        transparencyToggled = !transparencyToggled;

        if (parentImage != null)
        {
            Color imageColor = parentImage.color;
            imageColor.a = transparencyToggled ? 0.0f : 1.0f;
            parentImage.color = imageColor;
        }

        // foreach (var kvp in itemsDisplayed)
        // {
        //     GameObject displayObject = kvp.Value;
        //     if (displayObject != null)
        //     {
        //         displayObject.SetActive(!transparencyToggled);
        //     }
        // }
    }
}
