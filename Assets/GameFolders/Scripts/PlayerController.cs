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
        [Range(0, 1)]
        [SerializeField] private float _speed;

        private Vector3 _currentDirection;
        VectorConverter _vectorConverter;

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
            StartCoroutine(SnakeMoveRoutine());
        }

        void Update()
        {

            _currentDirection = _vectorConverter.MoveDirection;
            _vectorConverter.NormalUpdate();
        }

        IEnumerator SnakeMoveRoutine() //will add delay time raher than creating a new instance everytime.
        {
            while(IsAlive)
            {
                yield return new WaitForSeconds(1f);
                transform.position += _currentDirection;
            }
            
        }
    }

}

