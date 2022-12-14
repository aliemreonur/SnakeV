using System.Collections;
using System.Collections.Generic;
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

        private Tile[,] _allTiles;

        public int Height => _height;
        public int Width => _width;

        private void Awake()
        {
            SingletonThisObj(this);
            _allTiles = new Tile[_width, _height];
            CreateFloor();
        }

        void Start()
        {        
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
                    
                }
            }
        }

    }
}

