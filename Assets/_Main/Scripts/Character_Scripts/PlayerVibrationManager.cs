using UnityEngine;

public class PlayerVibrationManager : MonoBehaviour
{
    private void Awake()
    {
        Vibration.Init();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractible interactible))
        {
            Vibration.VibratePop();
        }
    }
}
