using System;
using System.Collections;
using SnakeV.Abstracts;
using SnakeV.Utilities;
using UnityEngine;
using SnakeV.Core.Managers;

namespace SnakeV.Core
{
    public class PlayerController : Singleton<PlayerController>, IControllable, IFollower
    {
        public Action OnPlayerDeath;
        public bool IsAlive { get; private set; }

        public Vector3 Direction => _currentDirection;
        public Vector3 PreviousPos { get; private set; }
        public IInputConverter InputConverter { get; set; } // => _vectorConverter;

        public TailController tailController => _tailController;

        public uint Score { get; private set; }

        [Range(0.1f, 0.95f)]
        [SerializeField] private float _speed;

        private Vector3 _currentDirection;
        //private IInputConverter _vectorConverter;
        private WaitForSeconds _movementDelayTime;

        private TailController _tailController;

        void Start()
        {
            InputConverter = new VectorConverter(this);
            _movementDelayTime = new WaitForSeconds(1-_speed);
            _tailController = new TailController();
            IsAlive = true;

            _tailController.tailsList.Add(this);
            StartCoroutine(SnakeMoveRoutine());
        }

        void Update()
        {
            InputConverter.NormalUpdate();
        }

        IEnumerator SnakeMoveRoutine()
        {
            while(IsAlive)
            {
                SetNewPos(_currentDirection);
                _tailController.MoveSnake();
                yield return _movementDelayTime;
                SetDirection();
                SetPreviousPos();
            }
        }
        public void SetNewPos(Vector3 newPos)
        {
            transform.position += newPos;
        }

        public void SetPreviousPos()
        {
            PreviousPos = transform.position;
        }

        public void Grow()
        {
            _tailController.AddTail();
        }

        public void SetDirection()
        {
            _currentDirection = InputConverter.MoveDirection;
        }

        public void Death()
        {
            IsAlive = false;
            OnPlayerDeath?.Invoke();
        }

        public void UpdateScore()
        {
            Score++;
            UIManager.Instance.UpdateScore(Score);
        }
    }

}

