using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeV.Abstracts
{

    public interface IInputConverter
    {
        IInputReader InputReader { get; set; }
        Vector3 MoveDirection { get; }
        void NormalUpdate();
        void Test();
    }
}


