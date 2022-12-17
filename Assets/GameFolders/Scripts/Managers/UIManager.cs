using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeV.Utilities;
using UnityEngine.UI;
using SnakeV.Core;
using System;

namespace SnakeV.Core.Managers
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private GameObject _gameLostPanel;


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
       

    }
}


