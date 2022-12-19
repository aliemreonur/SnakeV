using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using SnakeV.Abstracts;

namespace SnakeV.Inputs
{
    public class InputGatherer:IInputReader
    {
        public float Horizontal { get; set; }
        public float Vertical { get; set; }

        //private float _horizontal;
        //private float _vertical;

        private PlayerInput _playerInput;

        public InputGatherer()
        {
            _playerInput = new PlayerInput();
            _playerInput.Snake.HorizontalMovement.performed += OnHorizontalMovementGathered;
            _playerInput.Snake.VerticalMovement.performed += OnVerticalMovementGathered;
            _playerInput.Enable();
        }

        private void OnVerticalMovementGathered(InputAction.CallbackContext ctx)
        {
            Vertical = ctx.ReadValue<float>();
        }

        private void OnHorizontalMovementGathered(InputAction.CallbackContext ctx)
        {
            Horizontal = ctx.ReadValue<float>();
        }
    }
}

