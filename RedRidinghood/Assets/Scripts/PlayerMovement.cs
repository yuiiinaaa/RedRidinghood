using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 m_Movement;
    private Animator m_Animator;
    private Quaternion m_Rotation = Quaternion.identity;  // for storing rotation of player: default set as no rotation 
    private Rigidbody rb;

    //movement speed in units per second
    public float movementSpeed = 5f;

    // public float turnSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // FixedUpdate is called before physics system solves any collisions/interactions
    // setting movement vector and rotation, runs in time with physics loop (?)
    void FixedUpdate()
    {
        if(!GameManager.Instance.GetCutsceneTrigger()){
            UpdateMovementPatrick();
        }
    }

    void UpdateMovementPatrick()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontal, 0f, vertical).normalized * movementSpeed;
        Vector3 appliedGravityToMovement = movementDirection + new Vector3(0f, rb.velocity.y, 0f);

        rb.velocity = appliedGravityToMovement;
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
        m_Animator.SetBool("IsWalking", isWalking);


        //Dasol trying things
        if(vertical > 0)
        {
            m_Animator.SetBool("Forward", true);
        } else
        {
            m_Animator.SetBool("Forward", false);
        }
        if (horizontal > 0)
        {
            m_Animator.SetFloat("Side", horizontal);
        }
        if (horizontal < 0)
        {
            m_Animator.SetFloat("Side", horizontal);
        }


        // creating rotation for player
        //Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        //m_Rotation = Quaternion.LookRotation(desiredForward);


        transform.position = transform.position + new Vector3(horizontal * movementSpeed * Time.deltaTime, 0f, vertical * movementSpeed * Time.deltaTime); ;
    }

    // allows applying root motion
    void OnAnimatorMove()
    {
        // set position to: current position + movement vector * magnitude of animator's delta position (change in position due to root motion)
        rb.MovePosition(rb.position + m_Movement * m_Animator.deltaPosition.magnitude);

        // apply rotation: directly setting new rotation
        rb.MoveRotation(m_Rotation);
    }
}
