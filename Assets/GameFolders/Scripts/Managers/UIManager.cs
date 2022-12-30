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

        protected override void Awake()
        {
            base.Awake();
            _gameLostPanel.SetActive(false);
            _gameWonPanel.SetActive(false);
        }

        private void ActivateEndGamePanel(bool gameWon)
        {
            if (gameWon)
                ActivateWonPanel();
            else
                ActivateLostPanel();
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

        public void UpdateScore(uint score)
        {
            _scoreText.text = score.ToString();
        }

        public void StartGame()
        {
            _introPanel.SetActive(false);
            GameManager.Instance.StartGame();
        }

        private void OnEnable()
        {
            GameManager.Instance.OnGameWon += ActivateEndGamePanel;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnGameWon -= ActivateEndGamePanel;
        }

    }
}


