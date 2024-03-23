using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlowBar : MonoBehaviour
{
    public Slider slider;
    public PlayerInventory inv;
    // Start is called before the first frame update
    void Start()
    {
        setMaxGLow();
        
    }
    // Update is called once per frame
    void Update()
    {
        setGLow();
        
    }

    public void setMaxGLow(){
        slider.maxValue = inv.maxGlow;

    }
    public void setGLow(){
        slider.value = inv.flowerAmount;
    }
}
