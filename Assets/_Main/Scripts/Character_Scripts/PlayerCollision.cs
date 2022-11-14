using System;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] public BrickType _brickData;
    private PlayerStackManager _stackManager;

    private void Awake()
    {
        _stackManager = GetComponent<PlayerStackManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractible interactible))
        {
            interactible.OnInteract(_brickData, transform);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.TryGetComponent(out PlayerStackManager _playerStackManager))
        {
            if (_playerStackManager._stacks.Count > _stackManager._stacks.Count
                && _stackManager._stacks.Count > 3)
                _stackManager.StackSpread();
        }
    }
}
