using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeV.Abstracts;

namespace SnakeV.Core
{
    [RequireComponent(typeof(PlayerCollectableDetector))]
    public class Collectable : MonoBehaviour, ICollectable
    {
        private List<GameObject> _allFruits = new List<GameObject>();

        public void Collected()
        {
            RandomCollectable();
        }

        public void AddToList(GameObject objToAdd)
        {
            _allFruits.Add(objToAdd);
        }

        public void RandomCollectable()
        {
            int randomSelector = Random.Range(0, _allFruits.Count);

            foreach (GameObject obj in _allFruits)
            {
                if (obj.activeInHierarchy == true)
                    obj.SetActive(false);
            }
            _allFruits[randomSelector].SetActive(true);
        }

        public void MoveToNewPos(Vector3 newPos)
        {
            transform.position = newPos;
            RandomCollectable();
        }

    }
}

