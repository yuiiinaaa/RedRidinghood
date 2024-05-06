using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerJumpscare : MonoBehaviour
{
    public string sceneName;
    static string prevScene;
    static int doorsOpened;
    private int maxDoors;
    void Awake(){
        //doorsOpened = 0;
    }
    // Start is called before the first frame update
    void Start(){
        maxDoors = 15;

    
        
    }
    // Update is called once per frame
    void Update()
    {
        if(sceneName == "Ch5 S9"){
            if(Input.GetKeyDown(KeyCode.Return)){
                StartCoroutine(GameOver());
                //Play Jumpscare?
                //Wait for x amount of seconds to pass
                //Open GameOverScreen
            }
        }

        SceneOpen();

        
    }

    private void SceneOpen(){
        //This is for chapter 5 killing mechanic
        if(sceneName == "Ch5 S1"){
            doorsOpened = 0;
            prevScene = "Ch5 S1";
        }
        if(sceneName.Contains("Ch5") && (prevScene!=sceneName)){
            prevScene = sceneName;
            doorsOpened++;
            Debug.Log("Current Door Counter: " + doorsOpened);

            if(doorsOpened>=maxDoors){
                doorsOpened = 0;
                StartCoroutine(GameOver());
            }
        }
    }

        IEnumerator GameOver(){    
        // Play sound effect if assigned
        // if (interactSound != null)
        // {
        //     AudioSource.PlayClipAtPoint(interactSound, transform.position);
        // }
            yield return new WaitForSeconds(.8f);
            GameManager.Instance.OpenScene("GameOverScene");
        }
}
