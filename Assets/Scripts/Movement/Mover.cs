using System;
using ArchitectureLibrary;
using UnityEngine;

public class Mover
{
    public Percentage Control = 1;

    /// <summary>
    /// Moves object in direction specified. Best if used in fixed delta time.
    /// </summary>
    public void Update(IData data, Rigidbody2D rigidbody, float direction, float deltaTime)
    {
        Move(data, rigidbody, direction, deltaTime);
    }

    private void Move(IData data, Rigidbody2D rigidbody, float direction, float deltaTime)
    {
        float targetSpeed = Math.Sign(direction) * data.Speed;
        float speedDifference = targetSpeed - rigidbody.velocity.x;
        Percentage accelerationPercent = (Math.Abs(targetSpeed) > 0) ? data.Acceleration : data.Deceleration;
        float accelerationRate = accelerationPercent / deltaTime;
        float force = Mathf.Pow(Math.Abs(speedDifference) * accelerationRate, Control) * Math.Sign(speedDifference);
        rigidbody.AddForce(Vector2.right * force);
    }

    public interface IData
    {
        float Speed { get; }
        Percentage Acceleration { get; }
        Percentage Deceleration { get; }
    }
}