using UnityEngine;

public class PlayerData : ScriptableObject
{
    public Vector2 Position { get; protected set; }
    public MovementStateMachine.State State { get; protected set; }
}