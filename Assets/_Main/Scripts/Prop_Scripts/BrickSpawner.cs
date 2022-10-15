using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    [SerializeField] public GameObject _brick;
    [SerializeField] public BrickType[] _brickTypes;
    [SerializeField] private Vector2 GridSize;

    private GameObject _SpawnedBrick;
    private WaitForSeconds _respawnDelay;

    [HideInInspector] public List<Vector3> _respawnLocations = new List<Vector3>();

    private void Start()
    {
        _respawnDelay = new WaitForSeconds(2f);
        FirstSpawnPack();
        StartCoroutine("RespawnCo");
    }

    private void FirstSpawnPack()
    {
        for (int i = 0; i < GridSize.x; i++)
        {
            for (int j = 0; j < GridSize.y; j++)
            {
                _respawnLocations.Add(new Vector3(j - 4, transform.position.y, i - 4));
            }
        }
    }

    private IEnumerator RespawnCo ()
    {
        while (true)
        {
            if (_respawnLocations != null)
            {
                foreach (Vector3 location in _respawnLocations)
                {
                    _SpawnedBrick = Instantiate(_brick, location, Quaternion.identity);
                    _SpawnedBrick.transform.parent = transform;
                }
                _respawnLocations.Clear();
                yield return _respawnDelay;
            }
        }
    }
}
