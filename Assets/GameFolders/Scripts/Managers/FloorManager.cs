using UnityEngine;
using SnakeV.Abstracts;
using SnakeV.Utilities;
using System.Collections;
using System.Collections.Generic;

namespace SnakeV.Core.Managers
{
    public class FloorManager : Singleton<FloorManager>
    {
        private int _height;
        private int _width;
        [SerializeField] private Transform _tilesAndEdges;

        public Tile[,] allTiles;
        private TailController _tailController; //better to make this singleton??
        private TileLoader _tileLoader;

        public int Height => _height;
        public int Width => _width;
        private int _lavaIterations = 0;

        protected override void Awake()
        {
            base.Awake();
            _tileLoader = new TileLoader(this);
            _width = _height = PlayerPrefs.GetInt("MapSize");
            allTiles = new Tile[_width, _height];
        }

        void Start()
        {
            PlayerController.Instance.transform.position = new Vector3(_width / 2, 0.6f, _height / 2); //bad practice to reach this on this way.
            _tailController = PlayerController.Instance.tailController;
            StartCoroutine(LavaRoutine());
        }

        public void CreateFloor(Tile tile)
        {
            for(int y=0; y<_height; y++)
            {
                for(int x=0; x<_width; x++)
                {
                    Vector3 posToSpawn = new Vector3(x, 0.55f, y);
                    Tile spawnedTile = Instantiate(tile, posToSpawn, Quaternion.identity, _tilesAndEdges.transform);
                    
                    allTiles[x, y] = spawnedTile;
                    spawnedTile.SetTilePos(x, y);

                    if (!GameManager.Instance.IsEdgesOn)
                        continue;
                }
            }

            StaticBatchingUtility.Combine(_tilesAndEdges.gameObject);
        }

        public void SpawnEdges(GameObject edge)
        {
            Vector3 posToSpawn = Vector3.zero;

            for(int i=-1; i<=_width; i++)
            {
                posToSpawn = new Vector3(i, 0.55f, -1);
                Instantiate(edge, posToSpawn, Quaternion.identity, _tilesAndEdges.transform);
                posToSpawn = new Vector3(i, 0.55f, _height);
                Instantiate(edge, posToSpawn, Quaternion.identity, _tilesAndEdges.transform);
            }

            for(int j=0; j<_height;j++)
            {
                posToSpawn = new Vector3(-1, 0.55f, j);
                Instantiate(edge, posToSpawn, Quaternion.identity, _tilesAndEdges.transform);
                posToSpawn = new Vector3(_width, 0.55f, j);
                Instantiate(edge, posToSpawn, Quaternion.identity, _tilesAndEdges.transform);
            }
            StaticBatchingUtility.Combine(_tilesAndEdges.gameObject);

        }
        public void LavaTime()
        {
            _lavaIterations++;
            bool isEmptySpace = true;

            Tile selectedTile = RandomizeTile();
            for (int i = 0; i < _tailController.tailsList.Count; i++)
            {
                isEmptySpace = selectedTile.CheckSafeDistanceFromSnake(_tailController.tailsList[i]);
            }

            if (isEmptySpace && !selectedTile.IsFilled)
                selectedTile.LavaOn();
            else if (_lavaIterations < 1000)
                LavaTime();

            StaticBatchingUtility.Combine(_tilesAndEdges.gameObject); //TODO: check performance for calling this frequently.
        }

        private Tile RandomizeTile()
        {
            int randX = Random.Range(0, _width);
            int randZ = Random.Range(0, _height);
            return allTiles[randX, randZ];
        }

        private IEnumerator LavaRoutine()
        {
            yield return new WaitForSeconds(5f);
            while(PlayerController.Instance.IsAlive)
            {
                LavaTime();
                yield return new WaitForSeconds(10f);
            }
        }

        private void Update()
        {
            _tileLoader.NormalUpdate();
        }

        private void OnDisable()
        {
            _tileLoader.ReleaseMemory();
        }
    }
}

