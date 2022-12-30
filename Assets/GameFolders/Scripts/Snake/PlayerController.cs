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
        public int XPos { get; private set; }
        public int ZPos { get; private set; }

        public Vector3 Direction => _currentDirection;
        public Vector3 PreviousPos { get; private set; }
        public IInputConverter InputConverter { get; set; } // => _vectorConverter;

        public TailController tailController => _tailController;

        public uint Score { get; private set; }

        private Vector3 _currentDirection;
        private WaitForSeconds _movementDelayTime;
        private TailController _tailController;

        void Start()
        {
            InputConverter = new VectorConverter(this);
            _movementDelayTime = new WaitForSeconds(1-GameManager.Instance.GameSpeed);
            _tailController = new TailController();
            _tailController.tailsList.Add(this);

        }

        void Update()
        {
            InputConverter.NormalUpdate();
        }

        public void StartMoving()
        {
            IsAlive = true;
            StartCoroutine(SnakeMoveRoutine());
        }

        public void SetNewPos(Vector3 newPos)
        {
            transform.position += newPos;
        }

        public void SetPreviousPos()
        {
            PreviousPos = transform.position;
            XPos = (int)transform.position.x;
            ZPos = (int) transform.position.z;
        }

        public void Ate()
        {
            Grow();
            UpdateScore();
        }


        public void Death()
        {
            IsAlive = false;
            OnPlayerDeath?.Invoke();
        }

        public void SetDirection()
        {
            _currentDirection = InputConverter.MoveDirection;
        }

        private void Grow()
        {
            _tailController.AddTail();
        }


        private void UpdateScore()
        {
            Score++;
            UIManager.Instance.UpdateScore(Score);
        }

        private IEnumerator SnakeMoveRoutine()
        {
            while (IsAlive)
            {
                SetNewPos(_currentDirection);
                _tailController.MoveSnake();
                yield return _movementDelayTime;
                SetDirection();
                SetPreviousPos();
            }
        }
    }

}

