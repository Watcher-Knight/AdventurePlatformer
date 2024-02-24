using UnityEngine;
using ArchitectureLibrary;

[UpdateEditor]
[AddComponentMenu(ComponentPaths.PlayerMovement)]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField][AutoAssign] private Rigidbody2D Rigidbody;
    [SerializeField][AutoAssign] private BoxCollider2D Collider;
    [SerializeField][AutoAssign] private PlayerDataChannel DataChannel;
    [SerializeField][AutoAssign] private MoverBehavior Mover;
    [SerializeField][AutoAssign] private GrapplerBehavior Grappler;
    [SerializeField][AutoAssign] private JumperBehavior Jumper;
    [SerializeField] private LayerMask PlatformLayer;
    [DisplayPlayMode] private MovementStateMachine.State CurrentState => StateMachine.CurrentState;

    private MovementStateMachine StateMachine;

    private void OnEnable()
    {
        StateMachine = new(Jumper, Grappler);
    }

    private void FixedUpdate()
    {
        StateMachine.Update(Rigidbody, Collider, PlatformLayer);
    }
    private void Update()
    {
        DataChannel.WriteTo(transform.position, CurrentState);
    }
    public void Move(float direction) => Mover.Move(direction);
    public bool Jump()
    {
        if (StateMachine.ToJump())
        {
            Jumper.Jump();
            return true;
        }
        return false;
    }
    public void CancelJump()
    {
        Jumper.Cancel();
    }
    public bool Grapple()
    {
        if (StateMachine.ToGrapple(Grappler, Collider))
        {
            Grappler.Grapple();
            return true;
        }
        return false; 
    }
    public void CancelGrapple()
    {
        Grappler.Cancel();
    }
    public void Aim(Vector2 direction)
    {
        Grappler.Target(direction);
    }
}
