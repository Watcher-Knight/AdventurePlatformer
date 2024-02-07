using System;
using ArchitectureLibrary;
using UnityEngine;

public class Jumper
{
    public Action onFinish;
    public bool IsJumping = false;

    public Jumper()
    {
        onFinish += () => { IsJumping = false; };
    }
    
    public void Jump(IData data, Rigidbody2D rigidbody)
    {
        rigidbody.velocity = Vector2.right * rigidbody.velocity.x;
        rigidbody.AddForce(Vector2.up * data.Force, ForceMode2D.Impulse);
        IsJumping = true;
    }
    public void Cancel(IData data, Rigidbody2D rigidbody)
    {
        if (IsJumping)
            rigidbody.AddForce(data.JumpCut * rigidbody.velocity.y * Vector2.down, ForceMode2D.Impulse);
    }
    public void Update(Rigidbody2D rigidbody)
    {
        if (IsJumping && rigidbody.velocity.y < 0f) onFinish();
    }

    public interface IData
    {
        float Force { get; }
        Percentage JumpCut { get; }
    }
}