using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiIdleState : AiStateBase
{
    public AiIdleState(AiMovement aiMovement) : base(aiMovement)
    {
        
    }

    public override void OnEnter()
    {
        Debug.Log("Idle State " + _aiMovement.gameObject.name);
        _aiMovement.ChangeState(typeof(AiLootingState));
        return;
        
        _aiMovement.ClearDestination();
        
        _aiMovement.SetWalking(false);
        
        
    }

    public override void OnUpdate()
    {
        
    }
}