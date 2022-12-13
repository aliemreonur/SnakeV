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
        public int Rank { get; set; }
        public int rank;

        private void Start()
        {
            Collected = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (Collected)
                return;
               

            if(other.CompareTag("Player"))
            {
                Collected = true;
                transform.position = TailController.Instance.tailsList[TailController.Instance.tailsList.Count - 1].PreviousPos;
                TailController.Instance.AddTail(this);
                SpawnManager.Instance.Spawn();
                rank = Rank;
            }
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

