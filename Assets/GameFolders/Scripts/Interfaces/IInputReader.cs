using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeV.Abstracts
{
    public interface IInputReader
    {
        float Horizontal { get; set; }
        float Vertical { get; set; }
    }

}

