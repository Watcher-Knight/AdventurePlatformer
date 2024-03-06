using System;
using ArchitectureLibrary;
using UnityEngine;

[AddComponentMenu(ComponentPaths.MoverBehavior)]
public class MoverBehavior : MonoBehaviour
{
    [SerializeField][AutoAssign] private MoverData Data;
    [SerializeField][AutoAssign] private Rigidbody2D Rigidbody;
    [SerializeField][AutoAssign] private Transform RendererTransform;
    [SerializeField] private AnimatorBoolParameter parameter;
    [Display] private float Direction { get; set; } = 0f;
    [NonSerialized] public Percentage ControlLevel = 1f;
    private readonly Mover Mover = new();

    public void Move(float direction)
    {
        Direction = direction;
    }
    private void FixedUpdate()
    {
        Mover.Update(Data, Rigidbody, Direction, ControlLevel, Time.fixedDeltaTime);
    }
    private void Update()
    {
        UpdateAnimator();
        if (Direction * RendererTransform.localScale.x < 0) RendererTransform.Flip();
    }
    private void UpdateAnimator()
    {
        bool value = Direction != 0 && Rigidbody.velocity.x != 0;
        parameter.SetValue(value);
    }
}