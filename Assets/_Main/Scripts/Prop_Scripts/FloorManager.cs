using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    private BrickSpawner _brickSpawner;
    public List<BrickType> _brickTypes = new List<BrickType>();
    private BrickType _colliderData;

    private void Start()
    {
        _brickSpawner = GetComponentInChildren<BrickSpawner>();
        if (_brickTypes.Count > 0)
        {
            foreach (var brickType in _brickTypes)
            {
                _brickSpawner._activeBrickTypes.Add(brickType);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Brick Spawner Set
        _colliderData = other.GetComponent<PlayerCollision>()._brickData;
        if (!_brickTypes.Contains(_colliderData))
        {
            _brickTypes.Add(_colliderData);
            _brickSpawner._activeBrickTypes.Add(_colliderData);
        }
        _brickSpawner.enabled = true;
        
        //AI
        if (other.TryGetComponent(out AiMovement aiMovement))
        {
            aiMovement.SpawnerSet(_brickSpawner);
            _brickSpawner.SpawnPack();
        }
        
        //Player
        if (other.TryGetComponent(out PlayerMovement playerMovement))
            _brickSpawner.SpawnPack();
    }
}
