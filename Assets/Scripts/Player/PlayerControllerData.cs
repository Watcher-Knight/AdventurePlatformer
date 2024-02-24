using UnityEngine;

[CreateAssetMenu(fileName = "PlayerControllerData", menuName = CreateAssetPaths.PlayerControllerData, order = 0)]
public class PlayerControllerData : ScriptableObject
{
    [field: SerializeField] public float JumpBufferTime { get; private set; }
    [field: SerializeField] public float GrappleBufferTime { get; private set; }
}