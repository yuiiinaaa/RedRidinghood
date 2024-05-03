
using System.Collections;
using System.Collections;





using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;

[CreateAssetMenu(fileName = "New Choice", menuName = "DialogueSystem/Choice")]
public class ChoiceDialogueObj : ScriptableObject
{
    public bool isTriggered;
    public int leftID;
    public int rightID;

    public string leftChoice;
    public string rightChoice;
    [TextArea(5,10)]
    public List<string> LinesL = new List<string>();
    // Start is called before the first frame update

    [TextArea(5,10)]
    public List<string> LinesR = new List<string>();
    // Start is called before the first frame update
    
    public string GetLines(int i, int index){
        if(i == 1){
            return LinesL[index];
        }else if(i == 2){
            return LinesR[index];
        }
        return null;     
    }
}
