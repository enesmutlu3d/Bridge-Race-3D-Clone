using UnityEngine;


[CreateAssetMenu(fileName = "BrickType", menuName = "BrickTypes/BrickType")]
public class BrickType : ScriptableObject
{
    [SerializeField] public Material _material;
    [SerializeField] public BrickOwner _brickType = BrickOwner.Player;

    public enum BrickOwner
    {
        Player,
        AI_1,
        AI_2,
        AI_3
    }
}
