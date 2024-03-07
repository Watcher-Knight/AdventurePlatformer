using ArchitectureLibrary;
using UnityEngine;

[CreateAssetMenu(fileName = "JumperData", menuName = CreateAssetPaths.JumperData, order = 0)]
public class JumperData : ScriptableObject, Jumper.IData
{
    [field: SerializeField] public float Force { get; private set; } = 30f;
    [field: SerializeField] public Percentage JumpCut { get; private set; } = 0.75f;
    //[field: SerializeField] public float AnimatorFallingVelocity { get; private set; } = -10f;
}