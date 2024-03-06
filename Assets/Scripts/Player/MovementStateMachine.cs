using System;
using UnityEngine;

public class MovementStateMachine
{
    public enum State
    {
        Default,
        Fall,
        Jump,
        Grapple
    }

    public Action<State> onStateChange;

    public MovementStateMachine(JumperBehavior jumper, GrapplerBehavior grappler)
    {
        jumper.OnFinish += () => ToFall();
        grappler.OnFinish += () => ToFall();
    }

    public State CurrentState { get; private set; } = State.Default;

    private bool CanAirJump = true;
    private bool IsGrounded(BoxCollider2D collider, LayerMask platformLayer) =>
        Collisions.IsTouching(collider, ArchitectureLibrary.Direction.Down, platformLayer);

    private void ToDefault()
    {
        switch (CurrentState)
        {
            case State.Fall:
                CurrentState = State.Default;
                CanAirJump = true;
                break;
        }
    }
    private void ToFall()
    {
        switch (CurrentState)
        {
            case State.Default:
            case State.Jump:
            case State.Grapple:
                CurrentState = State.Fall;
                break;
        }
    }
    public bool ToJump()
    {
        switch (CurrentState)
        {
            case State.Default:
                CurrentState = State.Jump;
                return true;

            case State.Fall:
                if (CanAirJump)
                {
                    CurrentState = State.Jump;
                    CanAirJump = false;
                    return true;
                }
                break;
        }
        return false;
    }
    public bool ToGrapple(GrapplerBehavior grappler, Collider2D collider)
    {
        if (grappler.CanGrapple)
        {
            switch (CurrentState)
            {
                case State.Default:
                case State.Fall:
                case State.Jump:
                    CurrentState = State.Grapple;
                    CanAirJump = true;
                    return true;
            }
        }
        return false;
    }

    public void Update(Rigidbody2D rigidbody, BoxCollider2D collider, LayerMask layerMask)
    {
        switch (CurrentState)
        {
            case State.Fall:
                if (IsGrounded(collider, layerMask)) ToDefault();
                break;
            case State.Default:
                if (!IsGrounded(collider, layerMask)) ToFall();
                break;
        }
    }

    public void Reset()
    {
        CurrentState = State.Default;
        CanAirJump = true;
    }
}