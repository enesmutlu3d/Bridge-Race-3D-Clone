using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairStep : MonoBehaviour, IInteractible
{
    private MeshRenderer _meshRenderer;
    private BrickType _currentBrickType;
    private PlayerStackManager _playerStack;
    private bool isEmpty = true;
    private int _colliderScale = 1;
    [SerializeField] private Transform _floorCollider;
    [SerializeField] private bool _isShort;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        if (_isShort)
            _colliderScale = 2;
    }

    public void OnInteract(BrickType brickType, Transform interactor)
    {
        _playerStack = interactor.GetComponent<PlayerStackManager>();

        if (_playerStack._stacks.Count <= 0 ||
            _currentBrickType == brickType) 
            return;
        _currentBrickType = brickType;
        _playerStack.RemoveStack(transform.position, true);
        StepEnable();
    }

    private void StepEnable()
    {
        //Visual Enabling
        _meshRenderer.material = _currentBrickType._material;
        _meshRenderer.enabled = true;

        if (isEmpty)
        {
            isEmpty = false;
            _floorCollider.localScale += Vector3.forward * 0.05f * _colliderScale;
        }
    }
}
