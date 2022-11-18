using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiHitState : AiStateBase
{
    public AiHitState(AiStateMachine aiStateMachine) : base(aiStateMachine)
    {
        
    }

    public override void OnEnter()
    {
        AiStateMachine.ClearDestination();
        AiStateMachine.SetWalking(false);
        AiStateMachine.SetHit();
        AiStateMachine.OnHit += Hit;
    }

    public override void OnExit()
    {
        AiStateMachine.OnHit -= Hit;
    }

    private void Hit() => AiStateMachine.ChangeState(typeof(AiLootingState));
}
