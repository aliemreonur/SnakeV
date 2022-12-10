using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeV.Abstracts
{
    public interface IFillable
    {
        GameObject gameObject { get; }
        bool IsFilled { get; }
    }
}

