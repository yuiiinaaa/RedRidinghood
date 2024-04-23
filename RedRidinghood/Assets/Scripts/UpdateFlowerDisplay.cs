using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpdateFlowerDisplay : MonoBehaviour
{
    public PlayerInventory inv;
    static TextMeshProUGUI myText;
   
    void Start()
    {
        myText = GetComponentInChildren<TextMeshProUGUI>();
        myText.text = inv.flowerAmount.ToString("n0");
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
        
    }
    public void UpdateDisplay(){
        myText.text = inv.flowerAmount.ToString("n0");
    }
}
