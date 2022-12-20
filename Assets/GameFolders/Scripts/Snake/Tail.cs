using UnityEngine;
using System;
using SnakeV.Abstracts;

namespace SnakeV.Core
{
    public class Tail : MonoBehaviour, IFollower
    {
        public Vector3 PreviousPos => _previousPos;
        [SerializeField] private Vector3 _previousPos;

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

