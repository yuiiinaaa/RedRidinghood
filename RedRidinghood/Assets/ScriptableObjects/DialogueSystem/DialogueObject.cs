using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "DialogueSystem/Dialogue")]
public class DialogueObject : ScriptableObject
{
    public string name;
    public bool isTriggered;
    [TextArea(15,10)]
    public List<string> Lines = new List<string>();
    // Start is called before the first frame update
    
    public string GetLines(int index){
        return Lines[index];
    }
}


