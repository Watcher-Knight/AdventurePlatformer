using System;
using ArchitectureLibrary;
using UnityEngine;

[AddComponentMenu(ComponentPaths.JumperBehavior)]
public class JumperBehavior : MonoBehaviour
{
    [SerializeField][AutoAssign] JumperData Data;
    [SerializeField][AutoAssign] Rigidbody2D Rigidbody;
    [SerializeField] private AnimatorBoolParameter JumpingParameter;
    [SerializeField] private AnimatorBoolParameter FallingParameter;

    private readonly Jumper Jumper = new();

    public Action OnFinish { get => Jumper.OnFinish; set => Jumper.OnFinish = value; }

    private void Update()
    {
        Jumper.Update(Rigidbody);
        UpdateAnimator();
    }
    private void UpdateAnimator()
    {
        if (JumpingParameter.Value != Jumper.IsJumping) JumpingParameter.Value = Jumper.IsJumping;
        if (FallingParameter.Value != (Rigidbody.velocity.y < -0.1f))
            FallingParameter.Value = Rigidbody.velocity.y < -0.1f;
    }
    
    public void Jump() => Jumper.Jump(Data, Rigidbody);
    public void Cancel() => Jumper.Cancel(Data, Rigidbody);
}