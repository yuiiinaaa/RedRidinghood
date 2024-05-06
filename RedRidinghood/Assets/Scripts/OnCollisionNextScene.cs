using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionNextScene : MonoBehaviour
{
   //AudioSource audioSource;
    public string openSceneName;

    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
    //     foreach (ContactPoint contact in collision.contacts)
    //     {
    //         Debug.DrawRay(contact.point, contact.normal, Color.white);
    //     }
    //     if (collision.relativeVelocity.magnitude > 2)
    //         audioSource.Play();
    GameManager.Instance.OpenScene(openSceneName);

    }

}
