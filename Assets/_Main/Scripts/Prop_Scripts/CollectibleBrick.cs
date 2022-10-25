using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBrick : MonoBehaviour, IInteractible
{
    [SerializeField] public BrickType _brickData;
    [SerializeField] public BrickType[] _brickTypes;
    
    [SerializeField] private MeshRenderer _meshRenderer;
    private BrickSpawner _brickSpawner;

    private void Start()
    {
        BrickInitializer();
    }

    public void BrickInitializer ()
    {
        _brickData = _brickTypes[Mathf.RoundToInt(Random.Range(-0.5f, 3.5f))];
        _meshRenderer.material = _brickData._material;
    }

    public void OnInteract(BrickType brickType, Transform interactor)
    {
        _brickSpawner = GetComponentInParent<BrickSpawner>();
        if (_brickSpawner != null)
            _brickSpawner._respawnLocations.Add(transform.position);
        if (brickType == _brickData)
        {
            interactor.GetComponent<PlayerStackManager>().AddStack(transform.gameObject);
        }
    }
}
