using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public InputActionAsset inputActionMap;
    private float horizontalMove = 0.0f;
    public Animator Animator;

    [Header("Audio")] 
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private AudioClip jumpAudio;
    [SerializeField] private AudioClip playerDamageAudio;
    
    [SerializeField] private float runSpeed = 10f;
    private bool jump;
    
    private void OnEnable()
    {
        inputActionMap["Player/Jump"].performed += OnJump;
        
        inputActionMap.Enable();
    }

    private void OnDisable()
    {
        inputActionMap["Player/Jump"].performed -= OnJump;
        
        inputActionMap.Disable();
    }
    
    private void FixedUpdate()
    {
        controller.Move(horizontalMove , jump);
        Animator.SetFloat("Speed", Math.Abs(horizontalMove));
        horizontalMove = inputActionMap["Player/Move"].ReadValue<Vector2>().x * runSpeed * Time.fixedDeltaTime;
    }

    public void Landed()
    {
        Animator.SetBool("Jump", false);
        jump = false;
    }

    private void OnJump(InputAction.CallbackContext ctx)
    {
        Animator.SetBool("Jump", true);
        PlayJumpSound();
        jump = true;
    }
    
    #region  Audio

    private void PlayJumpSound()
    {
        if (jump) return;
        if(playerAudioSource.isPlaying)
            playerAudioSource.Stop();
        playerAudioSource.clip = jumpAudio;
        playerAudioSource.Play();
    }

    private void PlayPlayerDamageSound()
    {
        if(playerAudioSource.isPlaying)
            playerAudioSource.Stop();
        playerAudioSource.clip = playerDamageAudio;
        playerAudioSource.Play();
    }
    #endregion
}