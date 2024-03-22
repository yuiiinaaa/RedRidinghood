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
    public int flowerAmount;

    public void AddFlowerAmount(int value){
        flowerAmount += value;
    }
    public void RemoveFlowerAmount(int value){
        flowerAmount -= value;
    }
}


