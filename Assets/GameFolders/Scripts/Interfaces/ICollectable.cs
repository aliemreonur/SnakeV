using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeV.Abstracts
{
    public interface ICollectable : IEntityController
    {
        Transform transform { get; }
        void Collected();
        void AddToList(GameObject obj);
        void MoveToNewPos(Vector3 pos);
        void RandomizeCollectable();
    }
}


