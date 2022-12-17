using System.Collections.Generic;
using UnityEngine;
using SnakeV.Abstracts;
using DG.Tweening;
using SnakeV.Core.Managers;

namespace SnakeV.Core
{
    public class TailController
    {
        [SerializeField] public List<IFollower> tailsList;

        public Vector3 PreviousPos { get; private set; }
        private Vector3 _posToSpawn;

        public IFollower _collectedTail;
        public IControllable _iControllable;

        public TailController()
        {
            tailsList = new List<IFollower>();
        }

        public void AddTail()
        {
            _posToSpawn = tailsList[tailsList.Count - 1].PreviousPos;
            tailsList.Add(PoolManager.Instance.RequestTail(_posToSpawn));
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

