using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerInventory inv;
    public GameState State;
    //public static event Action<GameState> OnGameStateChanged;

    

    void Awake(){
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateGameState(GameState.StartScreen);
        
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

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum GameState{
        Tutorial,
        StartScreen,
        GameOver
    }
