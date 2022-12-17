using UnityEngine;
using SnakeV.Abstracts;
using SnakeV.Utilities;

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

        private Tile[,] _allTiles;

        public int Height => _height;
        public int Width => _width;

        protected override void Awake()
        {
            base.Awake();
            _allTiles = new Tile[_width, _height];
           
        }

        void Start()
        {
            CreateFloor();
            PlayerController.Instance.transform.position = new Vector3(_width / 2, 0.1f, _height / 2); //bad practice to reach this on this way.
        }

        public bool CheckTile(Vector2Int posToCheck)
        {
            return _allTiles[posToCheck.x, posToCheck.y].IsFilled;
        }

        private void CreateFloor()
        {
            for(int y=0; y<_height; y++)
            {
                for(int x=0; x<_width; x++)
                {
                    Vector3 posToSpawn = new Vector3(x, 0, y);
                    Instantiate(_tilePrefab, posToSpawn, Quaternion.identity, transform);

                    if (!GameManager.Instance.IsEdgesOn)
                        continue;

                    //TODO: DONT LIKE THIS CODE!
                    //spawn edges
                    if(x==0)
                    {
                        posToSpawn = new Vector3(-1, 0, y);
                        Instantiate(_edgePrefab, posToSpawn, Quaternion.identity, transform);
                    }
                    else if(x==_width-1)
                    {
                        posToSpawn = new Vector3(_width, 0, y);
                        Instantiate(_edgePrefab, posToSpawn, Quaternion.identity, transform);
                    }

                    if(y==0)
                    {
                        posToSpawn = new Vector3(x, 0, -1);
                        Instantiate(_edgePrefab, posToSpawn, Quaternion.identity, transform);
                    }

                    else if(y==_height-1)
                    {
                        posToSpawn = new Vector3(x, 0, _height);
                        Instantiate(_edgePrefab, posToSpawn, Quaternion.identity, transform);
                    }
                }
            }

            if(GameManager.Instance.IsEdgesOn)
                SpawnCornerEdges();
        }

        private void SpawnCornerEdges()
        {
            Vector3 posToSpawn = new Vector3(-1, 0, -1);
            Instantiate(_edgePrefab, posToSpawn, Quaternion.identity, transform);

            posToSpawn = new Vector3(-1, 0, _height);
            Instantiate(_edgePrefab, posToSpawn, Quaternion.identity, transform);

            posToSpawn = new Vector3(_width , 0, -1);
            Instantiate(_edgePrefab, posToSpawn, Quaternion.identity, transform);

            posToSpawn = new Vector3(_width, 0, _height);
            Instantiate(_edgePrefab, posToSpawn, Quaternion.identity, transform);
        }
    }
}

