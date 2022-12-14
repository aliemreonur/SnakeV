using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeV.Abstracts;
using SnakeV.Core.Managers;
using SnakeV.Utilities;

namespace SnakeV.Core
{
    public class Tail : MonoBehaviour, IFollower
    {
        public Vector3 PreviousPos => _previousPos;
        private Vector3 _previousPos;
        public bool Collected { get; private set; }
        public Vector2Int CurrentPos { get; private set; }

        private void Start()
        {
            Collected = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                if (Collected)
                    PlayerController.Instance.Death();

                Collected = true;
                PlayerController.Instance.Grow(this);
                //transform.position = TailController.Instance.tailsList[TailController.Instance.tailsList.Count - 1].PreviousPos;
                
                //SpawnManager.Instance.Spawn();
            }
        }

        public void SetCurrentPos()
        {
            CurrentPos = new Vector2Int((int)transform.position.x, (int)transform.position.z);
        }

        public void SetNewPos(Vector3 posToSet)
        {
            transform.position = posToSet;
        }

        public void SetPreviousPos()
        {
            _previousPos = transform.position;
        }


    }

}

