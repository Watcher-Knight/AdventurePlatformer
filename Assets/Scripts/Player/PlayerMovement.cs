using UnityEngine;
using ArchitectureLibrary;

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

    private GameInput Controls;
    // String References: PlayerMovenentEditor
    private MovementStateMachine StateMachine;

    private readonly Mover Mover = new();
    private readonly Jumper Jumper = new();
    private readonly Grappler Grappler = new();
    private readonly ObjectTargeter Targeter = new();

    private void OnEnable()
    {
        InitializeControls();
        StateMachine = new(Jumper, Grappler);
    }

    private void InitializeControls()
    {
        Controls ??= new();
        Controls.Player.Enable();

        Controls.Player.Jump.performed += _ => Jump();
        Controls.Player.Jump.canceled += _ => CancelJump();
        Controls.Player.Grapple.performed += _ => Grapple();
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
    }
    private void Jump()
    {
        if (StateMachine.ToJump())
            Jumper.Jump(JumperData, Rigidbody);
    }
    private void CancelJump()
    {
        Jumper.Cancel(JumperData, Rigidbody);
    }
    private void Grapple()
    {
        if (StateMachine.ToGrapple(Targeter.TargetCollider))
            Grappler.Grapple(Rigidbody);
    }
    private void CancelGrapple()
    {
        Grappler.Cancel(Collider, GrapplerData);
    }
}
