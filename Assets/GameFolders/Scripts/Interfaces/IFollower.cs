using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeV.Abstracts
{
    public interface IFollower : IEntityController
    {
        Vector3 PreviousPos { get;}
        void SetNewPos(Vector3 posToSet);
        void SetPreviousPos();
        public int XPos { get; }
        public int ZPos { get; }
    }
}

