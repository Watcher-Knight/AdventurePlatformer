using UnityEngine;
using ArchitectureLibrary;

[CreateAssetMenu(fileName = "MoverData", menuName = CreateAssetPaths.MoverData, order = 0)]
public class MoverData : ScriptableObject, Mover.IData
{
    [field: SerializeField] public float Speed { get; private set; } = 10;
    [field: SerializeField] public Percentage Acceleration { get; private set; } = 0.75f;
    [field: SerializeField] public Percentage Deceleration { get; private set; } = 0.75f;
}