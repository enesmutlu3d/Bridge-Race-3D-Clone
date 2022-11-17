using UnityEngine;

public class AiLootingState : AiStateBase
{
    private int _collectAmount;
    
    public AiLootingState(AiMovement aiMovement) : base(aiMovement)
    {
        
    }

    private bool DestinationPick(out Vector3 destinationPos)
    {
        if (_aiMovement.BrickSpawner.TryGetBrick(_aiMovement.BrickType, out Transform brick))
        {
            destinationPos = brick.position;
            return true;
        }

        destinationPos = default;
        return false;
    }

    public override void OnEnter()
    {
        Debug.Log("Looting State " + _aiMovement.gameObject.name);
        
        _collectAmount = Random.Range(2, 10);
        
        _aiMovement.ClearDestination();
        
        _aiMovement.SetWalking(true);
    }

    public override void OnUpdate()
    {
        if (_aiMovement.CheckPathComplete() && _aiMovement.RemaningDistance() > 0.05f)
            return;
        
        int stacksCount = _aiMovement.StacksCount;
        if (stacksCount < _collectAmount)
        {
            if (DestinationPick(out Vector3 destinationPos))
                _aiMovement.SetDestination(destinationPos);
            else
                _aiMovement.ChangeState(typeof(AiIdleState));
        }
        else
        {
            _aiMovement.ChangeState(typeof(AiBuildingStairs));
        }
    }
}
