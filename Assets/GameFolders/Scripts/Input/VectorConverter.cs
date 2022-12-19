using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeV.Abstracts;
using SnakeV.Inputs;

namespace SnakeV.Utilities
{
    public class VectorConverter : IInputConverter
    {
        private Vector3 _moveDirection;
        private IControllable _player;

        public Vector3 MoveDirection { get; set; }
        private float _horizontal, _vertical;

        public IInputReader InputReader { get; set; }

        public VectorConverter(IControllable controllable)
        {
            InputReader = new InputGatherer();
            _player = controllable;
            _moveDirection = Vector3.forward;
        }

        public void NormalUpdate()
        {
            _horizontal = InputReader.Horizontal;
            _vertical = InputReader.Vertical;
            SetDirection();
        }

        public void SetDirection()
        {
            if (_horizontal == 0 && _vertical == 0)
                return;

            if (_horizontal == 1 && _player.Direction != Vector3.left)
                _moveDirection = Vector3.right;

            else if (_horizontal == -1 && _player.Direction != Vector3.right)
                _moveDirection = Vector3.left;
   
            else if (_vertical == 1 && _player.Direction != Vector3.back)
                _moveDirection = Vector3.forward;

            else if (_vertical == -1 && _player.Direction != Vector3.forward)
                _moveDirection = Vector3.back;
            
        }

        public void Test()
        {
            if (_horizontal == 1)
                MoveDirection = Vector3.right;
        }

    }
}

