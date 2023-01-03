using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeV.Abstracts
{
    public interface IControllable
    {
        IInputConverter InputConverter { get; set; }
        Vector3 Direction { get;}
        void SetDirection();
        void Death();
    }
}

