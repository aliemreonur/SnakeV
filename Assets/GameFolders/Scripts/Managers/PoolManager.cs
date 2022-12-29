using SnakeV.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeV.Abstracts;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using SnakeV.Core.Bridges;

namespace SnakeV.Core.Managers
{
    //TODO: Repeating same methods for both bridge and tail.
    public class PoolManager : Singleton<PoolManager>
    {
        [SerializeField] private Tail _tailUnit;
        [SerializeField] private Transform _tailHolder;
        private List<Tail> _tailsList = new List<Tail>();
        private List<Bridge> _bridgesList = new List<Bridge>();
        private int _spawnAmount = 100; //will be changed later.
        private int _currentSpawns;

        private Bridge _bridgePrefab;
        private AsyncOperationHandle<GameObject> _bridgeHandle;

        private void Start()
        {
            LoadBridges();
            SpawnIinitialPool(_spawnAmount);
            _currentSpawns = _spawnAmount;
        }

        #region Tails
        public Tail RequestTail(Vector3 position)
        {
            Tail tailToReturn;
            for(int i=0; i<_tailsList.Count; i++)
            {
                if(!_tailsList[i].isActiveAndEnabled)
                {
                    tailToReturn = _tailsList[i];
                    tailToReturn.gameObject.SetActive(true);
                    tailToReturn.transform.position = position;
                    return tailToReturn;
                }
            }

            AddNewTail(position);
            return _tailsList[_tailsList.Count - 1];
        }

        private void SpawnIinitialPool(int spawnAmount)
        {
            for(int i=1; i<=spawnAmount; i++)
            {
                Tail _spawnedPiece = Instantiate(_tailUnit, transform.position, Quaternion.identity, _tailHolder.transform);
                _spawnedPiece.name = "Tail : " + i;
                _spawnedPiece.gameObject.SetActive(false);
                _tailsList.Add(_spawnedPiece);
            }
        }

        private void AddNewTail(Vector3 spawnPos)
        {
            _currentSpawns++;
            Tail newTail = Instantiate(_tailUnit, transform.position, Quaternion.identity, _tailHolder.transform);
            newTail.name = "Tail : " + _currentSpawns;
            _tailsList.Add(newTail);
            newTail.transform.position = spawnPos;   
        }
        #endregion

        #region Bridges

        public Bridge RequestBridge(Vector3 newPos)
        {
            Bridge bridgeToReturn;
            for(int i=0; i<_bridgesList.Count; i++)
            {
                if(!_bridgesList[0].isActiveAndEnabled)
                {
                    bridgeToReturn = _bridgesList[i];
                    bridgeToReturn.gameObject.SetActive(true);
                    bridgeToReturn.transform.position = newPos;
                    return bridgeToReturn;
                }
            }
            AddNewBridge(newPos);
            return _bridgesList[_bridgesList.Count-1];
        }

        private void AddNewBridge(Vector3 posToSpawn)
        {
            Bridge newBridge = Instantiate(_bridgePrefab, posToSpawn, Quaternion.identity, transform);
            newBridge.transform.position = posToSpawn;
            _bridgesList.Add(newBridge);
 
        }

        private void OnBridgeLoaded(AsyncOperationHandle<GameObject> obj)
        {
            _bridgePrefab = obj.Result.GetComponent<Bridge>();
            if (_bridgePrefab == null)
                Debug.Log("Could not load the bridge prefab!");
        }

        private void LoadBridges()
        {
            _bridgeHandle = Addressables.LoadAssetAsync<GameObject>("Prefabs/Bridge");
            _bridgeHandle.Completed += OnBridgeLoaded;
        }

        private void SpawnBridgePool()
        {
            for(int i=0; i<10; i++)
            {
                Bridge _instantiatedPoolBridge = Instantiate(_bridgePrefab, new Vector3(0, -50, 0), Quaternion.identity, transform);
                _bridgesList.Add(_instantiatedPoolBridge);
                _instantiatedPoolBridge.gameObject.SetActive(false);
            }
        }
        #endregion

        private void OnDisable()
        {
            Addressables.Release(_bridgeHandle);
        }

    }
}

