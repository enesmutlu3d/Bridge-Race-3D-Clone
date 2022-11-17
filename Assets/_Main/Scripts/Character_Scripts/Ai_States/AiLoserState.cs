using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiLoserState : AiStateBase
{
    public AiLoserState(AiStateMachine aiStateMachine) : base(aiStateMachine)
    {
        
    }

    public override void OnEnter()
    {
        AiStateMachine.SetLoser();
        AiStateMachine.ClearDestination();
        AiStateMachine.SetAgent(false);
    }
}
