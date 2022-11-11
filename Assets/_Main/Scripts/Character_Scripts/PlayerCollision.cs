using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] public BrickType _brickData;
    private PlayerStackManager _stackManager;

    private void Awake()
    {
        _stackManager = GetComponent<PlayerStackManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractible interactible))
        {
            interactible.OnInteract(_brickData, transform);
        }
    }

    
    // TO DO Spread Bricks
    /*public void OnInteract(BrickType brickType, Transform interactor)
    {
        if (interactor.GetComponent<PlayerStackManager>()._stacks.Count > _stackManager._stacks.Count)
            _stackManager.StackSpread();
    }*/
}
