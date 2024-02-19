using UnityEngine;
using ArchitectureLibrary;

[UpdateEditor]
[AddComponentMenu(ComponentPaths.PlayerMovement)]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField][AutoAssign] private MoverData MoverData;
    [SerializeField][AutoAssign] private JumperData JumperData;
    [SerializeField][AutoAssign] private GrapplerData GrapplerData;
    [SerializeField][AutoAssign] private ObjectTargeterData TargeterData;
    [SerializeField][AutoAssign] private Rigidbody2D Rigidbody;
    [SerializeField][AutoAssign] private BoxCollider2D Collider;
    [SerializeField] private LayerMask PlatformLayer;
    [SerializeField] private LayerMask GrapplePointLayer;
    [SerializeField] private float JumpBufferTime = 0.2f;
    [SerializeField] private float GrappleBufferTime = 0.2f;
    [DisplayPlayMode] private MovementStateMachine.State CurrentState => StateMachine.CurrentState;

    private GameInput Controls;
    private MovementStateMachine StateMachine;

    private readonly Mover Mover = new();
    private readonly Jumper Jumper = new();
    private readonly Grappler Grappler = new();
    private readonly ObjectTargeter Targeter = new();

    private readonly InputBuffer JumpBuffer = new();
    private readonly InputBuffer GrappleBuffer = new();

    private void OnEnable()
    {
        InitializeControls();
        StateMachine = new(Jumper, Grappler);
    }

    private void InitializeControls()
    {
        Controls ??= new();
        Controls.Player.Enable();

        JumpBuffer.Action = Jump;
        GrappleBuffer.Action = Grapple;
        Controls.Player.Jump.performed += _ => JumpBuffer.Invoke(JumpBufferTime);
        Controls.Player.Grapple.performed += _ => GrappleBuffer.Invoke(GrappleBufferTime);
        Controls.Player.Jump.canceled += _ => CancelJump();
        Controls.Player.Grapple.canceled += _ => CancelGrapple();
    }

    private void FixedUpdate()
    {
        float moverControls = Controls.Player.Move.ReadValue<float>();

        StateMachine.Update(Rigidbody, Collider, PlatformLayer);
        Mover.Update(MoverData, Rigidbody, moverControls, Time.fixedDeltaTime);
        Jumper.Update(Rigidbody);
        Grappler.Update(GrapplerData, Targeter.TargetCollider, Collider, Mover, Time.fixedDeltaTime);
    }
    private void Update()
    {
        Vector2 targeterControls = Controls.Player.Target.ReadValue<Vector2>();

        Targeter.Update(TargeterData, transform.position, targeterControls, GrapplePointLayer);
        JumpBuffer.Update(Time.deltaTime);
        GrappleBuffer.Update(Time.deltaTime);
    }
    private bool Jump()
    {
        if (StateMachine.ToJump())
        {
            Jumper.Jump(JumperData, Rigidbody);
            return true;
        }
        return false;
    }
    private void CancelJump()
    {
        Jumper.Cancel(JumperData, Rigidbody);
    }
    private bool Grapple()
    {
        if (StateMachine.ToGrapple(Grappler, Collider))
        {
            Grappler.Grapple(Collider);
            return true;
        }
        return false; 
    }
    private void CancelGrapple()
    {
        Grappler.Cancel(Collider, GrapplerData);
    }
}
