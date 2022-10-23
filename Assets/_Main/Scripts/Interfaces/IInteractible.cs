using UnityEngine;

public interface IInteractible
{
    void OnInteract(BrickType brickType, Transform interactor);
}
