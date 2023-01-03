using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeV.Core;

namespace SnakeV.Core.Abstracts
{
    public class PreferenceSetter
    {
        public int GameSpeed => _gameSpeed;
        private int _gameSpeed;
        public bool IsEdgesOn => _isEdgesOn;
        private bool _isEdgesOn;

        public void Initialize()
        {
            SetGameSpeed();
            SetGameEdges();
        }

        private void SetGameSpeed()
        {
            _gameSpeed = PlayerPrefs.HasKey("GameSpeed") ? PlayerPrefs.GetInt("GameSpeed") : 75;
        }

        public void SetGameEdges()
        {
            if (PlayerPrefs.HasKey("EdgesOn"))
                _isEdgesOn = PlayerPrefs.GetInt("EdgesOn") == 1 ? true : false;
            else
                _isEdgesOn = true;
        }
    }
}

