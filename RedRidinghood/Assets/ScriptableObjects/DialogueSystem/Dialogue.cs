using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    private static TextMeshProUGUI textComponent;
    private TextMeshProUGUI hitEnter;
    public float textSpeed;
    public List<DialogueObject>dialogueScript = new List<DialogueObject>();
    //private static List<bool>beenTriggered = new List<bool>();
    private int index;
    private int currentScript;
    private bool isToggled;

    public string currentChapter;
    // Start is called before the first frame update
    void Start()
    {
        isToggled = true;
        currentScript = 0;
       
        for(int i =0; i< dialogueScript.Count; i++){
            //beenTriggered.Add(false);
        }

        //beenTriggered[0] = true;

        textComponent = GetComponentInChildren<TextMeshProUGUI>();
        
        textComponent.text = string.Empty;
        
        StartDialogue();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && isToggled == true)
        {
            if(textComponent.text == dialogueScript[currentScript].Lines[index]){
                NextLine();
            } else{
                StopAllCoroutines();
                textComponent.text = dialogueScript[currentScript].Lines[index];
            }
            
            
        }

        Chapter1Triggers();

        // //this is for testing purposes
        // if (Input.GetKeyDown(KeyCode.W)){
        //     isToggled = true;
        //     ToggleChildren(true);
        //     textComponent.text = string.Empty;
        //     StartDialogue();

        // }
        
    }

    void StartDialogue(){
        index = 0;
        StartCoroutine(TypeLine());
    }

    void NextLine(){
        if(index < dialogueScript[currentScript].Lines.Count - 1){
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }else{
            //all dialogues are finished
            isToggled = false;
            ToggleChildren(false);
            GameManager.Instance.SetCutsceneTrigger(false);
            if(currentScript < dialogueScript.Count - 1){
                currentScript++;
                Debug.Log(currentScript);
            }

            if(currentScript == dialogueScript.Count - 1){
                isToggled = false;
                ToggleChildren(false);
                GameManager.Instance.SetCutsceneTrigger(false);
            }
        }
    }

    IEnumerator TypeLine(){

        foreach(char c in dialogueScript[currentScript].Lines[index].ToCharArray()){
            textComponent.text += c;

            if((c == ',') || (c == '?')){
                yield return new WaitForSeconds(textSpeed + 0.5f);

            } else if((c == '.')){
                yield return new WaitForSeconds(textSpeed + 0.5f);

            }
            else{
                yield return new WaitForSeconds(textSpeed);
            }  
        }
    }


    void ToggleChildren(bool activeState)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(activeState);
        }
}

    void Chapter1Triggers(){
       
        if((GameManager.Instance.GetTrigger(1,1))&& (isToggled == false) && currentScript == 1){
            isToggled = true;
            ToggleChildren(true);
            GameManager.Instance.SetCutsceneTrigger(true);
            textComponent.text = string.Empty;
            
            StartDialogue();

            //maybe?
            //GameManager.Instance.SetTrigger(1,1,false);

        }

        if((GameManager.Instance.GetTrigger(1,2))&& (isToggled == false)&& currentScript == 2){
            isToggled = true;
            ToggleChildren(true);
            GameManager.Instance.SetCutsceneTrigger(true);
            textComponent.text = string.Empty;
            
            StartDialogue();

            //GameManager.Instance.SetTrigger(1,2,false); 
        }

        if((GameManager.Instance.GetTrigger(1,3))&& (isToggled == false)&& currentScript == 3){
            isToggled = true;
            ToggleChildren(true);
            GameManager.Instance.SetCutsceneTrigger(true);
            textComponent.text = string.Empty;
            
            StartDialogue();

            GameManager.Instance.SetTrigger(1,3,false);
            
        }
        //GameManager.Instance.setTrigger

    }


}
