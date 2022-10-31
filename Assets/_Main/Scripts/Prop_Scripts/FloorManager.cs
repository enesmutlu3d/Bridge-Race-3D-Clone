using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        _brickSpawner.enabled = true;
        _colliderData = other.GetComponent<PlayerCollision>()._brickData;
        if (_brickTypes.Contains(_colliderData))
            return;
        _brickTypes.Add(_colliderData);
        _brickSpawner._activeBrickTypes.Add(_colliderData);
    }
}
