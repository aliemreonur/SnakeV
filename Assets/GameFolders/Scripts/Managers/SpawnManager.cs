
using UnityEngine;

namespace SnakeV.Core.Managers
{
    public class SpawnManager
    {
        private int _height, _width;
        Vector3 posToSpawn;
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
                if(tailController.tailsList[i].CurrentPos.x == (int)posToSpawn.x && tailController.tailsList[i].CurrentPos.y == (int)posToSpawn.z)
                {
                    isEmpty = false;
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
            posToSpawn = new Vector3(UnityEngine.Random.Range(0, _width), 0, UnityEngine.Random.Range(0, _height));

        }

        private void SpawnTail()
        {
            PoolManager.Instance.RequestTail(posToSpawn);
        }


    }

}


