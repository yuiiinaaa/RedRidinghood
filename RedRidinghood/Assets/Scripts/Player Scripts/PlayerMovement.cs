using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 m_Movement;
    Animator m_Animator;
    Quaternion m_Rotation = Quaternion.identity;  // for storing rotation of player: default set as no rotation 
    Rigidbody m_Rigidbody;
    //AudioSource audioData;
    AudioSource walkSound;
    public float velocity = 15f;


    //movement speed in units per second
    public float movementSpeed = 2f;

    public bool walkToggle;
    SpriteRenderer sr;


    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();

        walkSound = GetComponent<AudioSource>();
        walkToggle = false;
        sr = GetComponent<SpriteRenderer>();
        //m_Animator.SetBool("Forward", false);
    }

    // FixedUpdate is called before physics system solves any collisions/interactions
    // setting movement vector and rotation, runs in time with physics loop (?)
    void FixedUpdate()
    {
        //Debug.Log(GameManager.Instance.GetCutsceneTrigger());
        if(!GameManager.Instance.GetCutsceneTrigger()){
            UpdateMovement();
        } else
        {
            m_Animator.SetBool("Forward", false);
            m_Animator.SetBool("Right", false);
            m_Animator.SetBool("Left", false);
            m_Animator.SetBool("Torwards", false);
        }
    }

    void UpdateMovement(){
        // getting input from keyboard
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //m_Movement.Set(horizontal, 0f, vertical);
        //m_Movement.Normalize();

        // setting animator bool for animation transitions
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);

        bool isWalking = hasHorizontalInput || hasVerticalInput;
        //m_Animator.SetBool("IsWalking", isWalking);
        

        //Dasol trying to add walking sfx
        if (isWalking && walkToggle == false)
        {
            walkSound.Play();
            walkToggle = true;
            //AudioSource.PlayClipAtPoint(walkSound, transform.position);
            //Debug.Log("iswalking");
        }
        if (!isWalking)
        {
            walkSound.Stop();
            walkToggle = false;
        }

        m_Rigidbody.velocity = new Vector3(horizontal, 0f, vertical).normalized * movementSpeed + new Vector3(0f, m_Rigidbody.velocity.y, 0f);

        //Dasol trying things to get it to move
        if (vertical > 0)
        {
            m_Animator.SetBool("Forward", true);
            m_Rigidbody.velocity = m_Rigidbody.velocity.normalized * 7f;
            //Debug.Log(m_Rigidbody.velocity.x);
            //Debug.Log(m_Rigidbody.velocity.z);
        } else
        {
            m_Animator.SetBool("Forward", false);
        }
        if (vertical < 0)
        {
            m_Animator.SetBool("Towards", true);
            m_Rigidbody.velocity = m_Rigidbody.velocity.normalized * 7f;
        }
        else
        {
            m_Animator.SetBool("Towards", false);
        }


        if (horizontal > 0)
        {
            m_Animator.SetBool("Right", true);
            //Debug.Log(m_Rigidbody.velocity.x);
            //Debug.Log(m_Rigidbody.velocity.z);

        } else
        {
            m_Animator.SetBool("Right", false);
        }
        if (horizontal < 0)
        {
            m_Animator.SetBool("Left", true);
            sr.flipX = true;
  

        } else
        {
            m_Animator.SetBool("Left", false);
            sr.flipX = false;
  
        }

        //m_Rigidbody.velocity = new Vector3(horizontal, 0f, vertical).normalized;
        //m_Rigidbody.velocity = new Vector3(horizontal, 0f, vertical).normalized * movementSpeed + new Vector3(0f, m_Rigidbody.velocity.y, 0f);

        
    }

    // allows applying root motion
    //void OnAnimatorMove()
    //{
    //    // set position to: current position + movement vector * magnitude of animator's delta position (change in position due to root motion)
    //    //m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);

    //    // apply rotation: directly setting new rotation
    //    //m_Rigidbody.MoveRotation(m_Rotation);
    //}

    
}
