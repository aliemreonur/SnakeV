using System.Collections;
using UnityEngine;
using SnakeV.Utilities;
using SnakeV.Abstracts;
using System;
using SnakeV.Core.Managers;

namespace SnakeV.Core
{
    public class PlayerController : Singleton<PlayerController>, IControllable, IFollower
    {
        public Action OnPlayerDeath;

        [Range(0.1f, 0.95f)]
        [SerializeField] private float _speed;

        private Vector3 _currentDirection;
        VectorConverter _vectorConverter;
        WaitForSeconds _movementDelayTime;

        public Vector3 Direction => _currentDirection;
        public bool IsAlive { get; private set; }
        public Vector3 PreviousPos { get; private set; }
        public Vector2Int CurrentPos { get; private set; }

        private TailController _tailController;
        private SpawnManager _spawnManager;

        private void Awake()
        {
            SingletonThisObj(this);
            IsAlive = true;
        }

        void Start()
        {
            _vectorConverter = new VectorConverter(this);
            _movementDelayTime = new WaitForSeconds(1-_speed);
            _tailController = new TailController();
            _spawnManager = new SpawnManager();

            _tailController.tailsList.Add(this);
            StartCoroutine(SnakeMoveRoutine());
        }

        void Update()
        {
            _vectorConverter.NormalUpdate();
        }

        IEnumerator SnakeMoveRoutine()
        {
            while(IsAlive)
            {
                SetNewPos(_currentDirection);
                _tailController.MoveSnake();
                CurrentPos = new Vector2Int((int)transform.position.x, (int)transform.position.z);
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

        public void Death()
        {
            Debug.Log("DEAD!");
            OnPlayerDeath?.Invoke();
        }

        public void Grow(Tail tailToAdd)
        {
            tailToAdd.transform.position = _tailController.tailsList[_tailController.tailsList.Count - 1].PreviousPos;
            tailToAdd.SetCurrentPos();
            _tailController.AddTail(tailToAdd);
            _spawnManager.SpawnNewFood(_tailController);
        }

        private void SetDirection()
        {
            _currentDirection = _vectorConverter.MoveDirection;
        }
    }

}

