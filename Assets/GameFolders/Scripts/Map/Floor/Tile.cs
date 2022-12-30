using UnityEngine;
using SnakeV.Abstracts;

namespace SnakeV.Core
{
    public class Tile : MonoBehaviour
    {
        public bool IsFilled => _isFilled;

        private bool _isFilled;
        private int xPos, zPos;
        private GameObject _lavaObj;

        public void SetTilePos(int x, int z)
        {
            xPos = x;
            zPos = z;
        }

        private void Awake()
        {
            _lavaObj = transform.GetChild(1).gameObject;
            _lavaObj.SetActive(false);
        }

        public bool CheckSafeDistanceFromSnake(IFollower tailToCheck)
        {
            if (Mathf.Abs(tailToCheck.transform.position.x - xPos) < 3 ||
                Mathf.Abs(tailToCheck.transform.position.z - zPos) < 3)
                return false;
            else
                return true;
        }

        public void TileFull()
        {
            if (_isFilled)
                return;

            _isFilled = true;
        }

        public void TileEmpty()
        {
            if(_isFilled)
                _isFilled = false;
        }

        public void LavaOn()
        {
            TileFull();
            _lavaObj.SetActive(true);
        }
    }
}

