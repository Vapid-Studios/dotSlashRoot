using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuUIController : MonoBehaviour
{
    [SerializeField] private Button ResumeBtn;
    [SerializeField] private Button OptionsBtn;
    [SerializeField] private Button MainMenuBtn;
    [SerializeField] private Button QuitToDesktopBtn;

    [SerializeField] private String titleText = "Paused";
    [SerializeField] private TMP_Text TMP_Title;
        
        private void OnEnable()
    {
        ResumeBtn.onClick.AddListener(OnResumeBtnClick);
        OptionsBtn.onClick.AddListener(OnOptionsBtnClick);
        MainMenuBtn.onClick.AddListener(OnMainMenuBtnClick);
        QuitToDesktopBtn.onClick.AddListener(OnQuitToDesktopBtnClick);

        TMP_Title.text = titleText;
    }

    private void OnDisable()
    {
        ResumeBtn.onClick.RemoveListener(OnResumeBtnClick);
        OptionsBtn.onClick.RemoveListener(OnOptionsBtnClick);
        MainMenuBtn.onClick.RemoveListener(OnMainMenuBtnClick);
        QuitToDesktopBtn.onClick.RemoveListener(OnQuitToDesktopBtnClick);
    }

    private void OnResumeBtnClick()
    {
        
    }
    
    private void OnOptionsBtnClick()
    {
        
    }
    
    private void OnMainMenuBtnClick()
    {
        
    }
    
    private void OnQuitToDesktopBtnClick()
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
}
