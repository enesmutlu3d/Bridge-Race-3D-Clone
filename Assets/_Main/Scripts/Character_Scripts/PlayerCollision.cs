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
        if (other.transform.TryGetComponent(out PlayerStackManager playerStackManager) &&
            playerStackManager._stacks.Count > _stackManager._stacks.Count &&
             _stackManager._stacks.Count > 1)
        {
            _stackManager.StackSpread();
                
            //Ai Set State
            if (transform.TryGetComponent(out AiStateMachine aiStateMachine))
                aiStateMachine.ChangeState(typeof(AiHitState));
        }
    }
}
