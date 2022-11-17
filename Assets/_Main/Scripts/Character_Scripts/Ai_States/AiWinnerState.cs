using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiWinnerState : AiStateBase
{
    public AiWinnerState(AiStateMachine aiStateMachine) : base(aiStateMachine)
    {
        
    }

    public override void OnEnter()
    {
        AiStateMachine.SetWinner();
        AiStateMachine.ClearDestination();
        AiStateMachine.SetAgent(false);
    }
}
