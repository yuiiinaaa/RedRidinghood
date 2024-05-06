using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using Cinemachine;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public Animator transition;
    public static GameManager Instance;
    static string previousScene;
    public PlayerInventory inv;
    public GameState State;
    private new CinemachineVirtualCamera camera;

    private bool togInteractor = false;


    // for triggers other than dialogue
    public static List<bool>ch1Trigger = new List<bool>();
    public static List<bool>ch2Trigger = new List<bool>();
    public static List<bool>ch3Trigger = new List<bool>();
    public static List<bool>ch5Trigger = new List<bool>();

    public static List<bool>chEndTrigger = new List<bool>();

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

        for (int key = 300; key <= 307; key++)
        {
            if (!choicesSelected.ContainsKey(key))
            {
                choicesSelected.Add(key, false);
            }
        }

        for (int key = 500; key <= 512; key++)
        {
            if (!choicesSelected.ContainsKey(key))
            {
                choicesSelected.Add(key, false);
            }
        }

        // Setting cinemachine far clip plane
        //commented out bc was running into problems with chapter 5
        camera = null;
        if (!GameObject.FindGameObjectWithTag("ThirdPOVCamera").IsUnityNull())
        {
            camera = GameObject.FindGameObjectWithTag("ThirdPOVCamera").GetComponent<CinemachineVirtualCamera>();
            camera.m_Lens.FarClipPlane = 20f;
        }
    }


    void Start()
    {   
        insideCutscene = false; 
        UnlockScene();

        //may need to move to on awake
        for(int i =0; i< 10; i++){
            ch1Trigger.Add(false);
            ch2Trigger.Add(false);
            ch3Trigger.Add(false);
            ch5Trigger.Add(false);
            chEndTrigger.Add(false);
        }
        ch1Trigger[0] = true;
        ch2Trigger[0] = true;
        ch3Trigger[0] = true;
        ch5Trigger[0] = true;
        chEndTrigger[0] = true;

        //THIS IS JUST FOR DEBUGGING FOR CH2, DELETE LATER PLS
        ch2Trigger[1] = true;
        ch3Trigger[1] = true;
        ch5Trigger[1] = true;
        chEndTrigger[1] = true;


        for (int i =0; i< 10; i++){
            gatesUnlocked.Add(false);
        }
        

        //beenTriggered[0] = true;
        UpdateGameState(GameState.StartScreen);
        instantiatedCanvas = null;

        /** Delete ???? */
        //inv.resetInv();
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

        if (camera != null)
        {
            UpdateViewRange();
        }

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
        if (num == 5)
        {
            ch5Trigger[indx] = b;
        }
        if (num == 6) {
            chEndTrigger[indx] = b;
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
        if (num == 5) {
            return ch5Trigger[indx];
        }
        if (num == 6) {
            return chEndTrigger[indx];
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
        previousScene = SceneManager.GetActiveScene ().name;
        StartCoroutine(loadScene(levelName));
       
    }

    public void OpenPrevScene(){
       StartCoroutine(loadScene(previousScene));
    }

    public void OpenLevel(int i){
        if(levelUnlock[i-1] == true){
            previousScene = SceneManager.GetActiveScene ().name;
            SceneManager.LoadScene("Chapter" + i);
        }  
    }

    public string getSceneName(){
        return SceneManager.GetActiveScene ().name;
    }

    public void UnlockScene(){
        if(SceneManager.GetActiveScene ().name == "Ch1 Official"){
            levelUnlock[0] = true;
        } else if(SceneManager.GetActiveScene ().name == "Ch2 Official"){
            levelUnlock[1] = true;
        }else if(SceneManager.GetActiveScene ().name == "Ch3 Official"){
            levelUnlock[2] = true;
        }else if(SceneManager.GetActiveScene ().name == "Ch4 Official"){
            //levelUnlock[3] = true;
        }else if(SceneManager.GetActiveScene ().name == "Ch5 Official"){
            levelUnlock[3] = true;
        }
    }

    public async void OpenLastPlayedLevel(){
        int lastLvl = 0;

        for(int i = 0; i<6; i++){
            if(levelUnlock[i]==true){
                lastLvl = i;
            }else{
                break;
            }
        }
        if(lastLvl == 0){
            StartCoroutine(loadScene("Ch1 Official"));
            
        }else if(lastLvl == 1){
            StartCoroutine(loadScene("Ch2 Official"));
            
        }else if(lastLvl == 2){
            StartCoroutine(loadScene("Ch3 Official"));
           
        }else if(lastLvl == 3){
            StartCoroutine(loadScene("Ch5 S1"));
           
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
        for (int i = 0; i < ch5Trigger.Count; i++)
        {
            ch5Trigger[i] = false;
        }


        inv.resetInv();

        //reset choices
        foreach (var key in choicesSelected.Keys.ToList()){
            choicesSelected[key] = false;
        }
    } 

    public void QuitGame(){
        Application.Quit();
    }  

    IEnumerator loadScene(string scene){
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(scene);
    }
}

public enum GameState{
        Tutorial,
        StartScreen,
        GameOver
    }
