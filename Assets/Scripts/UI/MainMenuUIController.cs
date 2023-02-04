using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainMenuUIController : MonoBehaviour
{
    // For navigating hats
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private EventSystem eventSystem;
    
    [Header("Audio")]
    [SerializeField] private AudioSource UISFX;
    [SerializeField] private AudioClip errorAudio;
    
    [Header("Buttons")]
    [SerializeField] private GameObject ButtonParent;
    [FormerlySerializedAs("playButton")] 
    [SerializeField] private Button HatButton;
    [SerializeField] private Button SettingsButton;
    [SerializeField] private Button QuitButton;
    [SerializeField] private Button CreditsButton;
    [SerializeField] private Button GoButton;
    [SerializeField] private Button BackButton;

    [SerializeField] private Canvas creditsCanvas;
    [SerializeField] private Canvas CharacterCreaterCanvas;

    [Header("Hats")] 
    [SerializeField] private Image hat;
    [SerializeField] private List<Sprite> Hats;
    private int currentHatIndex = 0;
    
    private void OnEnable()
    {
        HatButton.onClick.AddListener(OnHatBtnClick);
        SettingsButton.onClick.AddListener(OnSettingsBtnClick);
        QuitButton.onClick.AddListener(OnQuitBtnClick);
        CreditsButton.onClick.AddListener(OnCreditsBtnClick);
        
        GoButton.onClick.AddListener(OnGoBtnClick);
        BackButton.onClick.AddListener(OnBackBtnClick);

        inputActions["UI/Navigate"].performed += NavigateHat;
    }

    private void OnDisable()
    {
        HatButton.onClick.RemoveListener(OnHatBtnClick);
        SettingsButton.onClick.RemoveListener(OnSettingsBtnClick);
        QuitButton.onClick.RemoveListener(OnQuitBtnClick);
        CreditsButton.onClick.RemoveListener(OnCreditsBtnClick);
    }

    private void OnHatBtnClick()
    {
        CharacterCreaterCanvas.enabled = true;
        ButtonParent.SetActive(false);

        eventSystem.firstSelectedGameObject = BackButton.gameObject;
    }

    private void OnSettingsBtnClick()
    {
        // Implement Options if time permits
    }

    private void OnQuitBtnClick()
    {
        #region EditorCode

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
                 Application.OpenURL(webplayerQuitURL);
#endif

        #endregion

        Application.Quit(0);
    }

    private void OnCreditsBtnClick()
    {
        creditsCanvas.enabled = !creditsCanvas.enabled;
    }

    private void OnGoBtnClick()
    {
        // SceneManager transition to 1-1
    }

    private void OnBackBtnClick()
    {
        CharacterCreaterCanvas.enabled = false;
        ButtonParent.SetActive(true);

        eventSystem.firstSelectedGameObject = HatButton.gameObject;
    }
    
    private void NavigateHat(InputAction.CallbackContext ctx)
    {
        if (!CharacterCreaterCanvas.enabled) return;

        float input = ctx.ReadValue<Vector2>().x;
        
        if (input < 0 && currentHatIndex > 0)
        {
            currentHatIndex--;
            hat.sprite = Hats[currentHatIndex];
        }
        else if(input > 0 && currentHatIndex < Hats.Count - 1)
        {
            currentHatIndex++;
            hat.sprite = Hats[currentHatIndex];
        }
        else
        {
            if(UISFX.isPlaying)
                UISFX.Stop();
            UISFX.clip = errorAudio;
            UISFX.Play();
        }
    }
}
