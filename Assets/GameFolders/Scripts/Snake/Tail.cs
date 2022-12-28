using UnityEngine;
using System;
using SnakeV.Abstracts;

namespace SnakeV.Core
{
    public class Tail : MonoBehaviour, IFollower
    {
        public Vector3 PreviousPos => _previousPos;
        [SerializeField] private Vector3 _previousPos;
        public int XPos { get; private set; }
        public int ZPos { get; private set; }

        public void SetNewPos(Vector3 posToSet)
        {
            transform.position = posToSet;
            XPos = (int)posToSet.x;
            ZPos = (int) posToSet.z;
        }

        public void SetPreviousPos()
        {
            _previousPos = transform.position;
        }
    }

}

