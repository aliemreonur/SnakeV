using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using SnakeV.Core.Managers;

namespace SnakeV.Core
{
    public class FoodSpawner
    {
        private int _height, _width;
        private Vector3 _posToSpawn;
        private int _iterations = 0;

        public FoodSpawner()
        {
            _height = FloorManager.Instance.Height;
            _width = FloorManager.Instance.Width;
            _posToSpawn = Vector3.zero;
            AssignSpawnPos();
        }

        public void SpawnNewFood(TailController tailController)
        {
            if (!PlayerController.Instance.IsAlive)
                return;
            CheckPossibleSpawnPos(tailController);
        }

        public Vector3 CheckPossibleSpawnPos(TailController tailController)
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

            return _posToSpawn;
        }

        private void AssignSpawnPos()
        {
            _posToSpawn = new Vector3(Random.Range(0, _width), 0, Random.Range(0, _height));
        }
    }

}


