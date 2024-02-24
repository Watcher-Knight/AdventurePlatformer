using ArchitectureLibrary;
using UnityEngine;

[AddComponentMenu(ComponentPaths.PlayerController)]
public class PlayerController : MonoBehaviour
{
    [SerializeField][AutoAssign] private PlayerMovement Movement;
    [SerializeField][AutoAssign] private PlayerControllerData Data;

    public InputBuffer JumpBuffer = new();
    public InputBuffer GrappleBuffer = new();
    private GameInput GameInput;

    private void Start()
    {
        GameInput ??= new();
        GameInput.Player.Enable();

        JumpBuffer.Action = Movement.Jump;
        GrappleBuffer.Action = Movement.Grapple;
        GameInput.Player.Move.performed += c => Movement.Move(c.ReadValue<float>());
        GameInput.Player.Move.canceled += _ => Movement.Move(0);
        GameInput.Player.Target.performed += c => Movement.Aim(c.ReadValue<Vector2>());
        GameInput.Player.Target.canceled += _ => Movement.Aim(Vector2.zero);
        GameInput.Player.Jump.performed += _ => JumpBuffer.Invoke(Data.JumpBufferTime);
        GameInput.Player.Jump.canceled += _ => Movement.CancelJump();
        GameInput.Player.Grapple.performed += _ => GrappleBuffer.Invoke(Data.GrappleBufferTime);
        GameInput.Player.Grapple.canceled += _ => Movement.CancelGrapple();
    }

    private void Update()
    {
        JumpBuffer.Update(Time.deltaTime);
        GrappleBuffer.Update(Time.deltaTime);
    }
}