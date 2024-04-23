using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceButtonScript : MonoBehaviour
{
    public int choiceID;
    public string choiceReturnString;
    public bool isPressed;
    // Start is called before the first frame update
    void Start()
    {
        isPressed =false;
        
    }

    public void buttonPressed(){
        //once pressed, both buttons should disappear and the dialogue is updated
        isPressed = true;
    }
}
