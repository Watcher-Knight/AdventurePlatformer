using UnityEngine;
using ArchitectureLibrary;

[CreateAssetMenu(fileName = "GrapplerData", menuName = CreateAssetPaths.GrapplerData, order = 0)]
public class GrapplerData : ScriptableObject, Grappler.IData
{
    [field: SerializeField] public float Speed { get; private set; } = 30;
    [field: SerializeField] public Percentage Acceleration { get; private set; } = 0.75f;
    [field: SerializeField] public Percentage Slowdown { get; private set; } = 0.75f;
    [field: SerializeField] public float ControlReturnTime { get; private set; } = 0.5f;
    [field: SerializeField] public EventTag SelectionTag { get; private set; }
    [field: SerializeField] public EventTag DeselectionTag { get; private set; }
}