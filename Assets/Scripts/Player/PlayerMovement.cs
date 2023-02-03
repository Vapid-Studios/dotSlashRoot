using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    private float horizontalMove = 0.0f;
    public Animator Animator;

    public float runSpeed = 10f;
    private bool jump;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed * Time.fixedDeltaTime;
        jump = Input.GetButton("Jump");
        Animator.SetFloat("Speed", Math.Abs(horizontalMove));
        
        if (jump)
        {
            Animator.SetBool("Jump", true);
        }
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove , jump);
    }

    public void Landed()
    {
        Animator.SetBool("Jump", false);
    }
}
