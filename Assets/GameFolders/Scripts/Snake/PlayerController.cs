using System;
using System.Collections;
using SnakeV.Abstracts;
using SnakeV.Utilities;
using UnityEngine;

namespace SnakeV.Core
{
    [RequireComponent(typeof(Player))]
    public class PlayerController : Singleton<PlayerController>, IControllable, IFollower
    {
        public Action OnPlayerDeath;
        public bool IsAlive { get; private set; }

        public Vector3 Direction => _currentDirection;
        public Vector3 PreviousPos { get; private set; }
        public IInputConverter InputConverter { get; set; } // => _vectorConverter;

        public TailController tailController => _tailController;
        public FoodSpawner foodSpawner => _foodSpawner;

        [Range(0.1f, 0.95f)]
        [SerializeField] private float _speed;

        private Vector3 _currentDirection;
        //private IInputConverter _vectorConverter;
        private WaitForSeconds _movementDelayTime;

        private TailController _tailController;
        private FoodSpawner _foodSpawner;

        void Start()
        {
            InputConverter = new VectorConverter(this);
            _movementDelayTime = new WaitForSeconds(1-_speed);
            _tailController = new TailController();
            _foodSpawner = new FoodSpawner();
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
            _foodSpawner.SpawnNewFood(_tailController);
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
    }

}

