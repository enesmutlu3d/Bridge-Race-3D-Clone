using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    [SerializeField] public GameObject _brick;
    [SerializeField] public BrickType[] _brickTypes;
    [SerializeField] private Vector2 GridSize;

    private GameObject _SpawnedBrick;
    private CollectibleBrick _collectibleBrick;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        for (int i = 0; i < GridSize.x; i++)
        {
            for (int j = 0; j < GridSize.y; j++)
            {
                _SpawnedBrick = Instantiate(_brick, new Vector3 (j -4, transform.position.y, i -4), Quaternion.identity);
                _collectibleBrick = _SpawnedBrick.GetComponent<CollectibleBrick>();
                _collectibleBrick._brickData = _brickTypes[Mathf.RoundToInt(Random.Range(-0.5f,3.5f))];
                _collectibleBrick.BrickInitializer();
            }
        }
    }
}
