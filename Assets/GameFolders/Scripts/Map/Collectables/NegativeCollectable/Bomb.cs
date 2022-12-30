using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeV.Core.Managers;
using SnakeV.Core;
using SnakeV.Utilities;

public class Bomb : MonoBehaviour
{
    private Tile _spawnedTile;
    private WaitForSeconds _onScreenTime;
    private WaitForSeconds _offScreenTime;
    private Vector3 _inactivePos;
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        if (_particleSystem == null)
            Debug.Log("Bomb could not gather the particle system");
        _inactivePos = transform.position;
        _onScreenTime = new WaitForSeconds(Random.Range(5, 15));
        _offScreenTime = new WaitForSeconds(Random.Range(5, 15));
    }

    private void StartMovingInMap()
    {
        StartCoroutine(ChangePosRoutine());
    }

    public void Spawn()
    {
        Vector3 posToSpawn = DetermineSpawnPos.GetEmptySpawnPos(PlayerController.Instance.tailController, FloorManager.Instance);
        transform.position = posToSpawn;
        _spawnedTile = FloorManager.Instance.allTiles[(int)posToSpawn.x, (int)posToSpawn.z];
        _spawnedTile.TileFull();

        ParticleEnabled(true);
    }

    public void MoveToInactivePoint()
    {
        ParticleEnabled(false);
        _spawnedTile.TileEmpty();
        transform.position = _inactivePos;
    }

    private void ParticleEnabled(bool isEnabled)
    {
        if (_particleSystem != null)
            _particleSystem.enableEmission = isEnabled;
    }

    private IEnumerator ChangePosRoutine()
    {
        while (PlayerController.Instance.IsAlive)
        {
            Spawn();
            yield return _onScreenTime;
            MoveToInactivePoint();
            yield return _offScreenTime;
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameStart += StartMovingInMap;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= StartMovingInMap;
    }
}
