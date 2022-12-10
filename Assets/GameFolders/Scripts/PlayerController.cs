using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeV.Inputs;
using SnakeV.Utilities;
using SnakeV.Abstracts;

namespace SnakeV.Core
{
    public class PlayerController : Singleton<PlayerController>, IControllable
    {
        [Range(0.1f, 0.95f)]
        [SerializeField] private float _speed;

        private Vector3 _currentDirection;
        VectorConverter _vectorConverter;
        WaitForSeconds _movementDelayTime;

        public Vector3 Direction => _currentDirection;
        public bool IsAlive { get; private set; }


        private void Awake()
        {
            SingletonThisObj(this);
            IsAlive = true;
        }

        void Start()
        {
            _vectorConverter = new VectorConverter(this);
            _movementDelayTime = new WaitForSeconds(1-_speed);
            StartCoroutine(SnakeMoveRoutine());
        }

        void Update()
        {
            _currentDirection = _vectorConverter.MoveDirection;
            _vectorConverter.NormalUpdate();
        }

        IEnumerator SnakeMoveRoutine()
        {
            while(IsAlive)
            {
                yield return _movementDelayTime;
                transform.position += _currentDirection;
            }
            
        }
    }

}

