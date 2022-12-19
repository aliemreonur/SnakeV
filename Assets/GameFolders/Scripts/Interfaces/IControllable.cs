using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeV.Abstracts
{
    public interface IControllable
    {
        IInputConverter InputConverter { get; set; }
        Transform transform { get; }
        Vector3 Direction { get;}
        void SetDirection();
    }
}

