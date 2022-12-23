using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using SnakeV.Core;
using SnakeV.Core.Managers;

namespace SnakeV.Abstracts
{
    public class TileLoader
    {
        private Tile _tilePrefab;
        private GameObject _edgePrefab;
        private AsyncOperationHandle<GameObject> _tileHandler;
        private AsyncOperationHandle<GameObject> _edgeHandler;
        private FloorManager _floorManager;
        private bool _isLoaded;
        private Material _lavaMaterial;

        public TileLoader(FloorManager floorManager)
        {
            _floorManager = floorManager;
            _edgeHandler = Addressables.LoadAssetAsync<GameObject>("Prefabs/Edge");
            _tileHandler = Addressables.LoadAssetAsync<GameObject>("Prefabs/Tile");
            _edgeHandler.Completed += OnEdgeLoaded;
            _tileHandler.Completed += OnTileLoad;
        }

        private void OnEdgeLoaded(AsyncOperationHandle<GameObject> obj)
        {
            _edgePrefab = obj.Result;
            _floorManager.SpawnEdges(_edgePrefab);
        }

        public void NormalUpdate()
        {
            if (!_isLoaded)
                return;
            _lavaMaterial.mainTextureOffset += Time.deltaTime * 0.5f * Vector2.right;
        }

        private void OnTileLoad(AsyncOperationHandle<GameObject> obj)
        {
            _tilePrefab = obj.Result.GetComponent<Tile>();
            _floorManager.CreateFloor(_tilePrefab);
            _lavaMaterial = _tilePrefab.transform.GetChild(1).GetComponent<MeshRenderer>().sharedMaterial;
            _isLoaded = true;
        }

        public void ReleaseMemory()
        {
            Addressables.Release(_tileHandler);
            Addressables.Release(_edgeHandler);
        }

    }
}

