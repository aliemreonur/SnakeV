using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeV.Utilities;
using SnakeV.Core.Managers;

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
                _collectable.MoveToNewPos(DetermineSpawnPos.GetEmptySpawnPos(_playerContoller.tailController, FloorManager.Instance));
                _playerContoller.Grow();
                _playerContoller.UpdateScore();
            }
        }
    }
}


