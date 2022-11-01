using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBrick : MonoBehaviour, IInteractible
{
    [SerializeField] private MeshRenderer _meshRenderer;
    private BrickSpawner _brickSpawner;
    private BrickType _brickType;

    public void BrickInitializer (BrickType brickType)
    {
        _brickType = brickType;
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
