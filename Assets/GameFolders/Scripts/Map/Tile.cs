using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeV.Abstracts;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace SnakeV.Core
{
    public class Tile : MonoBehaviour
    {
        private int xPos, zPos;

        public void SetTilePos(int x, int z)
        {
            xPos = x;
            zPos = z;
        }

        public bool CheckSafeDistanceFromSnake(IFollower tailToCheck)
        {
            if (Mathf.Abs(tailToCheck.transform.position.x - xPos) < 3 ||
                Mathf.Abs(tailToCheck.transform.position.z - zPos) < 3)
                return false;
            else
                return true;
        }

        public void TurnLavaOn()
        {
            /// Option 1: enable the player death script and change the material of this.
            // Option 2: The lava and tile are seperate child objects. First, the tile is on, and lava will be turned on when necessary.
            // Option 3:
            //memory check on addressable loadings of differet objects. maybe better to use a single asset loader for this purpose?

            gameObject.SetActive(false);
            //StartCoroutine(DisappearRoutine());
        }

        private IEnumerator DisappearRoutine()
        {
            yield return null;
        }
    }
}

