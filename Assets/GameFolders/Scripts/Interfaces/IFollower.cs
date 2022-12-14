using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeV.Abstracts
{
    public interface IFollower
    {
        Vector3 PreviousPos { get;}
        Vector2Int CurrentPos { get; }
        Transform transform { get; }
        void SetNewPos(Vector3 posToSet);
        void SetPreviousPos();
    }
}

