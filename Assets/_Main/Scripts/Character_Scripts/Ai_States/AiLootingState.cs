using UnityEngine;

public class AiLootingState : AiStateBase
{
    private const int checkLimit = 15;

    private int _collectAmount;
    
    public AiLootingState(AiMovement aiMovement) : base(aiMovement)
    {
        
    }

    private bool DestinationPick(out Vector3 destinationPos)
    {
        Transform brickSpawnerTransform = _aiMovement.BrickSpawner.transform;
        Transform randomChild;
        int checkLimiter = 0;
        
        do
        {
            if (brickSpawnerTransform.childCount < 1)
            {
                destinationPos = default;
                return false;
            }
            int random = Random.Range(0, brickSpawnerTransform.childCount - 1);
            randomChild = brickSpawnerTransform.GetChild(random);
            checkLimiter++;
        } while (!_aiMovement.CheckBrickType(randomChild.GetComponent<CollectibleBrick>()._brickType) && checkLimiter < checkLimit);

        if (checkLimiter >= checkLimit)
        {
            destinationPos = default;
            return false;
        }
        
        destinationPos = randomChild.position;
        return true;
    }

    public override void OnEnter()
    {
        _collectAmount = Random.Range(2, 10);
        if (DestinationPick(out Vector3 destinationPos))
            _aiMovement.SetDestination(destinationPos);
    }

    public override void OnUpdate()
    {
        int stacksCount = _aiMovement.StacksCount;

        if (_aiMovement.RemaningDistance() > 0.05f)
            return;
        
        if (stacksCount < _collectAmount)
        {
            if (DestinationPick(out Vector3 destinationPos))
                _aiMovement.SetDestination(destinationPos);
        }
        else
        {
            _aiMovement.ChangeState(typeof(AiBuildingStairs));
        }
    }
}
