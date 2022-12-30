using UnityEngine.SceneManagement;
using SnakeV.Utilities;
using UnityEngine;
using System;

namespace SnakeV.Core.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public bool GameRunning { get; private set; }
        public bool IsEdgesOn;
        public float GameSpeed => _gameSpeed;
        public Action<bool> OnGameWon;
        public Action OnGameStart;
        public int BridgeSpawnTime => _bridgeSpawnTime;
        private int _bridgeSpawnTime = 10;

        [Range(0.5f,0.95f)]
        [SerializeField] private float _gameSpeed = 0.8f;

        public void StartGame()
        {
            GameRunning = true;
            PlayerController.Instance.StartMoving();
            OnGameStart?.Invoke();
        }

        public void GameLost()
        {
            GameRunning = false;
            OnGameWon?.Invoke(false);
        }

        public void GameWon()
        {
            GameRunning = false;
            OnGameWon?.Invoke(true);
        }

        public void ReloadScene()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnEnable()
        {
            PlayerController.Instance.OnPlayerDeath += GameLost;
        }

        private void OnDisable()
        {
            PlayerController.Instance.OnPlayerDeath -= GameLost;
        }

        protected void SetGameSpeed()
        {
            if (PlayerPrefs.HasKey("GameSpeed"))
                _gameSpeed = PlayerPrefs.GetFloat("GameSpeed");
            else
                _gameSpeed = 0.8f;
        }

    }
}

