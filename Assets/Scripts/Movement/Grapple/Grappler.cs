using System;
using ArchitectureLibrary;
using UnityEngine;

public class Grappler
{
    private Collider2D TargetCollider;
    private bool IsGrappling = false;
    private float GravityScale;
    public bool CanGrapple(Collider2D collider) => !IsGrappling && TargetCollider != null && !TargetCollider.bounds.Intersects(collider.bounds);
    public Action OnFinish;

    public void Update(IData data, Collider2D target, Collider2D collider, MoverBehavior mover, float deltaTime)
    {
        if (CheckTarget(target)) ChangeTarget(target, data);
        if (IsGrappling && TargetCollider != null)
        {
            MoveTowardsTarget(collider, data, deltaTime);
            if (ReachedPoint(collider)) Finish(collider, data);
        }
        ControlMovement(mover, data, deltaTime);
    }

    public void Grapple(Collider2D collider)
    {
        if (CanGrapple(collider))
        {
            Rigidbody2D rigidbody = collider.GetRigidbody();
            IsGrappling = true;
            GravityScale = rigidbody.gravityScale;
            rigidbody.gravityScale = 0;
        }
    }
    public void Cancel(Collider2D collider, IData data) => Finish(collider, data);
    private void Finish(Collider2D collider, IData data)
    {
        if (IsGrappling)
        {
            Rigidbody2D rigidbody = collider.GetRigidbody();
            IsGrappling = false;
            OnFinish?.Invoke();
            rigidbody.AddForce(-rigidbody.velocity * data.Slowdown, ForceMode2D.Impulse);
            rigidbody.gravityScale = GravityScale;
        }
    }

    private void MoveTowardsTarget(Collider2D collider, IData data, float deltaTime)
    {
        Rigidbody2D rigidbody = collider.GetRigidbody();
        Vector2 direction = (TargetCollider.transform.position - collider.transform.position).normalized;
        Vector2 targetSpeed = direction * data.Speed;
        Vector2 speedDifference = targetSpeed - rigidbody.velocity;

        rigidbody.AddForce(speedDifference * data.Acceleration / deltaTime);
    }
    private bool ReachedPoint(Collider2D collider) =>
        TargetCollider.bounds.Intersects(collider.bounds);

    private bool CheckTarget(Collider2D target) =>
        target != TargetCollider && IsGrappling == false;

    private void ChangeTarget(Collider2D target, IData data)
    {
        TargetCollider?.SendMessage(data.DeselectionTag);
        TargetCollider = target;
        TargetCollider?.SendMessage(data.SelectionTag);
    }

    private void ControlMovement(MoverBehavior mover, IData data, float deltaTime)
    {
        if (IsGrappling && !mover.ControlLevel.Min) mover.ControlLevel = 0;
        if (!IsGrappling && !mover.ControlLevel.Max) mover.ControlLevel += deltaTime / data.ControlReturnTime;
    }

    public interface IData
    {
        float Speed { get; }
        Percentage Acceleration { get; }
        Percentage Slowdown { get; }
        float ControlReturnTime { get; }
        EventTag SelectionTag { get; }
        EventTag DeselectionTag { get; }
    }
}