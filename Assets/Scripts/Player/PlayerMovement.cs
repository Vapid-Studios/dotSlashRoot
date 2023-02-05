using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

class PlayerMovement : MonoBehaviour, ITakeDamage
{
    [SerializeField] private Stats playerStats;
    [SerializeField] private GameObject LoseUIPrefab;
    [Space]
   
    public CharacterController2D controller;
    public InputActionAsset inputActionMap;
    private float horizontalMove = 0.0f;
    
    [FormerlySerializedAs("Animator")]
    [Space]
    [Header("Animation")]
    public Animator playerAnimator;
    public Animator weaponAnimator;

    [FormerlySerializedAs("playerAudioSource")]
    [Header("Audio")] 
    [SerializeField] private AudioSource JumpAudioSource;
    [SerializeField] private AudioSource AttackAudioSource;
    [SerializeField] private AudioSource MusicAudioSource;
    [SerializeField] private AudioClip jumpAudio;
    [SerializeField] private AudioClip playerMeleeAudio;
    [SerializeField] private AudioClip playerDamageAudio;
    [SerializeField] private AudioClip DeathMusic;
    
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

        playerStats.currentHealth = 100;
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
        if(JumpAudioSource.isPlaying)
            JumpAudioSource.Stop();
        JumpAudioSource.clip = jumpAudio;
        JumpAudioSource.Play();
    }

    private void PlayMeleeSound()
    {
        if (AttackAudioSource.isPlaying) return;
        
        AttackAudioSource.clip = playerMeleeAudio;
        AttackAudioSource.Play();
    }
    
    private void PlayPlayerDamageSound()
    {
        if(AttackAudioSource.isPlaying)
            AttackAudioSource.Stop();
        AttackAudioSource.clip = playerDamageAudio;
        AttackAudioSource.Play();
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

    public void TakeDamage(int damage)
    {
        playerAnimator.SetTrigger("Hurt");
        playerStats.currentHealth -= damage;
        PlayPlayerDamageSound();

        if (playerStats.currentHealth <= 0)
        {
            Die();
        }

        StartCoroutine(InvincibiltyFrames());
    }

    private void Die()
    {
        Time.timeScale = 0f;
        playerAnimator.SetTrigger("Die");
        
        if(MusicAudioSource.isPlaying)
            AttackAudioSource.Stop();
        MusicAudioSource.clip = DeathMusic;
        MusicAudioSource.Play();
        
        Instantiate(LoseUIPrefab);
        //Time.timeScale = 0;
    }

    private IEnumerator InvincibiltyFrames()
    {
        inputActionMap.Disable();
        var projTRig = gameObject.GetComponent<ProjectileTrigger>();
        projTRig.enabled = false;
        yield return new WaitForSeconds(2f);
        projTRig.enabled = true;    
        inputActionMap.Enable();
    }
}