using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiIdleState : AiStateBase
{
    public AiIdleState(AiStateMachine aiStateMachine) : base(aiStateMachine)
    {
        
    }

    public override void OnEnter()
    {
        AiStateMachine.ChangeState(typeof(AiLootingState));
        return;
        
        AiStateMachine.ClearDestination();
        
        AiStateMachine.SetWalking(false);
        
        
    }

    public override void OnUpdate()
    {
        
    }
}