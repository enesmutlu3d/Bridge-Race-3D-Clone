using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBuildingStairs : AiStateBase
{
    public AiBuildingStairs(AiMovement aiMovement) : base(aiMovement)
    {
        
    }

    public override void OnEnter()
    {
        _aiMovement.SetDestination(_aiMovement.GetFinishLine().position);
    }

    public override void OnUpdate()
    {
        if (_aiMovement.StacksCount < 1)
        {
            _aiMovement.ChangeState(typeof(AiLootingState));
        }
    }
}
