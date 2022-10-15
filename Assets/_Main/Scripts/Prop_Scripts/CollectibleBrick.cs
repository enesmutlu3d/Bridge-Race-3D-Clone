using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBrick : MonoBehaviour, IInteractible
{
    [SerializeField] public BrickType _brickData;
    
    private MeshRenderer _meshRenderer;
    private BrickSpawner _brickSpawner;

    private void Start()
    {
        _brickSpawner = GetComponentInParent<BrickSpawner>();
        if (_brickSpawner != null)
            _brickData = _brickSpawner._brickTypes[Mathf.RoundToInt(Random.Range(-0.5f, 3.5f))];
        BrickInitializer();
    }

    public void BrickInitializer ()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _meshRenderer.material = _brickData._material;
    }

    public bool OnInteract(BrickType brickType)
    {
        _brickSpawner = GetComponentInParent<BrickSpawner>();
        if (_brickSpawner != null)
            _brickSpawner._respawnLocations.Add(transform.position);
        return brickType == _brickData;
    }
}
