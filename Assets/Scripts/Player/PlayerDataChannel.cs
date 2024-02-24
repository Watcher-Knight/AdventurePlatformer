using ArchitectureLibrary;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataChannel", menuName = CreateAssetPaths.PlayerDataChannel, order = 0)]
public class PlayerDataChannel : PlayerData
{
    public void WriteTo(Vector2 position, MovementStateMachine.State state)
    {
        Position = position;
        State = state;
    }
}