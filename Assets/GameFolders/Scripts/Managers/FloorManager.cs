using UnityEngine;
using SnakeV.Abstracts;
using SnakeV.Utilities;
using System.Collections;
using System.Collections.Generic;

namespace SnakeV.Core.Managers
{
    public class FloorManager : Singleton<FloorManager>
    {
        [Range(10,50)]
        [SerializeField] private int _height;
        [Range(10, 50)]
        [SerializeField] private int _width;
        [SerializeField] private Tile _tilePrefab; //change its type to tile rather than game object
        [SerializeField] private GameObject _edgePrefab;
        [SerializeField] private Transform _tilesAndEdges;

        public Tile[,] _allTiles;
        private TailController _tailController; //better to make this singleton??

        public int Height => _height;
        public int Width => _width;

        private int _lavaIterations = 0;

        protected override void Awake()
        {
            base.Awake();
            _allTiles = new Tile[_width, _height];
        }

        void Start()
        {
            CreateFloor();
            PlayerController.Instance.transform.position = new Vector3(_width / 2, 0.6f, _height / 2); //bad practice to reach this on this way.
            _tailController = PlayerController.Instance.tailController;
            StartCoroutine(LavaRoutine());
        }

        private void CreateFloor()
        {
            for(int y=0; y<_height; y++)
            {
                for(int x=0; x<_width; x++)
                {
                    Vector3 posToSpawn = new Vector3(x, 0, y);
                    Tile spawnedTile = Instantiate(_tilePrefab, posToSpawn, Quaternion.identity, _tilesAndEdges.transform);
                    _allTiles[x, y] = spawnedTile;
                    spawnedTile.SetTilePos(x, y);

                    if (!GameManager.Instance.IsEdgesOn)
                        continue;

                    //TODO: DONT LIKE THIS CODE!
                    //spawn edges
                    if(x==0)
                    {
                        posToSpawn = new Vector3(-1, 0, y);
                        Instantiate(_edgePrefab, posToSpawn, Quaternion.identity, _tilesAndEdges.transform);
                    }
                    else if(x==_width-1)
                    {
                        posToSpawn = new Vector3(_width, 0, y);
                        Instantiate(_edgePrefab, posToSpawn, Quaternion.identity, _tilesAndEdges.transform);
                    }

                    if(y==0)
                    {
                        posToSpawn = new Vector3(x, 0, -1);
                        Instantiate(_edgePrefab, posToSpawn, Quaternion.identity, _tilesAndEdges.transform);
                    }

                    else if(y==_height-1)
                    {
                        posToSpawn = new Vector3(x, 0, _height);
                        Instantiate(_edgePrefab, posToSpawn, Quaternion.identity, _tilesAndEdges.transform);
                    }
                }
            }

            if(GameManager.Instance.IsEdgesOn)
                SpawnCornerEdges();
        }

        private void SpawnCornerEdges()
        {
            Vector3 posToSpawn = new Vector3(-1, 0, -1);
            Instantiate(_edgePrefab, posToSpawn, Quaternion.identity, _tilesAndEdges.transform);

            posToSpawn = new Vector3(-1, 0, _height);
            Instantiate(_edgePrefab, posToSpawn, Quaternion.identity, _tilesAndEdges.transform);

            posToSpawn = new Vector3(_width , 0, -1);
            Instantiate(_edgePrefab, posToSpawn, Quaternion.identity, _tilesAndEdges.transform);

            posToSpawn = new Vector3(_width, 0, _height);
            Instantiate(_edgePrefab, posToSpawn, Quaternion.identity, _tilesAndEdges.transform);
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

            if (isEmptySpace)
                selectedTile.TurnLavaOn();
            else if (_lavaIterations < 1000)
                LavaTime();
        }

        private Tile RandomizeTile()
        {
            int randX = Random.Range(0, _width);
            int randZ = Random.Range(0, _height);
            return _allTiles[randX, randZ];
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
    }
}

