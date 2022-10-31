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

    [HideInInspector] public List<Vector3> _respawnLocations = new List<Vector3>();

    private void Start()
    {
        _respawnDelay = new WaitForSeconds(2f);
        FirstSpawnPack();
        StartCoroutine(nameof(RespawnCo));
    }

    private void FirstSpawnPack()
    {
        for (int i = 0; i < GridSize.x; i++)
        {
            for (int j = 0; j < GridSize.y; j++)
            {
                _respawnLocations.Add(new Vector3(j - 4, 0f, i - 4));
            }
        }
    }

    private IEnumerator RespawnCo ()
    {
        yield return new WaitForEndOfFrame();
        while (true)
        {
            if (_respawnLocations != null)
            {
                foreach (Vector3 location in _respawnLocations)
                {
                    if (Random.Range(-0.49f, 3.49f) < _activeBrickTypes.Count)
                    {
                        _SpawnedBrick = Instantiate(_brick, transform);
                        _SpawnedBrick.transform.localPosition = location;
                        _randomVar = Mathf.RoundToInt(Random.Range(-0.49f, _activeBrickTypes.Count - 0.51f));
                        _SpawnedBrick.GetComponent<CollectibleBrick>().BrickInitializer(_activeBrickTypes[_randomVar]);
                    }
                }
                _respawnLocations.Clear();
            }
            yield return _respawnDelay;
        }
    }
}
