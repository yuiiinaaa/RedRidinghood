using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Detected = " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Enemy")
        {
            GameManager.Instance.OpenScene("GameOverScene");
        }
    }
}
