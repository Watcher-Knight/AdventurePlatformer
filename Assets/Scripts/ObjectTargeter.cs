using System;
using System.Collections.Generic;
using ArchitectureLibrary;
using UnityEngine;

[AddComponentMenu("Custom/Object Targeter")]
public class ObjectTargeter : MonoBehaviour, IAxisControllable2D, IActivateable
{
    [SerializeField] private bool raySpread = true;
    [SerializeField] private VariableReference<float> distance = 30f;
    [SerializeField][DrawIf("raySpread")] private VariableReference<float> angle = 30f;
    [SerializeField][DrawIf("raySpread")] private VariableReference<float> raySpacing = 3f;
    [SerializeField] private new Tag tag;
    [SerializeField] private EventTag eventTag;
    [SerializeField] private bool debug = false;

    public new Collider2D collider { get; private set; }
    public Collider2D[] colliders { get; private set; }
    private Vector2 _direction;
    private Vector2 direction
    {
        get
        {
            if (isActive) return _direction;
            return Vector2.zero;
        }
    }
    public Action<Collider2D> onTargetChange;
    public bool isActive { get; set; } = true;

    public void Control(Vector2 direction) => _direction = direction;

    private void Update()
    {
        List<Collider2D> colliders = new();
        if (direction != Vector2.zero)
        {
            if (raySpread && angle > 0)
            {
                float rotation = Calculator.DirectionToRotation(direction);

                float spacing = (raySpacing > 0.1f) ? raySpacing : 0.1f;
                int rayAmount = (int)Math.Ceiling(angle / spacing) + 1;

                for (float i = -0.5f; i <= 0.5f; i += 1f / rayAmount)
                {
                    float currentRotation = rotation - (i * angle.value);
                    Vector2 currentDirection = Calculator.RotationToDirection(currentRotation);

                    Cast cast = new Raycast(transform.position, currentDirection, distance, tag, debug);

                    foreach (Collider2D collider in cast)
                    {
                        if (!colliders.Contains(collider)) colliders.Add(collider);
                    }
                }
            }
            else
            {
                Cast cast = new Raycast(transform.position, direction, distance, tag, debug);
                colliders = new List<Collider2D>(cast);
            }
        }

        this.colliders = colliders.ToArray();

        float smallestDistance = Mathf.Infinity;
        Collider2D closestCollider = null;
        foreach (Collider2D collider in colliders)
        {
            float distance = Calculator.GetDistance(transform.position, collider.transform.position);
            Vector2 targetPoint = transform.position;
            targetPoint += direction * distance;

            if (Calculator.GetDistance(collider.transform.position, targetPoint) < smallestDistance)
            {
                closestCollider = collider;
                smallestDistance = Calculator.GetDistance(collider.transform.position, targetPoint);
            }
        }

        if (collider != closestCollider)
        {
            Events.SendMessage(collider, eventTag, true);
            collider = closestCollider;
            Events.SendMessage(collider, eventTag);
            onTargetChange(collider);
        }
    }
}
public delegate void Action<T>(T value);
