using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeV.Abstracts;

public class Tile: MonoBehaviour, IFillable
{
    public bool IsFilled { get; set; }
}
