using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeV.Abstracts;
using SnakeV.Utilities;
using DG.Tweening;

namespace SnakeV.Core
{
    public class TailController
    {
        [SerializeField] public List<IFollower> tailsList;

        public Vector3 PreviousPos { get; private set; }

        public IFollower _collectedTail;
        public IControllable _iControllable;

        public TailController()
        {
            tailsList = new List<IFollower>();
        }

        public void AddTail(IFollower tailToAdd)
        {
            tailsList.Add(tailToAdd);
            SnakeAte();
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

        public void SnakeAte()
        {
            for (int j = 0; j < tailsList.Count; j++)
            {
                //TODO: ADD A DELAY - from the player controller?
                tailsList[j].transform.DOShakeScale(0.1f, 0.2f, 1, 0, true, ShakeRandomnessMode.Harmonic);
            }
        }
    }
}

