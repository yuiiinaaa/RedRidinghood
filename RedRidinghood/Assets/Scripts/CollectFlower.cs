using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrig : MonoBehaviour
{
    public PlayerInventory inv;
    private bool firstMovementPressed;
    private bool firstFlower;
    void Start(){
        firstMovementPressed = false;
        firstFlower = false;
    }

    void Update()
    {
        UpdatePlayerEvents();

    }

    //handle collider triggers
    private void OnTriggerEnter(Collider other){
        if(other.transform.tag == "GlowFlower"){
            inv.AddFlowerAmount(1);
            Debug.Log("Flower Amount" + inv.flowerAmount);
            Destroy(other.gameObject);

            if(!firstFlower){
                firstFlower = true;
                StartCoroutine(DelayThenReturnTrue(.2f, 1, 2));
            }


        }
    }

    IEnumerator DelayThenReturnTrue(float f, int chap, int indx)
    {
        yield return new WaitForSeconds(f); // Wait for 4 seconds
        Debug.Log("True after delay");
        GameManager.Instance.SetTrigger(chap, indx,true);
    }

    private void UpdatePlayerEvents(){
         //if the player has moved for the first time
        if (!firstMovementPressed && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)))
        {   
            StartCoroutine(DelayThenReturnTrue(6f, 1, 1));
            firstMovementPressed = true;
        }

        // if(firstFlower){
        //     //GameManager.Instance.SetTrigger(1,2,true);
        // }

    }
}
