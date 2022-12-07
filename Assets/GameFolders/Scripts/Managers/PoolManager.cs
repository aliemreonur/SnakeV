using SnakeV.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeV.Abstracts;

namespace SnakeV.Utilities
{
    public class PoolManager : Singleton<PoolManager>
    {
        [SerializeField] private Tail _tailUnit;
        private List<Tail> _tailsList = new List<Tail>();
        private int _spawnAmount = 100; //will be changed later.  

        private void Awake()
        {
            SingletonThisObj(this);
        }

        private void Start()
        {
            SpawnIinitialPool(_spawnAmount);
        }

        public Tail RequestTail(Vector3 position)
        {
            Tail tailToReturn;
            for(int i=0; i<_tailsList.Count; i++)
            {
                if(_tailsList[i].enabled == false)
                {
                    tailToReturn = _tailsList[i];
                    return tailToReturn;
                }
            }

            AddNewTail(position);
            return _tailsList[_tailsList.Count - 1];

        }

        private void SpawnIinitialPool(int spawnAmount)
        {
            for(int i=0; i<spawnAmount; i++)
            {
                Tail _spawnedPiece = Instantiate(_tailUnit, transform.position, Quaternion.identity, transform);
                _spawnedPiece.enabled = false;
                _tailsList.Add(_spawnedPiece);
            }
        }

        private void AddNewTail(Vector3 spawnPos)
        {
            Tail newTail = Instantiate(_tailUnit, transform.position, Quaternion.identity, transform);
            _tailsList.Add(newTail);
            newTail.transform.position = spawnPos;
        }



    }
}

