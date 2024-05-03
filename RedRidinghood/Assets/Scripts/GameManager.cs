using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerInventory inv;
    public GameState State;
    private new CinemachineVirtualCamera camera;

    private bool togInteractor = false;


    // for triggers other than dialogue
    public static List<bool>ch1Trigger = new List<bool>();
    public static List<bool>ch2Trigger = new List<bool>();
    public static List<bool>ch3Trigger = new List<bool>();

    public static List<bool>gatesUnlocked = new List<bool>();

    // for all triggers for choice dialogue
    public static Dictionary<int, bool> choicesSelected = new Dictionary<int, bool>();

    private static bool insideCutscene;
    //public static event Action<GameState> OnGameStateChanged;
    private GameObject instantiatedCanvas;
    static List<bool> levelUnlock = new List<bool>();

    public static int chap5ScenesAmount;

    

    void Awake(){
        Instance = this;

         for (int i = 0; i<5; i++){
            levelUnlock.Add(false);
        }
        levelUnlock[0] = true;


        //create the key values
        for (int key = 200; key <= 208; key++){
            // Assuming your dictionary is named choicesSelected
            if (!choicesSelected.ContainsKey(key)){
                choicesSelected.Add(key, false);
            }
        }

        for (int key = 300; key <= 303; key++)
        {
            if (!choicesSelected.ContainsKey(key))
            {
                choicesSelected.Add(key, false);
            }
        }

        // Setting cinemachine far clip plane
        camera = GameObject.FindGameObjectWithTag("ThirdPOVCamera").GetComponent<CinemachineVirtualCamera>();
        camera.m_Lens.FarClipPlane = 20f;
    }


    void Start()
    {   
        insideCutscene = false; 

        //may need to move to on awake
        for(int i =0; i< 10; i++){
            ch1Trigger.Add(false);
        }
        ch1Trigger[0] = true;

        for(int i =0; i< 10; i++){
            ch2Trigger.Add(false);
        }
        ch2Trigger[0] = true;

        for (int i = 0; i < 10; i++)
        {
            ch3Trigger.Add(false);
        }
        ch3Trigger[0] = true;

        //THIS IS JUST FOR DEBUGGING FOR CH2, DELETE LATER PLS
        ch2Trigger[1] = true;
        ch3Trigger[1] = true;


        for (int i =0; i< 10; i++){
            gatesUnlocked.Add(false);
        }
        

        //beenTriggered[0] = true;
        UpdateGameState(GameState.StartScreen);
        instantiatedCanvas = null;

        /** Delete ???? */
        inv.resetInv();
        /****************/
        
    }

    void Update()
    {
        // if (instantiatedCanvas != null){
        //     insideCutscene = true;
        // }else{
        //     insideCutscene = false;
        // }

        // Check if the player pressed the Escape key
        if (Input.GetKeyDown(KeyCode.Escape) && instantiatedCanvas != null)
        {
            // Destroy the instantiated canvas and its children
            Destroy(instantiatedCanvas);
            instantiatedCanvas = null;
            insideCutscene = false;
        }

        UpdateViewRange();

    }

    /*
     * When you collect a flower or put one flower down, it updates the far clip plane
     */
    public void UpdateViewRange()
    {
        camera.m_Lens.FarClipPlane = 15f + inv.flowerAmount * 5f;
    }

    public void UpdateGameState(GameState newstate){
        State = newstate;

        switch(newstate){
            case GameState.Tutorial:
                break;
            case GameState.StartScreen:
                break;
            case GameState.GameOver:
                break;

        }

        //OnGameStateChanged?.Invoke(newstate);

    }
    public void setCanvasNote(GameObject note){
        instantiatedCanvas = note;

    }

    public void SetTrigger(int num, int indx, bool b){
        if(num == 1){
            ch1Trigger[indx] = b;
        }
        if(num == 2){
            ch2Trigger[indx] = b;
        }
        if (num == 3)
        {
            ch3Trigger[indx] = b;
        }
    }

    public bool GetTrigger(int num, int indx){
        if(num == 1){
            return ch1Trigger[indx];
        }
        if(num == 2){
            return ch2Trigger[indx];
        }
        if (num == 3) {
            return ch3Trigger[indx];
        }
        return false;
    }

    public void SetGateUnlock(int indx, bool b){
        gatesUnlocked[indx] = b;   
    }
    public bool GetGateUnlock(int indx){
        return gatesUnlocked[indx];   
    }

    public void SetCutsceneTrigger(bool b){
        insideCutscene = b;
    }

    public bool GetCutsceneTrigger(){
        return insideCutscene;
    }

    public void ToggleInteractor(bool b){
        togInteractor = b;
    }

     public bool GetToggleInteractor(){
        return togInteractor;
    }

    public void OpenScene(string levelName){
        //previousScene = SceneManager.GetActiveScene ().name;
        SceneManager.LoadScene(levelName);
    }

    public void OpenLevel(int i){
        if(levelUnlock[i-1] == true){
            //previousScene = SceneManager.GetActiveScene ().name;
            SceneManager.LoadScene("Chapter" + i);
        }  
    }

    public string getSceneName(){
        return SceneManager.GetActiveScene ().name;
    }

    public void UnlockScene(){
        if(SceneManager.GetActiveScene ().name == "Chapter1"){
            levelUnlock[1] = true;
        } else if(SceneManager.GetActiveScene ().name == "Chapter2"){
            levelUnlock[2] = true;
        }else if(SceneManager.GetActiveScene ().name == "Chapter3"){
            levelUnlock[3] = true;
        }else if(SceneManager.GetActiveScene ().name == "Chapter4"){
            levelUnlock[4] = true;
        }else if(SceneManager.GetActiveScene ().name == "Chapter5"){
            levelUnlock[5] = true;
        }
    }

    public void SetChoiceValue(int key, bool b){
        if(!choicesSelected.ContainsKey(key)){
                choicesSelected.Add(key, b);
        }else{
            choicesSelected[key] = b;
        }

    }
    public bool GetChoiceValue(int key){
        return choicesSelected[key];
    }
    public void OnApplicationQuit(){
        // Reset all bools in the lists when the application quits
        for(int i = 0; i < ch1Trigger.Count; i++)
        {
            ch1Trigger[i] = false;
        }

        for(int i = 0; i < ch2Trigger.Count; i++)
        {
            ch2Trigger[i] = false;
        }

        for (int i = 0; i < ch3Trigger.Count; i++)
        {
            ch3Trigger[i] = false;
        }


        inv.resetInv();

        //reset choices
        foreach (var key in choicesSelected.Keys.ToList()){
            choicesSelected[key] = false;
        }
    }   
}

public enum GameState{
        Tutorial,
        StartScreen,
        GameOver
    }
