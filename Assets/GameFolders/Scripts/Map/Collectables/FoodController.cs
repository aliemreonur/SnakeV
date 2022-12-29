using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using SnakeV.Utilities;
using SnakeV.Core.Managers;
using SnakeV.Abstracts;

namespace SnakeV.Core.Collectables
{
    public class FoodController : MonoBehaviour
    {
        ICollectable _iCollectable;
        FloorManager _floorManager;
        PlayerCollectableDetector _playerCollectableDetector;
        PlayerController _playerController;

        private void Awake()
        {
            _playerController = PlayerController.Instance;
            _floorManager = FloorManager.Instance;
            _iCollectable = new Collectable(_floorManager, this);
            _playerCollectableDetector = new PlayerCollectableDetector(_playerController, _iCollectable);
        }

        void Start()
        {

            StartCoroutine(LoadAllAssetsByKey());
        }

        IEnumerator LoadAllAssetsByKey()
        {

            AsyncOperationHandle<IList<GameObject>> loadWithSingleKeyHandle = Addressables.LoadAssetsAsync<GameObject>("Food", obj =>{});
            yield return loadWithSingleKeyHandle;
            IList<GameObject> loadedObjects = loadWithSingleKeyHandle.Result;

            if(loadWithSingleKeyHandle.Status == AsyncOperationStatus.Succeeded)
            {
                foreach(GameObject obj in loadedObjects)
                {
                    GameObject instantiatedObj = Instantiate(obj, transform.position, Quaternion.identity, transform);
                    _iCollectable.AddToList(instantiatedObj);
                }

                _iCollectable.RandomizeCollectable();
                _iCollectable.MoveToNewPos(DetermineSpawnPos.GetEmptySpawnPos(_playerController.tailController, FloorManager.Instance));
            }

            Addressables.Release(loadWithSingleKeyHandle);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                _playerCollectableDetector.OnPlayerTrigger();
            }
        }

    }

}