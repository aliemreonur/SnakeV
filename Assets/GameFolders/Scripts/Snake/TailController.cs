using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeV.Abstracts;
using SnakeV.Utilities;
using System;

namespace SnakeV.Core
{
    public class TailController
    {
        [SerializeField] public List<IFollower> tailsList;

        public Vector3 PreviousPos { get; private set; }

        public IFollower _collectedTail;
        public IControllable _iControllable;

        public static Action<Tail> SnakeGrow;

        public TailController()
        {
            tailsList = new List<IFollower>();
        }

        public void AddTail(IFollower tailToAdd)
        {
            tailsList.Add(tailToAdd);
            
        }

        public void MoveSnake()
        {
            for (int i = tailsList.Count - 1; i > 0; i--)
            {
                tailsList[i].SetPreviousPos();
            }

            for (int i = tailsList.Count-1; i>0; i--)
            {
                tailsList[i].SetNewPos(tailsList[i-1].PreviousPos);
            }
        }

    }
}

