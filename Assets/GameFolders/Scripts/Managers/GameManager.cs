using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using SnakeV.Utilities;

namespace SnakeV.Core.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        private void Awake()
        {
            SingletonThisObj(this);
        }

        private void OnEnable()
        {
            PlayerController.Instance.OnPlayerDeath += ReloadScene;
        }

        private void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnDisable()
        {
            PlayerController.Instance.OnPlayerDeath -= ReloadScene;
        }



    }
}

