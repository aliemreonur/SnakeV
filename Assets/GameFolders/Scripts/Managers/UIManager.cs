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
        [SerializeField] private GameObject _gameLostPanel;
        [SerializeField] private TextMeshProUGUI _scoreText;

        protected override void Awake()
        {
            base.Awake();
            _gameLostPanel.SetActive(false);
        }

        public void ActivateLostPanel()
        {
            _gameLostPanel.SetActive(true);
        }

        public void ActivateWonPanel()
        {
            //activate game won panel
        }

        public void UpdateScore(uint score)
        {
            _scoreText.text = score.ToString();
        }

    }
}


