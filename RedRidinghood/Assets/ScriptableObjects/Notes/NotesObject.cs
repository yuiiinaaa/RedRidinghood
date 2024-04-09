using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "default", menuName = "Collectables/Notes")]
public class NotesObject : ScriptableObject{
    public int ID;
    public string displayName;
    //public Sprite icon;
    public GameObject prefab;
    public bool hasBeenFound;

    [TextArea(15,10)]
    public string description;
    
}
