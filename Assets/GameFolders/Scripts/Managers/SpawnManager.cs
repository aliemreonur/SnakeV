using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeV.Utilities;
using SnakeV.Core;
using SnakeV.Core.Managers;

public class SpawnManager : Singleton<SpawnManager>
{
    private int _height, _width;

    private void Awake()
    {
        SingletonThisObj(this);
    }

    private void Start()
    {
        _height = FloorManager.Instance.Height;
        _width = FloorManager.Instance.Width;
    }

    public void Spawn()
    {
        if(PlayerController.Instance.IsAlive)
        {
            Vector3 spawnPos = new Vector3(Random.Range(0, _width), 0, Random.Range(0, _height));
            PoolManager.Instance.RequestTail(spawnPos);
        }
    }
}
