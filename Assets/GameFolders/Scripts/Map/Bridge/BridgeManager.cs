using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeV.Utilities;
using SnakeV.Core.Managers;
using SnakeV.Abstracts;

namespace SnakeV.Core.Bridge
{
    public class BridgeManager : MonoBehaviour
    {
        private List<Bridge> _bridgesList = new List<Bridge>();
        [SerializeField] Bridge _bridgePrefab; //addressables and pool!
        public int bridgeAmount;
        BridgeDirection _bridgeEnteranceDirection;
        private TailController _tailController;
        private List<Vector3> bridgePosToSpawn = new List<Vector3>();
        private Vector3 _bridgeStartPoint;
        

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                ConstructBridges();
        }

        public void ConstructBridges()
        {
            AssignTailController();
            SetBridgeAmount();

            if (!CheckSuitablePosAndDirection())
                return;

            SpawnBridges();
        }

        private void SpawnBridges()
        {
            for (int i = 0; i < bridgePosToSpawn.Count; i++)
            {
                Bridge instantiatedBridge = Instantiate(_bridgePrefab, bridgePosToSpawn[i], Quaternion.identity, transform);
                _bridgesList.Add(instantiatedBridge);
                instantiatedBridge.SetXandZPos(bridgePosToSpawn[i]);
                instantiatedBridge.SetBridgeDirection(_bridgeEnteranceDirection);
                //bridge setdirection
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
            bridgeAmount = Random.Range(2, Mathf.Min(5, FloorManager.Instance.Width));
        }


        public bool CheckSuitablePosAndDirection()
        {
            int iterations = 0;
            Vector3 posToSpawn = DetermineSpawnPos.GetEmptySpawnPos(_tailController, FloorManager.Instance);
            while(!FloorManager.Instance.IsAwayFromBounds(posToSpawn) && iterations<100)
            {
                posToSpawn = DetermineSpawnPos.GetEmptySpawnPos(_tailController, FloorManager.Instance);
                iterations++;
            }

            if (!FloorManager.Instance.IsAwayFromBounds(posToSpawn))
                return false;

            bridgePosToSpawn = BridgesPositions(bridgeAmount, Random.Range(0, 4), posToSpawn);
            return bridgePosToSpawn.Count > 1;
        }

        //TODO: THIS METHOD REPEATS ITSELF
        private List<Vector3> BridgesPositions(int totalAmount, int directionToCheck, Vector3 initialPos)
        {
            List<Vector3> pointsToCheck = new List<Vector3>();

            switch(directionToCheck)
            {
                case 0: //up
                    for(int i=0; i<totalAmount; i++)
                    {
                        Vector3 posToAdd = new Vector3(initialPos.x, initialPos.y, initialPos.z + i);
                        if (DetermineSpawnPos.IsPosValid(_tailController, posToAdd))
                            pointsToCheck.Add(posToAdd);
                        else if(pointsToCheck.Count<2)
                        {
                            pointsToCheck.Clear();
                        }
                    }
                    break;
                case 1: //right
                    for (int i = 0; i < totalAmount; i++)
                    {
                        Vector3 posToAdd = new Vector3(initialPos.x+i, initialPos.y, initialPos.z);
                        if (DetermineSpawnPos.IsPosValid(_tailController, posToAdd))
                            pointsToCheck.Add(posToAdd);
                        else if (pointsToCheck.Count < 2)
                        {
                            pointsToCheck.Clear();
                        }
                    }
                    break;
                case 2: //down
                    for (int i = 0; i < totalAmount; i++)
                    {
                        Vector3 posToAdd = new Vector3(initialPos.x, initialPos.y, initialPos.z-i);
                        if (DetermineSpawnPos.IsPosValid(_tailController, posToAdd))
                            pointsToCheck.Add(posToAdd);
                        else if (pointsToCheck.Count < 2)
                        {
                            pointsToCheck.Clear();
                        }
                    }
                    break;
                default: //left
                    for (int i = 0; i < totalAmount; i++)
                    {
                        Vector3 posToAdd = new Vector3(initialPos.x-i, initialPos.y, initialPos.z);
                        if (DetermineSpawnPos.IsPosValid(_tailController, posToAdd))
                            pointsToCheck.Add(posToAdd);
                        else if (pointsToCheck.Count < 2)
                        {
                            pointsToCheck.Clear();
                        }
                    }
                    break; 
            }

            if (pointsToCheck.Count == 0)
                return pointsToCheck;

            else if (pointsToCheck[pointsToCheck.Count - 1].x == 0 || pointsToCheck[pointsToCheck.Count - 1].x == FloorManager.Instance.Width - 1
                || pointsToCheck[pointsToCheck.Count - 1].z == 0 || pointsToCheck[pointsToCheck.Count - 1].z == FloorManager.Instance.Height - 1)
                pointsToCheck.RemoveAt(pointsToCheck.Count - 1);

            if (pointsToCheck.Count > 1)
                SetBridgeDirection(directionToCheck);

            return pointsToCheck;

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

       

    }

}


