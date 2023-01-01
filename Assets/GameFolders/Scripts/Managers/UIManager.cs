using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeV.Utilities;
using UnityEngine.UI;
using SnakeV.Core;
using System;
using TMPro;

namespace SnakeV.Core.Managers
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private GameObject _introPanel;
        [SerializeField] private GameObject _gameLostPanel;
        [SerializeField] private GameObject _gameWonPanel;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private GameObject _newHighScore;

        public void UpdateScore(int score)
        {
            _scoreText.text = score.ToString();
        }

        public void StartGame()
        {
            _introPanel.SetActive(false);
            GameManager.Instance.StartGame();
        }

        protected override void Awake()
        {
            base.Awake();
            _gameLostPanel.SetActive(false);
            _gameWonPanel.SetActive(false);
            _newHighScore.SetActive(false);
        }

        private void ActivateEndGamePanel(bool gameWon)
        {
            if (gameWon)
                ActivateWonPanel();
            else
                ActivateLostPanel();
            CheckHighestScore();
        }

        private void ActivateLostPanel()
        {
            _gameLostPanel.SetActive(true);
        }

        private void ActivateWonPanel()
        {
            Debug.Log("Game Won"); //no win conditions set just yet
            _gameWonPanel.SetActive(true);
        }

        private void OnEnable()
        {
            GameManager.Instance.OnGameWon += ActivateEndGamePanel;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnGameWon -= ActivateEndGamePanel;
        }

        private void CheckHighestScore()
        {
            int currentScore = PlayerController.Instance.Score;

            if (PlayerPrefs.HasKey("HighestScore"))
            {
                int highestScore = PlayerPrefs.GetInt("HighestScore");
                if (currentScore > highestScore)
                    _newHighScore.SetActive(true);
            }
            else
            {
                PlayerPrefs.SetInt("HighestScore", currentScore);
                _newHighScore.SetActive(true);
            }
                
        }

    }
}


