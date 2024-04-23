using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerInventory inv;
    public GameState State;

    private bool togInteractor = false;

    public static List<bool>ch1Trigger = new List<bool>();
    public static List<bool>ch2Trigger = new List<bool>();

    public static List<bool>gatesUnlocked = new List<bool>();

    private static bool insideCutscene;
    //public static event Action<GameState> OnGameStateChanged;
    private GameObject instantiatedCanvas;

    

    void Awake(){
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {   
        insideCutscene = true; 
        //may need to move to on awake
        for(int i =0; i< 10; i++){
            ch1Trigger.Add(false);
        }

        for(int i =0; i< 10; i++){
            gatesUnlocked.Add(false);
        }
        ch1Trigger[0] = true;

        //beenTriggered[0] = true;
        UpdateGameState(GameState.StartScreen);
        instantiatedCanvas = null;

        /** Delete ???? */
        inv.resetInv();
        /****************/
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (instantiatedCanvas != null){
        //     insideCutscene = true;
        // }else{
        //     insideCutscene = false;
        // }

        // Check if the player pressed the Escape key
        if (Input.GetKeyDown(KeyCode.Mouse1) && instantiatedCanvas != null)
        {
            // Destroy the instantiated canvas and its children
            Destroy(instantiatedCanvas);
            instantiatedCanvas = null;
            insideCutscene = false;
        }
        
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
    }

    public bool GetTrigger(int num, int indx){
        if(num == 1){
            return ch1Trigger[indx];
        }
        if(num == 2){
            return ch2Trigger[indx];
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

    public void OnApplicationQuit()
    {
        // Reset all bools in the lists when the application quits
        for(int i = 0; i < ch1Trigger.Count; i++)
        {
            ch1Trigger[i] = false;
        }

        for(int i = 0; i < ch2Trigger.Count; i++)
        {
            ch2Trigger[i] = false;
        }

        inv.resetInv();
    }

    
}

public enum GameState{
        Tutorial,
        StartScreen,
        GameOver
    }
