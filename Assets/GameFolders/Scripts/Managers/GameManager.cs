using UnityEngine.SceneManagement;
using SnakeV.Utilities;
using UnityEngine;
using System;

namespace SnakeV.Core.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public bool GameRunning { get; private set; }
        public Action<bool> OnGameWon;
        public Action OnGameStart;
        public int BridgeSpawnTime => _bridgeSpawnTime;
        private int _bridgeSpawnTime = 10;
        private PlayerController _playerController;

        private new void Awake()
        {
            base.Awake();
            _playerController = PlayerController.Instance;
        }


        public void StartGame()
        {
            GameRunning = true;
            _playerController.StartMoving();
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
            _playerController.OnPlayerDeath += GameLost;
        }

        private void OnDisable()
        {
            _playerController.OnPlayerDeath -= GameLost;
        }

    }
}

