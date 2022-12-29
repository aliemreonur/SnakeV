using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeV.Utilities;
using SnakeV.Core.Managers;
using SnakeV.Abstracts;

namespace SnakeV.Core.Collectables
{
    public class PlayerCollectableDetector
    {
        private ICollectable _collectable;
        private PlayerController _playerController;

        public PlayerCollectableDetector(PlayerController playerController, ICollectable icollectable)
        {
            _collectable = icollectable;
            _playerController = playerController;
        }

        public void OnPlayerTrigger()
        {
            _collectable.MoveToNewPos(DetermineSpawnPos.GetEmptySpawnPos(_playerController.tailController, FloorManager.Instance));
            _playerController.Ate();
        }
    }
}


