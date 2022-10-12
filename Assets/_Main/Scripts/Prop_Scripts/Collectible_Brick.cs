using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible_Brick : MonoBehaviour
{
    [SerializeField] public BrickType _brickData;
    
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        BrickInitializer();
    }

    public void BrickInitializer ()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _meshRenderer.material = _brickData._material;
    }
}
