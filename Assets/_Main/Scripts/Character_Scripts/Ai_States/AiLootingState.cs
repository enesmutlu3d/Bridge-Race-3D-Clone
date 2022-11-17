using UnityEngine;

public class AiLootingState : AiStateBase
{
    private int _collectAmount;
    
    public AiLootingState(AiStateMachine aiStateMachine) : base(aiStateMachine)
    {
        
    }

    private bool DestinationPick(out Vector3 destinationPos)
    {
        if (AiStateMachine.BrickSpawner.TryGetBrick(AiStateMachine.BrickType, out Transform brick))
        {
            destinationPos = brick.position;
            return true;
        }

        destinationPos = default;
        return false;
    }

    public override void OnEnter()
    {
        _collectAmount = Random.Range(2, 10);
        
        AiStateMachine.ClearDestination();
        
        AiStateMachine.SetWalking(true);
    }

    public override void OnUpdate()
    {
        if (AiStateMachine.CheckPathComplete() && AiStateMachine.RemaningDistance() > 0.05f)
            return;
        
        int stacksCount = AiStateMachine.StacksCount;
        if (stacksCount < _collectAmount)
        {
            if (DestinationPick(out Vector3 destinationPos))
                AiStateMachine.SetDestination(destinationPos);
            else
                AiStateMachine.ChangeState(typeof(AiIdleState));
        }
        else
        {
            AiStateMachine.ChangeState(typeof(AiBuildingStairState));
        }
    }
}
