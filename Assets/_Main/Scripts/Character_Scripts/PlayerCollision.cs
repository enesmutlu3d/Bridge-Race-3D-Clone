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
}
