using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory")]
public class PlayerInventory : ScriptableObject
{
    public List<NotesObject>notesList = new List<NotesObject>();
    public int flowerAmount;
    public int maxGlow;

    public void AddFlowerAmount(int value){
        if(flowerAmount < maxGlow){
            flowerAmount += value;
        }
    }
    public void RemoveFlowerAmount(int value){
        if(flowerAmount > 0){
            flowerAmount -= value;
        }
    }
    public void FoundNote(int id){
        notesList[id].hasBeenFound = true;
    }
    public void resetInv(){
        flowerAmount = 1;
        maxGlow = 10;
        foreach(var note in notesList){
            note.hasBeenFound = false;
        }
    }
}


