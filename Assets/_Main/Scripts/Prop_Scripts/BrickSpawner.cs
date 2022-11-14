using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    [SerializeField] private Vector2 GridSize;
    [SerializeField] public List<BrickType> _activeBrickTypes = new List<BrickType>();
    [SerializeField] private bool isFirstFloor;

    private BrickPoolManager _poolManager;
    private GameObject _SpawnedBrick;
    private WaitForSeconds _respawnDelay;
    private int _randomVar;

    private List<Vector3> _respawnLocations = new List<Vector3>();
    [HideInInspector] public List<Vector3> _emptyLocations = new List<Vector3>();

    private void Start()
    {
        _poolManager = GameObject.FindWithTag("BrickPool").GetComponent<BrickPoolManager>();
        _respawnDelay = new WaitForSeconds(2f);
        GridSet();
        StartCoroutine(nameof(RespawnCo));
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

    private IEnumerator RespawnCo ()
    {
        yield return new WaitForEndOfFrame();
        while (true)
        {
            if (_emptyLocations != null)
            {
                foreach (Vector3 location in _emptyLocations)
                {
                    _SpawnedBrick = _poolManager.SpawnBrickFromPool(transform);
                    _SpawnedBrick.transform.localPosition = location;
                    _SpawnedBrick.transform.rotation = Quaternion.identity;
                    _SpawnedBrick.SetActive(true);
                    _randomVar = Mathf.RoundToInt(Random.Range(-0.49f, _activeBrickTypes.Count - 0.51f));
                    _SpawnedBrick.GetComponent<CollectibleBrick>().BrickInitializer(_activeBrickTypes[_randomVar]);
                }
                _emptyLocations.Clear();
            }
            yield return _respawnDelay;
        }
    }
}
