using UnityEngine;
using SnakeV.Core.Managers;
using SnakeV.Core.Abstracts;

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
                posToSpawn = floorManager.SetRandomPosInMap();

                for (int i = 0; i < tailController.tailsList.Count; i++)
                {
                    isEmpty = CheckTails(tailController, posToSpawn) && CheckFloorTiles(posToSpawn);
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

        public static bool CheckFloorTiles(Vector3 posToSpawn)
        {
            bool posEmpty = true;
            if (FloorManager.Instance.allTiles[(int)posToSpawn.x, (int)posToSpawn.z].IsFilled)
                posEmpty = false;
            return posEmpty;
        }

        public static bool CheckTails(TailController tailController, Vector3 posToCheck)
        {
            bool posEmpty = tailController.CheckEmptySpaceForTails(posToCheck);
            return posEmpty;
        }

        public static bool IsPosValid(TailController tailController, Vector3 posToCheck)
        {
            if (posToCheck.x >= FloorManager.Instance.Width || posToCheck.z >= FloorManager.Instance.Height || posToCheck.x < 0 || posToCheck.z < 0)
                return false;

            return (CheckTails(tailController, posToCheck) && CheckFloorTiles(posToCheck));

        }
    }
    
}

