using UnityEngine;
using SnakeV.Abstracts;

namespace SnakeV.Core
{
    public class Tail : MonoBehaviour, IFollower
    {
        public Vector3 PreviousPos => _previousPos;
        private Vector3 _previousPos;
        public bool Collected { get; private set; }

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

