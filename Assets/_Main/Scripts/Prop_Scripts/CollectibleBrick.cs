using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBrick : MonoBehaviour, IInteractible
{
    [SerializeField] public BrickType[] _brickTypes;
    [SerializeField] private MeshRenderer _meshRenderer;
    private BrickSpawner _brickSpawner;
    private BrickType _brickType;

    public void BrickInitializer (BrickType brickType)
    {
        _brickType = brickType;
        //_brickType = _brickTypes[Mathf.RoundToInt(Random.Range(-0.5f, 3.5f))];
        _meshRenderer.material = _brickType._material;
    }

    public void OnInteract(BrickType brickType, Transform interactor)
    {
        if (brickType != _brickType)
            return;
        _brickSpawner = GetComponentInParent<BrickSpawner>();
        if (_brickSpawner != null)
            _brickSpawner._respawnLocations.Add(transform.localPosition);
        interactor.GetComponent<PlayerStackManager>().AddStack(transform.gameObject);
    }
}
