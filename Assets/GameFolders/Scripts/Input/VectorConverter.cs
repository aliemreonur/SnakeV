using UnityEngine;
using SnakeV.Abstracts;

namespace SnakeV.Inputs
{
    public class VectorConverter : IInputConverter
    {
        //private Vector3 _moveDirection;
        private IControllable _player;

        public Vector3 MoveDirection { get; set; }
        private float _horizontal, _vertical;

        public IInputReader InputReader { get; set; }

        public VectorConverter(IControllable controllable)
        {
            InputReader = new InputGatherer();
            _player = controllable;
            MoveDirection = Vector3.forward;
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
                MoveDirection = Vector3.right;

            else if (_horizontal == -1 && _player.Direction != Vector3.right)
                MoveDirection = Vector3.left;
   
            else if (_vertical == 1 && _player.Direction != Vector3.back)
                MoveDirection = Vector3.forward;

            else if (_vertical == -1 && _player.Direction != Vector3.forward)
                MoveDirection = Vector3.back;
            
        }

        public void Test()
        {
            if (_horizontal == 1)
                MoveDirection = Vector3.right;
        }

    }
}

