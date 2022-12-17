using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectable 
{
    Transform transform { get; }
    void Collected();
    void AddToList(GameObject obj);
    void MoveToNewPos(Vector3 pos);
    void RandomCollectable();
}
