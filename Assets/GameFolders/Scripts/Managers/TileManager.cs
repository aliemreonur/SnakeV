using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeV.Utilities;

namespace SnakeV.Core.Managers
{
    public class TileManager : Singleton<TileManager>
    {
        private Tile[,] allTiles;

        private void Awake()
        {
            SingletonThisObj(this);
        }

        private void Start()
        {
            
        }

        public void SpawnTiles()
        {

        }

    }

}


