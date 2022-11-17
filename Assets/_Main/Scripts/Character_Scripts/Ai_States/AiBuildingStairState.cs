using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBuildingStairState : AiStateBase
{
    public AiBuildingStairState(AiStateMachine aiStateMachine) : base(aiStateMachine)
    {
        
    }

    public override void OnEnter()
    {
        AiStateMachine.SetDestination(AiStateMachine.GetFinishLine().position);
        
        AiStateMachine.SetWalking(true);
    }

    public override void OnUpdate()
    {
        if (AiStateMachine.StacksCount < 1)
        {
            AiStateMachine.ChangeState(typeof(AiLootingState));
        }
    }
}
