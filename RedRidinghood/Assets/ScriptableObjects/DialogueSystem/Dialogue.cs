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

    //for the choices mechanic
    private DisplayChoices choiceFunction;
    // Start is called before the first frame update

    private GameObject choicesPanel;
    private bool choicesToggled;
    void Start()
    {
        //get the choices box
        Transform parentTransform = this.transform;
        choicesPanel = parentTransform.GetChild(3).gameObject;
        choiceFunction = choicesPanel.GetComponent<DisplayChoices>();


        isToggled = true;
        currentScript = 0;
       
        for(int i =0; i< dialogueScript.Count; i++){
            //beenTriggered.Add(false);
        }

        //beenTriggered[0] = true;

        textComponent = GetComponentInChildren<TextMeshProUGUI>();
        
        textComponent.text = string.Empty;

        choicesToggled = false;
        
        StartDialogue();
        
    }

    // Update is called once per frame
    void Update()
    {
        //display choices if dialogue reaches the last box
        //...
        if(textComponent.text == dialogueScript[currentScript].Lines[0] && (dialogueScript[currentScript].hasChoice == true) && choicesToggled ==false){
            
            choiceFunction.leftText = dialogueScript[currentScript].myChoices.leftChoice;
            choiceFunction.rightText = dialogueScript[currentScript].myChoices.rightChoice;

            choiceFunction.currentLeftID = dialogueScript[currentScript].myChoices.leftID;
            choiceFunction.currentRightID = dialogueScript[currentScript].myChoices.rightID;
            choiceFunction.ActivateChoice(true);
            choicesToggled = true;
            
        }

        // if(choicesToggled == true && choiceFunction.choicePressed == false){
        //     //display the pressed choices dialogue lines 
        // }

        
        if (Input.GetKeyDown(KeyCode.Return) && isToggled == true && choicesToggled == false)
        {
            if(textComponent.text == dialogueScript[currentScript].Lines[index]){
                NextLine();
            } else{
                StopAllCoroutines();
                textComponent.text = dialogueScript[currentScript].Lines[index];
            }
            
            
        }

        if(currentChapter == "1"){
            Chapter1Triggers();
        }else if(currentChapter == "2"){
            chapter2Triggers();
        }
       

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
        for(int i =1; i<=4; i++){
            TriggerScriptLine(1,i);
        }
    }

    void chapter2Triggers(){
        
        TriggerScriptLine(2,1);
        TriggerScriptLine(2,2);
        TriggerScriptLine(2,3);
        //TriggerScriptLine(2,4);

        if(choicesToggled == true && choiceFunction.choicePressed == false){
            //display the pressed choices dialogue lines
            if(textComponent.text == dialogueScript[1].Lines[index]){ //maybe this will work?
                if( GameManager.Instance.GetChoiceValue(200)){
                    dialogueScript[1].Lines[1] = dialogueScript[1].myChoices.GetLines(1,0);

                }else if(GameManager.Instance.GetChoiceValue(201)){
                    dialogueScript[1].Lines[1] = dialogueScript[1].myChoices.GetLines(2,0);
                }
                choicesToggled = false;
                NextLine();
                GameManager.Instance.SetTrigger(2,2,true);

            }else if(textComponent.text == dialogueScript[2].Lines[index]){
                if( GameManager.Instance.GetChoiceValue(202)){
                    dialogueScript[2].Lines[1] = dialogueScript[2].myChoices.GetLines(1,0);

                }else if(GameManager.Instance.GetChoiceValue(203)){
                    dialogueScript[2].Lines[1] = dialogueScript[2].myChoices.GetLines(2,0);
                }
                choicesToggled = false;
                NextLine();
                GameManager.Instance.SetTrigger(2,3,true);

            }else if(textComponent.text == dialogueScript[3].Lines[index]){
                if( GameManager.Instance.GetChoiceValue(204)){
                    dialogueScript[3].Lines[1] = dialogueScript[3].myChoices.GetLines(1,0);

                }else if(GameManager.Instance.GetChoiceValue(205)){
                    dialogueScript[3].Lines[1] = dialogueScript[3].myChoices.GetLines(2,0);
                }
                choicesToggled = false;
                NextLine();
                //GameManager.Instance.SetTrigger(2,4,true);
            }

        }
        
    }
    private void TriggerScriptLine(int chap, int curIndx){
        if((GameManager.Instance.GetTrigger(chap,curIndx))&& (isToggled == false)&& currentScript == curIndx){
            isToggled = true;
            ToggleChildren(true);
            GameManager.Instance.SetCutsceneTrigger(true);
            textComponent.text = string.Empty;          
            StartDialogue();
            GameManager.Instance.SetTrigger(chap,curIndx,false);   
        }

    }
}
