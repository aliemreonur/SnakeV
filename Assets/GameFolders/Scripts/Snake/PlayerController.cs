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

        public uint Score { get; private set; }
        public Vector3 Direction => _currentDirection;
        public Vector3 PreviousPos { get; private set; }
        public IInputConverter InputConverter { get; set; } // => _vectorConverter;

        public TailController tailController => _tailController;

        private Vector3 _currentDirection;
        private WaitForSeconds _movementDelayTime;
        private TailController _tailController;
        private FloorManager _floorManager;
        private int _winScore = 10;
        private bool _edgesOn;

        void Start()
        {
            _edgesOn = GameManager.Instance.IsEdgesOn;
            InputConverter = new VectorConverter(this);
            _movementDelayTime = new WaitForSeconds(1-(GameManager.Instance.GameSpeed/100));
            _tailController = new TailController();
            _tailController.tailsList.Add(this);

            if (!GameManager.Instance.IsEdgesOn)
                _floorManager = FloorManager.Instance;
        }

        void Update()
        {
            InputConverter.NormalUpdate();
        }

        public void SetStartPos(Vector3 pos)
        {
            transform.position = pos;
        }

        public void SetWinScore(int value)
        {
            _winScore = value;
        }

        public void StartMoving()
        {
            IsAlive = true;
            StartCoroutine(SnakeMoveRoutine());
        }

        public void SetNewPos(Vector3 newPos)
        {
            transform.position += newPos;
            if (!_edgesOn)
                PositionIfNoEdges();
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
            if (Score >= _winScore)
            {
                GameManager.Instance.GameWon();
                IsAlive = false;
            }
           
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

        private void PositionIfNoEdges()
        {
            if (transform.position.x < 0)
                transform.position = new Vector3(_floorManager.Width - 1, transform.position.y, transform.position.z);
            else if(transform.position.x > _floorManager.Width-1)
                transform.position = new Vector3(0, transform.position.y, transform.position.z);
            else if(transform.position.z < 0)
                transform.position = new Vector3(transform.position.x, transform.position.y, _floorManager.Height-1);
            else if(transform.position.z > _floorManager.Height-1)
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }

}

