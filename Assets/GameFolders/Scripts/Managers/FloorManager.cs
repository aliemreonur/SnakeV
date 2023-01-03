using UnityEngine;
using SnakeV.Abstracts;
using SnakeV.Utilities;
using System.Collections;
using SnakeV.Core.Abstracts;

namespace SnakeV.Core.Managers
{
    public class FloorManager : Singleton<FloorManager>
    {
        private int _height = 10;
        private int _width = 10;
        [SerializeField] private Transform _tilesAndEdges;

        public Tile[,] allTiles;
        private TailController _tailController; //better to make this singleton??
        private PlayerController _playerController;
        private TileLoader _tileLoader;
        private int _numberOfLava;
        private int _maxNumberOfLava = 10;

        public int Height => _height;
        public int Width => _width;
        private int _lavaIterations = 0;
        private int _winScore;

        #region Public Methods
        public void CreateFloor(Tile tile)
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    Vector3 posToSpawn = new Vector3(x, 0.55f, y);
                    Tile spawnedTile = Instantiate(tile, posToSpawn, Quaternion.identity, _tilesAndEdges.transform);

                    allTiles[x, y] = spawnedTile;
                    spawnedTile.SetTilePos(x, y);

                    if (!_playerController.PreferenceSetter.IsEdgesOn)
                        continue;
                }
            }

            StaticBatchingUtility.Combine(_tilesAndEdges.gameObject); //called twice
        }

        public void SpawnEdges(GameObject edge)
        {
            Vector3 posToSpawn = Vector3.zero;

            for (int i = -1; i <= _width; i++)
            {
                posToSpawn = new Vector3(i, 0.55f, -1);
                Instantiate(edge, posToSpawn, Quaternion.identity, _tilesAndEdges.transform);
                posToSpawn = new Vector3(i, 0.55f, _height);
                Instantiate(edge, posToSpawn, Quaternion.identity, _tilesAndEdges.transform);
            }

            for (int j = 0; j < _height; j++)
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
            {
                selectedTile.LavaOn();
                _numberOfLava++;
            }

            else if (_lavaIterations < 1000)
                LavaTime();

            //StaticBatchingUtility.Combine(_tilesAndEdges.gameObject); //TODO: check performance for calling this frequently.
        }

        public Vector3 SetRandomPosInMap()
        {
            int xPos = Random.Range(0, Width);
            int zPos = Random.Range(0, Height);
            return new Vector3(xPos, 0.55f, zPos);
        }

        public bool IsAwayFromBounds(Vector3 posToCheck)
        {
            if (posToCheck.x + 2 >= _width || posToCheck.x - 2 < 0 || posToCheck.z + 2 >= _height || posToCheck.z - 2 < 0)
                return false;
            else
                return true;
        }
        #endregion

        #region Private Methods

        void Start()
        {
            _playerController = PlayerController.Instance;
            _tailController = PlayerController.Instance.tailController;
            InitializeTiles();
            _winScore = Mathf.FloorToInt(Width * Height * 0.3f); //TODO: will add multipler
            SetMaximumLavaAmount();
            SetPlayerStartPos();
            StartCoroutine(LavaRoutine());
        }

        private void InitializeTiles()
        {
            _tileLoader = new TileLoader(this);
            if(PlayerPrefs.HasKey("MapSize"))
            {
                _width = _height = PlayerPrefs.GetInt("MapSize");
            }
            else
            {
                _width = 10;
                _height = 10;
            }
       
            allTiles = new Tile[_width, _height];
        }

        private void SetMaximumLavaAmount()
        {
            _maxNumberOfLava = Mathf.FloorToInt(_width * _height * 0.3f);
        }

        private void SetPlayerStartPos()
        {
            Vector3 playerStartPos = new Vector3(_width / 2, 0.6f, _height / 2);
            _playerController.SetStartPos(playerStartPos);
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
            while(_playerController.IsAlive && _numberOfLava<_maxNumberOfLava)
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
        #endregion
    }
}

