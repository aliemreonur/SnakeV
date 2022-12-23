using UnityEngine;
using SnakeV.Core.Managers;
using SnakeV.Core;

namespace SnakeV.Utilities
{
    public static class DetermineSpawnPos
    {
        public static Vector3 GetEmptySpawnPos(TailController tailController, FloorManager floorManager)
        {
            Vector3 posToSpawn = Vector3.zero;
            int iterations = 0;
            bool isEmpty = false;

            while(!isEmpty && iterations <500)
            {
                isEmpty = true;
                posToSpawn = new Vector3(Random.Range(0, floorManager.Width), 0.55f, Random.Range(0, floorManager.Height));

                for (int i = 0; i < tailController.tailsList.Count; i++)
                {
                    if (tailController.tailsList[i].transform.position.x == (int)posToSpawn.x && tailController.tailsList[i].transform.position.z == (int)posToSpawn.z)
                    {
                        isEmpty = false;
                    }

                    if (isEmpty) 
                    {
                        isEmpty = CheckFloorTiles(posToSpawn, isEmpty);
                    }
                }

                iterations++;
            }

            if (!isEmpty)
            {
                posToSpawn = Vector3.zero;
                GameManager.Instance.GameWon();
            }
                

            return posToSpawn;
        }

        private static bool CheckFloorTiles(Vector3 posToSpawn, bool isEmpty)
        {
            if (FloorManager.Instance.allTiles[(int)posToSpawn.x, (int)posToSpawn.z].IsFilled)
                isEmpty = false;
            return isEmpty;
        }
    }
    
}

