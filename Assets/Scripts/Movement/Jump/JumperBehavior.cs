using System;
using ArchitectureLibrary;
using UnityEngine;

[AddComponentMenu(ComponentPaths.JumperBehavior)]
public class JumperBehavior : MonoBehaviour
{
    [SerializeField][AutoAssign] JumperData Data;
    [SerializeField][AutoAssign] Rigidbody2D Rigidbody;

    private readonly Jumper Jumper = new();

    public Action OnFinish { get => Jumper.OnFinish; set => Jumper.OnFinish = value; }

    private void Update()
    {
        Jumper.Update(Rigidbody);
    }
    
    public void Jump() => Jumper.Jump(Data, Rigidbody);
    public void Cancel() => Jumper.Cancel(Data, Rigidbody);
}