using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private GameObject player;
    private GameManager gm;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (gm == null)
        {
            Debug.Log("Missing Game Manager! - Death.cs");
        }

        if (FellIntoHole())
        {
            Debug.Log("Whoop, fell into whole - Death.cs");
            gm.OpenScene("GameOverScene");
        }

    }

    private bool FellIntoHole()
    {
        if (player == null)
        {
            Debug.Log("Missing Player Object! - Death.cs");
            return false;
        }
        return player.transform.position.y <= -30f;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Detected = " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Enemy")
        {
            GameManager.Instance.OpenScene("GameOverScene");
        }
    }
}
