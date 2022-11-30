using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace SnakeV.Inputs
{
    public class InputGatherer
    {
        public float Horizontal => _horizontal;
        public float Vertical => _vertical;

        private float _horizontal;
        private float _vertical;

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
            _vertical = ctx.ReadValue<float>();
        }

        private void OnHorizontalMovementGathered(InputAction.CallbackContext ctx)
        {
            _horizontal = ctx.ReadValue<float>();
        }
    }
}

