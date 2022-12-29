using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeV.Core.Managers;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using SnakeV.Utilities;


namespace SnakeV.Core
{
    //TODO: 
    public class BombSpawner : MonoBehaviour 
    {
        public uint NumberOfBombs => _numberOfBombs;
        private uint _numberOfBombs;

        private Vector3 _spawnPos;

        private GameObject _bombPrefab;
        private AsyncOperationHandle<GameObject> _bombHandle;
        private List<Bomb> _allBombs = new List<Bomb>();
        private Bomb bomb;

        private void Start()
        {
            AssignBombAmount(1); //this will be determined according to level hardness.
            _bombHandle = Addressables.LoadAssetAsync<GameObject>("Collectables/Bomb");
            _bombHandle.Completed += OnBombAssetLoaded;
            _spawnPos = new Vector3(0, -15, 0);
        }


        private void OnBombAssetLoaded(AsyncOperationHandle<GameObject> obj)
        {
            _bombPrefab = obj.Result;
            for(int i=0; i<_numberOfBombs; i++)
            {
                GameObject instantiatedObj = Instantiate(_bombPrefab, _spawnPos, Quaternion.identity);
                bomb = instantiatedObj.GetComponent<Bomb>();
                if (bomb != null)
                {
                    _allBombs.Add(bomb);
                }
                else
                    Debug.Log("Could not gather the bomb script!");
            }
        }

        private void AssignBombAmount(uint bombAmount)
        {
            _numberOfBombs = bombAmount; //this will be gathered regarding to hardness of the level.
        }
        
        private void OnDisable()
        {
            Addressables.Release(_bombHandle);
        }
    }

}


