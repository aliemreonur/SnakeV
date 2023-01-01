using UnityEngine.SceneManagement;
using SnakeV.Utilities;
using UnityEngine;
using System;

namespace SnakeV.Core.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public bool GameRunning { get; private set; }
        public bool IsEdgesOn => _isEdgesOn;
        public float GameSpeed => _gameSpeed;
        public Action<bool> OnGameWon;
        public Action OnGameStart;
        public int BridgeSpawnTime => _bridgeSpawnTime;
        private int _bridgeSpawnTime = 10;

        [Range(10f,100f)]
        [SerializeField] private int _gameSpeed = 80;
        private bool _isEdgesOn;

        private void Awake()
        {
            base.Awake();
            SetGameEdges();
            SetGameSpeed();
        }

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

        private void SetGameSpeed()
        {
            if (PlayerPrefs.HasKey("GameSpeed"))
                _gameSpeed = PlayerPrefs.GetInt("GameSpeed");
            else
                _gameSpeed = 75;
        }

        private void SetGameEdges()
        {
            if (PlayerPrefs.HasKey("EdgesOn"))
                _isEdgesOn = PlayerPrefs.GetInt("EdgesOn") == 1 ? true : false;
            else
                _isEdgesOn = true;
        }

    }
}

