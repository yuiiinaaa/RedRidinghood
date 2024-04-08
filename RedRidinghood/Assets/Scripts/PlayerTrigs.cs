using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrig : MonoBehaviour
{
    public PlayerInventory inv;
        public GameObject prefabFlower;
    private bool firstMovementPressed;
    private bool firstFlower;
    private bool firstPlant;
    void Start(){
        firstMovementPressed = false;
        firstFlower = false;
        firstPlant = false;
    }
    void Update()
    {
        UpdatePlayerEvents();


        //Plants the flower prefab
        if (Input.GetKeyDown(KeyCode.P)){
            Spawn();
            inv.RemoveFlowerAmount(1);
            if (!firstPlant){
                firstPlant = true;
                StartCoroutine(DelayThenReturnTrue(0f, 1, 3));
            }
        }

    }

    //handle collider triggers
    private void OnTriggerEnter(Collider other){
        // if(other.transform.tag == "GlowFlower"){
        //     inv.AddFlowerAmount(1);
        //     Debug.Log("Flower Amount" + inv.flowerAmount);
        //     Destroy(other.gameObject);

        //     if(!firstFlower){
        //         firstFlower = true;
        //         StartCoroutine(DelayThenReturnTrue(.2f, 1, 2));
        //     }
        // }
    }

    IEnumerator DelayThenReturnTrue(float f, int chap, int indx)
    {
        yield return new WaitForSeconds(f); // Wait for 4 seconds
        Debug.Log("True after delay");
        GameManager.Instance.SetTrigger(chap, indx,true);
    }

    private void UpdatePlayerEvents(){
         //if the player has moved for the first time
        if(GameManager.Instance.GetCutsceneTrigger() == false){
            if (!firstMovementPressed && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)))
            {   
                StartCoroutine(DelayThenReturnTrue(3f, 1, 1));
                firstMovementPressed = true;
            }

            if(inv.flowerAmount > 1 && !firstFlower){
                firstFlower = true;
                StartCoroutine(DelayThenReturnTrue(.2f, 1, 2));
            }

            // if (!firstPlant && (Input.GetKeyDown(KeyCode.P))){
            //     firstPlant = true;

            // }
        }




        // if(firstFlower){
        //     //GameManager.Instance.SetTrigger(1,2,true);
        // }

    }
    void Spawn(){
        // Ensure that the prefab to spawn is assigned
        if (prefabFlower != null){
            // Instantiate the prefab at the current position and rotation of this GameObject
            Vector3 transPos = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            GameObject spawnedObject = Instantiate(prefabFlower, transPos, Quaternion.identity);
        }
        else{
            Debug.LogWarning("Prefab to spawn is not assigned!");
        }
    }
}
