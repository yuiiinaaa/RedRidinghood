using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowerTracker : MonoBehaviour
{

    public PlayerInventory inv;
    public Text flowerCountText;

    // Start is called before the first frame update
    void Start()
    {
        flowerCountText = GetComponent<Text>();
        
    }
    // Update is called once per frame
    void Update()
    {
        flowerCountText.text = "x" + inv.lavenderAmount.ToString() + "    x" + inv.sunflowerAmount.ToString() + "    x" + inv.blackroseAmount.ToString() + "      x" + inv.lovAmount.ToString() + "     x" + inv.lilyAmount.ToString() + "    x" + inv.tulipAmount.ToString();
    }
}
