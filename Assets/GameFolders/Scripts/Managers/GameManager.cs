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

        //use a scneemanager instead?
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
}

