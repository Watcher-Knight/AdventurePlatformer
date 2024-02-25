using System;
using UnityEngine;

public class MovementStateMachine
{
    public enum State
    {
        Idle,
        Walk,
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

    public State CurrentState { get; private set; } = State.Idle;

    private bool CanAirJump = true;
    private bool IsGrounded(BoxCollider2D collider, LayerMask platformLayer) =>
        Collisions.IsTouching(collider, ArchitectureLibrary.Direction.Down, platformLayer);
    private bool IsMoving(Rigidbody2D rigidbody) =>
        rigidbody.velocity.x > 0.05f || rigidbody.velocity.x < -0.05f;

    private void ToIdle(Rigidbody2D rigidbody)
    {
        switch (CurrentState)
        {
            case State.Fall:
                CurrentState = State.Idle;
                CanAirJump = true;
                break;
            case State.Walk:
                if (!IsMoving(rigidbody))
                {
                    CurrentState = State.Idle;
                    CanAirJump = true;
                }
                break;
        }
    }
    private void ToWalk()
    {
        switch (CurrentState)
        {
            case State.Idle:
                CurrentState = State.Walk;
                break;
        }
    }
    private void ToFall()
    {
        switch (CurrentState)
        {
            case State.Walk:
            case State.Idle:
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
            case State.Idle:
            case State.Walk:
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
                case State.Idle:
                case State.Walk:
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
        if (IsGrounded(collider, layerMask)) ToIdle(rigidbody);
        if (IsMoving(rigidbody)) ToWalk();
    }

    public void Reset() => CurrentState = State.Idle;
}