
using UnityEngine;

namespace SnakeV.Core.Managers
{
    public class SpawnManager
    {
        private int _height, _width;
        private Vector3 _posToSpawn;
        private int _iterations = 0;

        public SpawnManager()
        {
            _height = FloorManager.Instance.Height;
            _width = FloorManager.Instance.Width;
            AssignSpawnPos();
            SpawnTail();
        }

        public void SpawnNewFood(TailController tailController)
        {
            if (!PlayerController.Instance.IsAlive)
                return;
            CheckPossibleSpawnPos(tailController);
        }

        private void CheckPossibleSpawnPos(TailController tailController)
        {
            bool isEmpty = true;
            AssignSpawnPos();

            for(int i=0; i<tailController.tailsList.Count; i++)
            {
                if(tailController.tailsList[i].transform.position.x == (int)_posToSpawn.x && tailController.tailsList[i].transform.position.z == (int)_posToSpawn.z)
                {
                    isEmpty = false;
                    Debug.Log("Tried to spawn on: " + _posToSpawn.x + " and " + _posToSpawn.z);
                }
            }

            _iterations++;

            if (!isEmpty && _iterations < 100)
            {
                CheckPossibleSpawnPos(tailController); //recursive method! beware!
            }

            else if (!isEmpty && _iterations > 100)
                Debug.Log("No free point left bro ??");
            else
                SpawnTail();
        }

        private void AssignSpawnPos()
        {
            _posToSpawn = new Vector3(Random.Range(0, _width), 0, Random.Range(0, _height));

        }

        private void SpawnTail()
        {
            PoolManager.Instance.RequestTail(_posToSpawn);
        }


    }

}


