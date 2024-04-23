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

    //movement speed in units per second
    private float movementSpeed = 5f;

    public float turnSpeed = 20f;
    //public bool left = false;
    //public AudioClip walkSound;

    //public bool walkPlay;
    public bool walkToggle;


    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();

        walkSound = GetComponent<AudioSource>();
        walkToggle = false;
        //m_Animator.SetBool("Forward", false);
    }

    // FixedUpdate is called before physics system solves any collisions/interactions
    // setting movement vector and rotation, runs in time with physics loop (?)
    void FixedUpdate()
    {
        //if(!GameManager.Instance.GetCutsceneTrigger()){
            UpdateMovement();
        //}
    }

    void UpdateMovement(){
        // getting input from keyboard
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

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
            Debug.Log("iswalking");
        }
        if (!isWalking)
        {
            walkSound.Stop();
            walkToggle = false;
        }


        //Dasol trying things to get it to move
        if(vertical > 0)
        {
            m_Animator.SetBool("Forward", true);
        } else
        {
            m_Animator.SetBool("Forward", false);
        }
        if (vertical < 0)
        {
            m_Animator.SetBool("Towards", true);
        }
        else
        {
            m_Animator.SetBool("Towards", false);
        }


        if (horizontal > 0)
        {
            m_Animator.SetBool("Right", true);
 
        } else
        {
            m_Animator.SetBool("Right", false);
        }
        if (horizontal < 0)
        {
            m_Animator.SetBool("Left", true);
            //left = true;
            m_Rotation = Quaternion.Euler(new Vector3(0, 180f, 0));

        } else
        {
            m_Animator.SetBool("Left", false);
            m_Rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        
        // creating rotation for player
        //Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        //m_Rotation = Quaternion.LookRotation(desiredForward);
        //if (left)
        //{
        //    m_Rotation = Quaternion.Euler(new Vector3(0, 180f, 0));
        //} else
        //{
            
        //}
        transform.position = transform.position + new Vector3(horizontal * movementSpeed * Time.deltaTime, 0f, vertical * movementSpeed * Time.deltaTime); 
    }

    // allows applying root motion
    void OnAnimatorMove()
    {
        // set position to: current position + movement vector * magnitude of animator's delta position (change in position due to root motion)
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);

        // apply rotation: directly setting new rotation
        m_Rigidbody.MoveRotation(m_Rotation);
    }

    
}
