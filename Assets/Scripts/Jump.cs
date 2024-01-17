using ArchitectureLibrary;
using UnityEngine;

[AddComponentMenu("Custom/Jump")]
public class Jump : MonoBehaviour, IInvokeable, IResetable
{
    [Header("Stats")]
    [Space(8)]
    [SerializeField] private VariableReference<float> force = 30;
    [SerializeField] private PercentReference jumpCut;
    [SerializeField] private new Rigidbody2D rigidbody;

    [Space(4)]

    [Header("Floor Checking")]
    [Space(8)]
    [SerializeField] private bool useFloorChecking = true;
    [SerializeField][DrawIf("useFloorChecking")] private VariableReference<int> totalAirJumps = 0;
    [SerializeField][DrawIf("useFloorChecking")] private Tag floorTag;
    [SerializeField][DrawIf("useFloorChecking")] private new Collider2D collider;
    [SerializeField][DrawIf("useFloorChecking")] private VariableReference<float> inputBuffer = 0.1f;


    private bool isJumping = false;
    private int airJumps = 0;
    private float inputInitTime = 0;

    private bool canJump
    {
        get
        {
            if (useFloorChecking)
            {
                return isGrounded || airJumps > 0;
            }
            return true;
        }
    }
    private bool isGrounded
    {
        get
        {
            if (collider != null)
            {
                if (Collisions.IsTouching(collider, floorTag, Direction.down)) return true;
            }
            return false;
        }
    }

    public void Invoke()
    {
        inputInitTime = inputBuffer + Time.deltaTime;
    }
    public void Cancel()
    {
        inputInitTime = 0;

        if (isJumping)
        {
            rigidbody.AddForce(Vector2.down * rigidbody.velocity.y * jumpCut, ForceMode2D.Impulse);
            isJumping = false;
        }
    }

    public void Reset()
    {
        airJumps = totalAirJumps;
    }

    public void StartJump()
    {
        rigidbody.velocity = Vector2.right * rigidbody.velocity.x;
        rigidbody.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        if (!isGrounded) airJumps -= 1;
        isJumping = true;
    }

    private void Update()
    {
        if (isGrounded && airJumps < totalAirJumps) Reset();

        if (isJumping && rigidbody.velocity.y <= 0) isJumping = false;

        if (inputInitTime > 0)
        {
            inputInitTime -= Time.deltaTime;

            if (canJump)
            {
                StartJump();
                inputInitTime = 0;
            }
        }
    }
}