using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBrick : MonoBehaviour, IInteractible
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Material _passsiveMaterial;
    [HideInInspector] public BrickSpawner _brickSpawner;
    [HideInInspector] public BrickType _brickType;

    private BoxCollider _boxCollider;
    private WaitForSeconds _passiveDelay;
    private bool isPassive;

    public event Action<CollectibleBrick> OnCollected;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _passiveDelay = new WaitForSeconds(0.75f);
    }

    public void BrickInitializer (BrickType brickType)
    {
        _brickType = brickType;
        _meshRenderer.material = _brickType._material;
    }

    public void OnInteract(BrickType brickType, Transform interactor)
    {
        if (brickType == _brickType || isPassive)
        {
            OnCollected?.Invoke(this);
            BrickInitializer(brickType);
            interactor.GetComponent<PlayerStackManager>().AddStack(transform.gameObject);
            isPassive = false;
        }
        
    }

    public void SpreadedBrick()
    {
        StartCoroutine("SpreadedBrickCO");
    }
    
    public IEnumerator SpreadedBrickCO()
    {
        _boxCollider.enabled = false;
        _meshRenderer.material = _passsiveMaterial;
        yield return _passiveDelay;
        _boxCollider.enabled = true;
        isPassive = true;
        yield return null;
        yield break;
    }
}
