using System;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

namespace Managers
{
    public enum GameStates
    {
        Start,
        Pause,
        Ingame,
        Death,
        Win
    }

    public class GameManager : MonoBehaviour
    {
        #region Singleton

        public static GameManager Instance;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        #endregion
        
        private GameStates State;
        private bool paused;

        void Start()
        {
            UpdateGameState(GameStates.Start);
        }
        
        public static event Action<GameStates> OnGameStateChanged;
        public void UpdateGameState(GameStates state)
        {
            State = state;
            switch (state)
            {
                case GameStates.Start:
                    break;
                case GameStates.Pause:
                    Pause(!paused);
                    break;
                case GameStates.Ingame:
                    break;
                case GameStates.Death:
                    break;
                case GameStates.Win:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }

            OnGameStateChanged?.Invoke(State);
        }
        
        private void Pause(bool value)
        {
            if (value)
            {
                State = GameStates.Pause;
                Time.timeScale = 0.0f; //pause game physics
            }
            else
            {
                State = GameStates.Ingame;
                Time.timeScale = 1.0f; // resume game physics
            }
        }
    }
}