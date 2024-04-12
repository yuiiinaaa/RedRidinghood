using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Padlock : MonoBehaviour
{
    public int lockNum;
    public List<char>characterCode = new List<char>();
    public int numChars;
    public bool isUnlocked;
    private List<char>playerCodeInput = new List<char>();
    // Start is called before the first frame update
    public GameObject inputFieldPrefab; // Reference to the InputField prefab
    private List<TMP_InputField> inputFields = new List<TMP_InputField>(); // List to store InputField components

    void Start()
    {
        isUnlocked = false;

        // Instantiate 5 InputField boxes
        for (int i = 0; i < numChars; i++)
        {
            GameObject inputFieldObject = Instantiate(inputFieldPrefab, this.transform);
            TMP_InputField inputField = inputFieldObject.GetComponent<TMP_InputField>();
            if (inputField != null)
            {
                inputField.characterLimit = 1; // Restrict input to one character
                inputFields.Add(inputField); // Add InputField component to the list
                inputField.onValueChanged.AddListener(SaveInputToList);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Return)){
        //     CheckCodeInput();
        // }  

        //CheckCodeInput();

        if(isUnlocked==true){
            GameManager.Instance.SetGateUnlock(lockNum, true);
        } 
    }

    public void SaveInputToList(string str2)
    {
        playerCodeInput.Clear(); // Clear previous input

        // Store inputs from all InputFields into a list
        foreach (TMP_InputField inputField in inputFields)
        {
            string str = inputField.text;
            if (str.Length > 0)
            {
                playerCodeInput.Add(str[0]);
            }
        }

        CheckCodeInput();
    }

    void CheckCodeInput(){
        // int numSame = 0;
        // for (int i = 0; i < numChars; i++){
        //     if(characterCode[i]==playerCodeInput[i]){
        //         numSame++;
        //     }else{
        //         numSame = 0;
        //         break;
        //     }    
        // }
        // if(numSame == numChars-1){
        //     isUnlocked = true;
        // }
        if (playerCodeInput.Count != numChars)
        {
            Debug.Log("Incorrect code length");
            return;
        }

        // Check if each character matches the characterCode
        for (int i = 0; i < numChars; i++)
        {
            if (characterCode[i] != playerCodeInput[i])
            {
                Debug.Log("Incorrect code");
                return;
            }
        }

        // Code is correct
        Debug.Log("Correct code");
        isUnlocked = true;
    }

    public void SetLockNum(int n){
        lockNum = n;
    }
}
