using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeV.Core
{

    [RequireComponent(typeof(Collectable))]
    public class PlayerCollectableDetector : MonoBehaviour
    {
        Collectable _collectable;
        private void Start()
        {
            _collectable = GetComponent<Collectable>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                PlayerController _playerContoller = PlayerController.Instance;
                _collectable.MoveToNewPos(_playerContoller.foodSpawner.CheckPossibleSpawnPos(_playerContoller.tailController));
                _playerContoller.Grow();
                _playerContoller.UpdateScore();
            }
        }
    }
}


