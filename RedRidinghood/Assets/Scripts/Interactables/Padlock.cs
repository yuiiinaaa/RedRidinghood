using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Padlock : MonoBehaviour
{
    public int lockNum;
    public List<char> characterCode = new List<char>();
    public int numChars;
    public bool isUnlocked;
    private List<char> playerCodeInput = new List<char>();
    public GameObject inputFieldPrefab; // Reference to the InputField prefab
    private List<TMP_InputField> inputFields = new List<TMP_InputField>(); // List to store InputField components
    private  bool containsZero;

    void Start()
    {
        isUnlocked = false;
        containsZero = true;

        // Instantiate InputField boxes
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

    void Update()
    {
        if (isUnlocked)
        {
            GameManager.Instance.SetGateUnlock(lockNum, true);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            // Destroy the instantiated canvas and its children
            Destroy(gameObject);
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

        // Check if "0" exists in characterCode
        // Check if each character matches the characterCode
       
        for (int i = 0; i < numChars; i++){
            if (playerCodeInput[i] == '0')
            {
                containsZero = true;
                break;
            }else{ 
                containsZero = false; 

            }
        }
        

        // If "0" does not exist in characterCode, set input field texts to "0"
        if (!containsZero && isUnlocked!=true)
        {
            foreach (TMP_InputField inputField in inputFields)
            {
                inputField.text = "0";
            }
        }
    
    }

    void CheckCodeInput()
    {
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

    public void SetLockNum(int n)
    {
        lockNum = n;
    }
}
