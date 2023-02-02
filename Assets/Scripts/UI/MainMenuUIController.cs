using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button SettingsButton;
    [SerializeField] private Button QuitButton;
    [SerializeField] private Button CreditsButton;

    [SerializeField] private Canvas creditsCanvas;
    private void OnEnable()
    {
        playButton.onClick.AddListener(OnStartBtnClick);
        SettingsButton.onClick.AddListener(OnSettingsBtnClick);
        QuitButton.onClick.AddListener(OnQuitBtnClick);
        CreditsButton.onClick.AddListener(OnCreditsBtnClick);
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveListener(OnStartBtnClick);
        SettingsButton.onClick.RemoveListener(OnSettingsBtnClick);
        QuitButton.onClick.RemoveListener(OnQuitBtnClick);
        CreditsButton.onClick.RemoveListener(OnCreditsBtnClick);
    }

    private void OnStartBtnClick()
    {
        // Implement SceneManager
        // Transition to start of game
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
}
