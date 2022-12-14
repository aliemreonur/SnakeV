using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeV.Utilities;
using UnityEngine.UI;
using SnakeV.Core;

namespace SnakeV.Managers
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private GameObject _gameLostPanel;

        private void Awake()
        {
            SingletonThisObj(this);
            _gameLostPanel.SetActive(false);
        }

        private void ActivateLostPanel()
        {
            _gameLostPanel.SetActive(true);
        }

        private void OnEnable()
        {
            PlayerController.Instance.OnPlayerDeath += ActivateLostPanel;
        }

        private void OnDisable()
        {
            PlayerController.Instance.OnPlayerDeath -= ActivateLostPanel;
        }

    }
}


