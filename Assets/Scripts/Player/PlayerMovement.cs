using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Stats playerStats;
    
    [Space]
   
    public CharacterController2D controller;
    public InputActionAsset inputActionMap;
    private float horizontalMove = 0.0f;
    
    [FormerlySerializedAs("Animator")]
    [Space]
    [Header("Animation")]
    public Animator playerAnimator;
    public Animator weaponAnimator;

    [Header("Audio")] 
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private AudioClip jumpAudio;
    [SerializeField] private AudioClip playerMeleeAudio;
    [SerializeField] private AudioClip playerDamageAudio;
    
    [SerializeField] private float runSpeed = 10f;
    private bool jump;
    private bool FireButtonHeld = false;

    #region Built-in Functions

    private void OnEnable()
    {
        // Input Actions
        inputActionMap["Player/Jump"].performed += OnJump;
        //inputActionMap["Player/Fire"].performed += OnFire;
        
        inputActionMap.Enable();
        //
        
        // Player Stat Events
        playerStats.onAttackSpeedChanged.AddListener(ChangeAttackSpeed);
        //
    }

    private void OnDisable()
    {
        inputActionMap["Player/Jump"].performed -= OnJump;
        //inputActionMap["Player/Fire"].performed -= OnFire;

        inputActionMap.Disable();
    }
    
    private void FixedUpdate()
    {
        if (inputActionMap["Player/Fire"].ReadValue<float>() < 0.1f)
        {
            weaponAnimator.SetTrigger("StopMelee");
        }
        if (inputActionMap["Player/Fire"].ReadValue<float>() > 0.1)
        {
            OnFire();
        }
        
        controller.Move(horizontalMove , jump);
        playerAnimator.SetFloat("Speed", Math.Abs(horizontalMove));
        horizontalMove = inputActionMap["Player/Move"].ReadValue<Vector2>().x * runSpeed * Time.fixedDeltaTime;
    }
    

    #endregion
    
    #region  Audio
    private void PlayJumpSound()
    {
        if (jump) return;
        if(playerAudioSource.isPlaying)
            playerAudioSource.Stop();
        playerAudioSource.clip = jumpAudio;
        playerAudioSource.Play();
    }

    private void PlayMeleeSound()
    {
        if (playerAudioSource.isPlaying) return;
        
        playerAudioSource.clip = playerMeleeAudio;
        playerAudioSource.Play();
    }
    
    private void PlayPlayerDamageSound()
    {
        if(playerAudioSource.isPlaying)
            playerAudioSource.Stop();
        playerAudioSource.clip = playerDamageAudio;
        playerAudioSource.Play();
    }

    private void ChangeAttackSpeed()
    {
        weaponAnimator.SetFloat("AttackSpeed", playerStats.attackSpeed);
    }
    
    bool isAnimationPlaying(Animator anim, string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }
    #endregion
    
    public void Landed()
    {
        playerAnimator.SetBool("Jump", false);
        jump = false;
    }

    private void OnJump(InputAction.CallbackContext ctx)
    {
        playerAnimator.SetBool("Jump", true);
        PlayJumpSound();
        jump = true;
    }

    private void OnFire()
    {
        if (isAnimationPlaying(weaponAnimator, "MeleeSwing") || 
            weaponAnimator.GetBool("FireMelee")) return;
        
        weaponAnimator.SetTrigger("FireMelee");
        PlayMeleeSound();
    }
    
}