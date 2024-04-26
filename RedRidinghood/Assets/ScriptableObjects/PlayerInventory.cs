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


    // Garden flowers
    public int lavenderAmount;
    public int tulipAmount;
    public int lilyAmount;
    public int lovAmount;
    public int sunflowerAmount;
    public int blackroseAmount;

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
    
        lavenderAmount = 0;
        tulipAmount = 0;
        lilyAmount = 0;
        lovAmount = 0;
        sunflowerAmount = 0;
        blackroseAmount = 0;

        maxGlow = 10;
        foreach(var note in notesList){
            note.hasBeenFound = false;
        }
    }

    public void AddLavenderAmount(int value)
    {
        lavenderAmount += value;
    }

    public void RemoveLavenderAmount(int value)
    {
        if (lavenderAmount > 0)
        {
            lavenderAmount -= value;
        }
    }
    public void AddTulipAmount(int value)
    {
        tulipAmount += value;
    }

    public void RemoveTulipAmount(int value)
    {
        if (tulipAmount > 0)
        {
            tulipAmount -= value;
        }
    }

    public void AddLilyAmount(int value)
    {
        lilyAmount += value;
    }

    public void RemoveLilyAmount(int value)
    {
        if (lilyAmount > 0)
        {
            lilyAmount -= value;
        }
    }

    public void AddLovAmount(int value)
    {
        lovAmount += value;
    }

    public void RemoveLovAmount(int value)
    {
        if (lovAmount > 0)
        {
            lovAmount -= value;
        }
    }
    public void AddSunflowerAmount(int value)
    {
        sunflowerAmount += value;
    }

    public void RemoveSunflowerAmount(int value)
    {
        if (sunflowerAmount > 0)
        {
            sunflowerAmount -= value;
        }
    }

    public void AddBlackroseAmount(int value)
    {
        blackroseAmount += value;
    }

    public void RemoveBlackroseAmount(int value)
    {
        if (blackroseAmount > 0)
        {
            blackroseAmount -= value;
        }
    }

}


