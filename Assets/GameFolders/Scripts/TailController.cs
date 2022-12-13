using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeV.Abstracts;
using SnakeV.Utilities;

namespace SnakeV.Core
{
    public class TailController : Singleton<TailController>
    {
        [SerializeField] public List<IFollower> tailsList;

        public Vector3 PreviousPos { get; private set; }

        public IFollower _collectedTail;
        public IControllable _iControllable;

        private void Awake()
        {
            SingletonThisObj(this);
        }

        private void Start()
        {
            tailsList = new List<IFollower>();
            _iControllable = GetComponent<IControllable>();
            if (_iControllable == null)
                Debug.Log("The tail manager could not get the player controller");
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

