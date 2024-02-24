using System;
using ArchitectureLibrary;
using UnityEngine;

[AddComponentMenu(ComponentPaths.GrapplerBehavior)]
public class GrapplerBehavior : MonoBehaviour
{
    [SerializeField][AutoAssign] private ObjectTargeterData TargeterData;
    [SerializeField][AutoAssign] private GrapplerData GrapplerData;
    [SerializeField][AutoAssign] private Transform Origin;
    [SerializeField][AutoAssign] private Collider2D Collider;
    [SerializeField][AutoAssign] private MoverBehavior Mover;
    [SerializeField] private LayerMask GrapplePointLayer;

    private readonly ObjectTargeter Targeter = new();
    private readonly Grappler Grappler = new();

    public Action OnFinish { get => Grappler.OnFinish; set => Grappler.OnFinish = value; }

    private Vector2 Direction = Vector2.zero;

    private void Update()
    {
        Targeter.Update(TargeterData, Origin.position, Direction, GrapplePointLayer);
    }
    private void FixedUpdate()
    {
        Grappler.Update(GrapplerData, Targeter.TargetCollider, Collider, Mover, Time.fixedDeltaTime);
    }

    public void Grapple() => Grappler.Grapple(Collider);
    public void Cancel() => Grappler.Cancel(Collider, GrapplerData);

    public void Target(Vector2 direction)
    {
        Direction = direction;
    }

    public bool CanGrapple => Grappler.CanGrapple(Collider);
}