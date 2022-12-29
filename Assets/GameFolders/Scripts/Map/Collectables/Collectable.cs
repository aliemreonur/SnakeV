using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeV.Abstracts;
using SnakeV.Core.Managers;

namespace SnakeV.Core.Collectables
{
    public class Collectable : ICollectable
    {
        private List<GameObject> _allFruits = new List<GameObject>();
        public Transform transform { get; private set; }
        private FloorManager _floorManager;
        MonoBehaviour _monoBehaviourObj;

        public Collectable(FloorManager floorManager, MonoBehaviour monoBehaviour)
        {
            _floorManager = floorManager;
            _monoBehaviourObj = monoBehaviour;
        }

        public void Collected()
        {
            RandomizeCollectable();
        }

        public void AddToList(GameObject objToAdd)
        {
            _allFruits.Add(objToAdd);
        }

        public void RandomizeCollectable()
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
            _floorManager.allTiles[(int)_monoBehaviourObj.transform.position.x, (int)_monoBehaviourObj.transform.position.z].TileEmpty();
            _monoBehaviourObj.transform.position = newPos;
            _floorManager.allTiles[(int)newPos.x, (int)newPos.z].TileFull(); 
            RandomizeCollectable();
        }

    }
}

