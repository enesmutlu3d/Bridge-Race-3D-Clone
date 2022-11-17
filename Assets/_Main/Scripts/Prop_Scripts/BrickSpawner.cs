using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    private const int checkLimit = 100;
    private const float _respawnDelay = 2f;

    [SerializeField] private Vector2 GridSize;
    [SerializeField] public List<BrickType> _activeBrickTypes = new List<BrickType>();
    [SerializeField] private bool isFirstFloor;

    private BrickPoolManager _poolManager;
    private readonly  Dictionary<BrickType, List<CollectibleBrick>> _bricks = new Dictionary<BrickType, List<CollectibleBrick>>();

    private readonly List<Vector3> _respawnLocations = new List<Vector3>();
    private readonly List<Vector3> _emptyLocations = new List<Vector3>();

    private void Start()
    {
        _poolManager = GameObject.FindWithTag("BrickPool").GetComponent<BrickPoolManager>();
        GridSet();
        SpawnInitialBricks();
    }

    private void GridSet()
    {
        if (!isFirstFloor)
            return;
        
        for (float i = -1 * (GridSize.x * 0.5f); i < (GridSize.x * 0.5f); i++)
        {
            for (float j = -1 * (GridSize.y * 0.5f); j < (GridSize.y * 0.5f); j++)
            {
                _respawnLocations.Add(new Vector3(j + 0.5f, 0f, i + 0.5f));
                _emptyLocations.Add(new Vector3(j + 0.5f, 0f, i + 0.5f));
            }
        }
    }

    public void SpawnPack()
    {
        for (float i = -1 * (GridSize.x * 0.5f); i < (GridSize.x * 0.5f); i++)
        {
            for (float j = -1 * (GridSize.y * 0.5f); j < (GridSize.y * 0.5f); j++)
            {
                if (Random.Range(0,10) < 3)
                    _emptyLocations.Add(new Vector3(j + 0.5f, 0f, i + 0.5f));
            }
        }
        foreach (var location in _emptyLocations)
        {
            if (!_respawnLocations.Contains(location))
                _respawnLocations.Add(location);
        }
    }

    private void SpawnBrickOnLocation(Vector3 location)
    {
        GameObject spawnedBrick = _poolManager.SpawnBrickFromPool(transform);
        CollectibleBrick collectibleBrick = spawnedBrick.GetComponent<CollectibleBrick>();
        collectibleBrick.OnCollected += BrickCollected;
        spawnedBrick.transform.localPosition = location;
        spawnedBrick.transform.rotation = Quaternion.identity;
        spawnedBrick.SetActive(true);
        int randomVar = Random.Range(0, _activeBrickTypes.Count);
        BrickType brickType = _activeBrickTypes[randomVar];
        spawnedBrick.GetComponent<CollectibleBrick>().BrickInitializer(brickType);
                    
        if (_bricks.TryGetValue(brickType, out List<CollectibleBrick> brickTypeList))
            brickTypeList.Add(collectibleBrick);
        else
        {
            brickTypeList = new List<CollectibleBrick>();
            brickTypeList.Add(collectibleBrick);
            _bricks.Add(brickType, brickTypeList);
        }
    }

    private void SpawnInitialBricks ()
    {
        foreach (Vector3 location in _emptyLocations)
        {
            SpawnBrickOnLocation(location);
        }
        _emptyLocations.Clear();
    }

    private void ReinitEmptyBrick()
    {
        if (_emptyLocations.Count < 1)
            return;
        
        Vector3 location = _emptyLocations[0];
        SpawnBrickOnLocation(location);
    }

    private void BrickCollected(CollectibleBrick collectibleBrick)
    {
        collectibleBrick.OnCollected -= BrickCollected;
        _emptyLocations.Add((collectibleBrick.transform.position));
        Invoke(nameof(ReinitEmptyBrick), _respawnDelay);
    }

    public bool TryGetBrick(BrickType brickType, out Transform brick)
    {
        if (_bricks.TryGetValue(brickType, out List<CollectibleBrick> brickTypeList) &&
            brickTypeList.Count > 0)
        {
            brick = brickTypeList[Random.Range(0, brickTypeList.Count - 1)].transform;
            return true;
        }

        brick = default;
        return false;
    }
}
