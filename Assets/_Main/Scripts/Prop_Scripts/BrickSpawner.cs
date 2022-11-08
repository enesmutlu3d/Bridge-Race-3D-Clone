using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    [SerializeField] public GameObject _brick;
    [SerializeField] private Vector2 GridSize;
    public List<BrickType> _activeBrickTypes = new List<BrickType>();

    private GameObject _SpawnedBrick;
    private WaitForSeconds _respawnDelay;
    private int _randomVar;

    private List<Vector3> _respawnLocations = new List<Vector3>();
    [HideInInspector] public List<Vector3> _emptyLocations = new List<Vector3>();

    private void Start()
    {
        _respawnDelay = new WaitForSeconds(2f);
        GridSet();
        StartCoroutine(nameof(RespawnCo));
    }

    private void GridSet()
    {
        if (_activeBrickTypes.Count < 4)
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
                    _SpawnedBrick = Instantiate(_brick, transform);
                    _SpawnedBrick.transform.localPosition = location;
                    _randomVar = Mathf.RoundToInt(Random.Range(-0.49f, _activeBrickTypes.Count - 0.51f));
                    _SpawnedBrick.GetComponent<CollectibleBrick>().BrickInitializer(_activeBrickTypes[_randomVar]);
                }
                _emptyLocations.Clear();
            }
            yield return _respawnDelay;
        }
    }
}
