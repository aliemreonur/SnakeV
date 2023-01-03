using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeV.Utilities;
using SnakeV.Core.Managers;
using SnakeV.Abstracts;
using SnakeV.Core.Abstracts;

namespace SnakeV.Core.Bridges
{
    public class BridgeManager : Singleton<BridgeManager>
    {
        private int _bridgeAmount;
        private BridgeDirection _bridgeEnteranceDirection;
        private TailController _tailController;
        private List<Bridge> _bridgesList = new List<Bridge>();
        private List<Vector3> bridgePosToSpawn = new List<Vector3>();
        private WaitForSeconds _bridgeSpawnTime;
        FloorManager _floorManager;

        public void ConstructBridges()
        {
            SetBridgeAmount();

            if (!CheckSuitablePosAndDirection())
                return;
            SpawnBridges();
        }

        private void Start()
        {
            _bridgeSpawnTime = new WaitForSeconds(GameManager.Instance.BridgeSpawnTime);
            _floorManager = FloorManager.Instance;
            AssignTailController();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                ConstructBridges();
            }
        }

        private void SpawnBridges()
        {
            for (int i = 0; i < bridgePosToSpawn.Count; i++)
            {
                Bridge spawnedBridge = PoolManager.Instance.RequestBridge(bridgePosToSpawn[i]);
                _bridgesList.Add(spawnedBridge);
                spawnedBridge.SetXandZPos(bridgePosToSpawn[i]);
                spawnedBridge.SetBridgeDirection(_bridgeEnteranceDirection);
            }
            _bridgesList.Clear();
        }

        private void AssignTailController()
        {
            if (_tailController == null)
                _tailController = PlayerController.Instance.tailController;
        }

        private void SetBridgeAmount()
        {
            _bridgeAmount = Random.Range(2, Mathf.Min(5, _floorManager.Width));
        }

        private bool CheckSuitablePosAndDirection()
        {
            int iterations = 0;
            Vector3 posToSpawn = DetermineSpawnPos.GetEmptySpawnPos(_tailController, _floorManager);
            while(!FloorManager.Instance.IsAwayFromBounds(posToSpawn) && iterations<100)
            {
                posToSpawn = DetermineSpawnPos.GetEmptySpawnPos(_tailController, _floorManager);
                iterations++;
            }

            if (!_floorManager.IsAwayFromBounds(posToSpawn))
                return false;

            bridgePosToSpawn = BridgesPositions(_bridgeAmount, Random.Range(0, 4), posToSpawn);
            if (bridgePosToSpawn.Count < 2)
                bridgePosToSpawn.Clear();

            return bridgePosToSpawn.Count > 1;
        }

        private List<Vector3> BridgesPositions(int totalAmount, int directionToCheck, Vector3 initialPos)
        {
            List<Vector3> pointsToCheck = new List<Vector3>();

            switch(directionToCheck)
            {
                case 0: //up
                    for(int i=0; i<totalAmount; i++)
                    {
                        Vector3 posToAdd = new Vector3(initialPos.x, initialPos.y, initialPos.z + i);
                        CheckPointToAddToList(pointsToCheck, posToAdd);
                    }
                    break;
                case 1: //right
                    for (int i = 0; i < totalAmount; i++)
                    {
                        Vector3 posToAdd = new Vector3(initialPos.x+i, initialPos.y, initialPos.z);
                        CheckPointToAddToList(pointsToCheck, posToAdd);
                    }
                    break;
                case 2: //down
                    for (int i = 0; i < totalAmount; i++)
                    {
                        Vector3 posToAdd = new Vector3(initialPos.x, initialPos.y, initialPos.z-i);
                        CheckPointToAddToList(pointsToCheck, posToAdd);
                    }
                    break;
                default: //left
                    for (int i = 0; i < totalAmount; i++)
                    {
                        Vector3 posToAdd = new Vector3(initialPos.x-i, initialPos.y, initialPos.z);
                        CheckPointToAddToList(pointsToCheck, posToAdd);
                    }
                    break; 
            }

            if (pointsToCheck.Count == 0)
                return pointsToCheck;

            if (pointsToCheck[pointsToCheck.Count - 1].x == 0 || pointsToCheck[pointsToCheck.Count - 1].x == _floorManager.Width - 1
                || pointsToCheck[pointsToCheck.Count - 1].z == 0 || pointsToCheck[pointsToCheck.Count - 1].z == _floorManager.Height - 1)
                pointsToCheck.RemoveAt(pointsToCheck.Count - 1);

            if (pointsToCheck.Count > 1)
                SetBridgeDirection(directionToCheck);

            return pointsToCheck;

        }

        private void CheckPointToAddToList(List<Vector3> pointsToCheck, Vector3 posToAdd)
        {
            if (DetermineSpawnPos.IsPosValid(_tailController, posToAdd))
                pointsToCheck.Add(posToAdd);
            else if (pointsToCheck.Count < 2)
            {
                pointsToCheck.Clear();
            }
        }

        private void SetBridgeDirection(int direction)
        {
            switch (direction)
            {
                case 0:
                    _bridgeEnteranceDirection = BridgeDirection.Up;
                    break;
                case 1:
                    _bridgeEnteranceDirection = BridgeDirection.Right;
                    break;
                case 2:
                    _bridgeEnteranceDirection = BridgeDirection.Down;
                    break;
                default:
                    _bridgeEnteranceDirection = BridgeDirection.Left;
                    break;
            }
        }

        private void StartSpawning()
        {
            StartCoroutine(BridgeSpawnRoutine());
        }

        private IEnumerator BridgeSpawnRoutine()
        {
            while(PlayerController.Instance.IsAlive)
            {
                yield return _bridgeSpawnTime;
                ConstructBridges();
            }
        }

        private void OnEnable()
        {
            GameManager.Instance.OnGameStart += StartSpawning;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnGameStart -= StartSpawning;
        }

    }

}


