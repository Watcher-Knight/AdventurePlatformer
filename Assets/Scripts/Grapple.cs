using System.Collections;
using ArchitectureLibrary;
using UnityEngine;

[AddComponentMenu("Custom/Grapple")]
public class Grapple : MonoBehaviour, IInvokeable, ICondition
{
    [SerializeField] private ObjectTargeter targeter;
    [SerializeField] private VariableReference<float> speed = 30f;
    [SerializeField] private VariableReference<float> inputBuffer = 0.1f;
    [SerializeField] private PercentReference acceleration;
    [SerializeField] private PercentReference slowdown;
    [SerializeField] private new Collider2D collider;
    [SerializeField] private Move move;
    [SerializeField] private VariableReference<float> controlReturnTime = 1f;
    [SerializeField] private EventTag selectionEventTag;
    [SerializeField] private ObjectList<IResetable> resetableComponents = new();

    private Collider2D targetCollider = null;
    private bool input = false;
    private float inputInitTime = 0;
    private bool isGrappling = false;
    public bool conditionValue => isGrappling;

    public void Invoke()
    {
        inputInitTime = inputBuffer + Time.deltaTime;
        input = true;
    }
    public void Cancel()
    {
        inputInitTime = 0;
        input = false;
    }

    private void OnEnable()
    {
        if (targeter != null) targeter.onTargetChange += OnTargetChange;
    }

    private void OnTargetChange(Collider2D collider)
    {
        if (!isGrappling) Events.SendMessage(targetCollider, selectionEventTag, true);
        targetCollider = collider;
        if (!isGrappling) Events.SendMessage(targetCollider, selectionEventTag);
    }

    private IEnumerator MoveTo(Collider2D collider, Collider2D target)
    {
        Rigidbody2D rigidbody = collider.attachedRigidbody;
        isGrappling = true;
        move.SetControl(0);
        float gravityScale = rigidbody.gravityScale;
        rigidbody.gravityScale = 0;

        while (!Collisions.Overlap(collider, target) && input)
        {
            Vector2 direction = (target.transform.position - rigidbody.transform.position).normalized;
            Vector2 targetSpeed = direction * speed;
            Vector2 speedDifference = targetSpeed - rigidbody.velocity;

            rigidbody.AddForce(speedDifference * acceleration / Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }

        rigidbody.AddForce(-rigidbody.velocity * slowdown, ForceMode2D.Impulse);
        rigidbody.gravityScale = gravityScale;
        Events.SendMessage(target, selectionEventTag, true);
        Events.SendMessage(collider, selectionEventTag);
        isGrappling = false;

        if (!isGrappling) move.SetControl(1, controlReturnTime);
    }

    public void Update()
    {
        if (inputInitTime > 0)
        {
            inputInitTime -= Time.deltaTime;
            if (targetCollider != null && collider != null && !isGrappling)
            {
                inputInitTime = 0;
                foreach (IResetable component in resetableComponents) component.Reset();
                StartCoroutine(MoveTo(collider, targetCollider));
            }
        }
    }
}
