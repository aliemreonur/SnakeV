using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeV.Inputs;
using SnakeV.Utilities;
using SnakeV.Abstracts;

namespace SnakeV.Core
{
    [RequireComponent(typeof(TailController))]
    public class PlayerController : Singleton<PlayerController>, IControllable, IFollower
    {
        //public static Action OnPlayerDeath;

        [Range(0.1f, 0.95f)]
        [SerializeField] private float _speed;

        private Vector3 _currentDirection;
        VectorConverter _vectorConverter;
        WaitForSeconds _movementDelayTime;

        public Vector3 Direction => _currentDirection;
        public bool IsAlive { get; private set; }
        public Vector3 PreviousPos { get; private set; }

        private TailController _tailController;


        private void Awake()
        {
            SingletonThisObj(this);
            IsAlive = true;
        }

        void Start()
        {
            _vectorConverter = new VectorConverter(this);
            _movementDelayTime = new WaitForSeconds(1-_speed);
            _tailController = GetComponent<TailController>();
            if (_tailController == null)
                Debug.LogError("Head could not gather the tail controller");
            _tailController.tailsList.Add(this);
            StartCoroutine(SnakeMoveRoutine());
        }

        void Update()
        {
            _vectorConverter.NormalUpdate();
            _currentDirection = _vectorConverter.MoveDirection; //this is buggy!
        }

        IEnumerator SnakeMoveRoutine()
        {
            while(IsAlive)
            {
                SetNewPos(_currentDirection);
                _tailController.MoveSnake();
                yield return _movementDelayTime;
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
    }

}

