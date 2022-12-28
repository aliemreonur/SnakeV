using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SnakeV.Abstracts;

namespace SnakeV.Core.Bridge
{
    [RequireComponent(typeof(Bridge))]
    public class BridgeDirectionUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _directionText;
        private Bridge _bridge;

        private void Awake()
        {
            _bridge = GetComponent<Bridge>();
        }


        private void SetDirectionText()
        {
            if (_bridge.BridgeDirection == BridgeDirection.Up)
                _directionText.text = "↑";
            else if (_bridge.BridgeDirection == BridgeDirection.Down)
                _directionText.text = "↓";
            else if (_bridge.BridgeDirection == BridgeDirection.Left)
                _directionText.text = "←";
            else
            {
                _directionText.text = "→";
            }
            
        }

        private void OnEnable()
        {
            _bridge.OnDirectionSet += SetDirectionText;
        }

        private void OnDisable()
        {
            _bridge.OnDirectionSet -= SetDirectionText;
        }
    }

}


