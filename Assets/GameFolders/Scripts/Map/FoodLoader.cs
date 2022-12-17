using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using SnakeV.Core;

namespace SnakeV.Abstracts
{
    [RequireComponent(typeof(Food))]
    public class FoodLoader : MonoBehaviour
    {
        ICollectable _iCollectable;

        void Start()
        {
            StartCoroutine(LoadAllAssetsByKey());
            
            _iCollectable = GetComponent<ICollectable>();
            if (_iCollectable == null)
                Debug.LogError("Could not get the icollectable!");
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

                _iCollectable.RandomCollectable();

                //TODO: this is not really fine! -repeats on player script also!
                _iCollectable.MoveToNewPos(PlayerController.Instance.foodSpawner.CheckPossibleSpawnPos(PlayerController.Instance.tailController));
            }
        }

    }

}