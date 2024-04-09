using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateText : MonoBehaviour
{
    public PlayerInventory inv;
    private TextMeshProUGUI textComponent;
    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        textComponent.text = string.Empty;
        
    }

    // Update is called once per frame
    void Update()
    {
        textComponent.text = string.Empty;
        textComponent.text =  inv.flowerAmount.ToString()+"/5";
        
    }
}
