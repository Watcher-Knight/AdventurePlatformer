using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ArchitectureLibrary;

public class ObjectTargeter
{
    public readonly float RaySpacing = 3f;
    public Collider2D TargetCollider;

    public void Update(IData data, Vector2 position, Vector2 direction, LayerMask layer)
    {
        IEnumerable<Collider2D> colliders = new Collider2D[0];
        if (direction != Vector2.zero)
        {
            if (data.Angle > 0)
            {
                colliders = GetCollidersInCast(data, position, direction, layer);
            }
            else
            {
                RaycastHit2D[] raycasts = Physics2D.RaycastAll(position, direction, data.Distance, layer);
                colliders = raycasts.Select(cast => cast.collider);
            }
        }

        Collider2D closestCollider = GetClosestCollider(colliders, position, direction);

        if (TargetCollider != closestCollider)
        {
            TargetCollider = closestCollider;
        }

        if (Input.GetKeyDown(KeyCode.D)) Debug.Log(TargetCollider);
    }

    private Collider2D[] GetCollidersInCast(IData data, Vector2 position, Vector2 direction, LayerMask layer)
    {
        IEnumerable<Collider2D> colliders = new Collider2D[0];

        float rotation = Calculator.ToRotation(direction);
        float spacing = (RaySpacing > 0.1f) ? RaySpacing : 0.1f;
        int rayAmount = (int)Math.Ceiling(data.Angle / spacing) + 1;

        for (float i = -0.5f; i <= 0.5f; i += 1f / rayAmount)
        {
            float currentRotation = rotation - (i * data.Angle);
            Vector2 currentDirection = Calculator.ToDirection(currentRotation);

            RaycastHit2D[] raycasts = Physics2D.RaycastAll(position, currentDirection, data.Distance, layer);
            if (data.Debug) Debug.DrawRay(position, currentDirection * data.Distance, Color.red);

            IEnumerable<Collider2D> currentColliders = raycasts.Select(cast => cast.collider);
            colliders = colliders.Union(currentColliders);
        }

        return colliders.ToArray();
    }

    private Collider2D GetClosestCollider(IEnumerable<Collider2D> colliders, Vector2 position, Vector2 direction)
    {
        float smallestDistance = Mathf.Infinity;
        Collider2D closestCollider = null;
        foreach (Collider2D collider in colliders)
        {
            float distance = Calculator.GetDistance(position, collider.transform.position);
            Vector2 targetPoint = position;
            targetPoint += direction * distance;

            if (Calculator.GetDistance(collider.transform.position, targetPoint) < smallestDistance)
            {
                closestCollider = collider;
                smallestDistance = Calculator.GetDistance(collider.transform.position, targetPoint);
            }
        }
        return closestCollider;
    }

    public interface IData
    {
        float Distance { get; }
        float Angle { get; }
        bool Debug { get; }
    }
}