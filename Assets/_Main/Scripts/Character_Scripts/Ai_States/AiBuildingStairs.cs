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
        Debug.Log("BuildingStairs State " + _aiMovement.gameObject.name);
        
        _aiMovement.SetDestination(_aiMovement.GetFinishLine().position);
        
        _aiMovement.SetWalking(true);
    }

    public override void OnUpdate()
    {
        if (_aiMovement.StacksCount < 1)
        {
            _aiMovement.ChangeState(typeof(AiLootingState));
        }
    }
}
