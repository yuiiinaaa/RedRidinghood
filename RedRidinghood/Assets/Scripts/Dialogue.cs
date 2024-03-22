using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    private static TextMeshProUGUI textComponent;
    private TextMeshProUGUI hitEnter;
    public float textSpeed;
    public DialogueObject dialogueScript;
    private int index;
    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponentInChildren<TextMeshProUGUI>();
        
        textComponent.text = string.Empty;
        
        StartDialogue();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(textComponent.text == dialogueScript.Lines[index]){
                NextLine();
            } else{
                StopAllCoroutines();
                textComponent.text = dialogueScript.Lines[index];
            }
            
            // Enter key is pressed
            Debug.Log("Enter key is pressed.");
        }
        
    }

    void StartDialogue(){
        index = 0;
        StartCoroutine(TypeLine());
    }

    void NextLine(){
        if(index < dialogueScript.Lines.Count - 1){
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }else{
            gameObject.SetActive(false);
        }
    }

    IEnumerator TypeLine(){

        foreach(char c in dialogueScript.Lines[index].ToCharArray()){
            textComponent.text += c;

            if((c == ',') || (c == '.')){
                yield return new WaitForSeconds(textSpeed + 0.2f);

            }else{
                yield return new WaitForSeconds(textSpeed);
            }  
        }
    }
}
