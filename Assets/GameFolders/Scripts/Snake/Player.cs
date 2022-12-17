using UnityEngine;
using SnakeV.Abstracts;
using SnakeV.Core.Managers;

namespace SnakeV.Core
{
    public class Player : MonoBehaviour
    {
        ICollectable _icollectable;
        PlayerController _playerController;
        private uint _score;

        private void Start()
        {
            _playerController = GetComponent<PlayerController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Tail"))
            {
                _playerController.Death();
            }
            else if(other.CompareTag("Collectable"))
            {
                if (_icollectable == null)
                    _icollectable = other.GetComponent<ICollectable>();
                //MEH ?!?
                _icollectable.MoveToNewPos(_playerController.foodSpawner.CheckPossibleSpawnPos(_playerController.tailController));
                _playerController.Grow();
                UpdateScore();
            }
        }

        private void UpdateScore()
        {
            _score++;
            UIManager.Instance.UpdateScore(_score);
        }
    }

}

