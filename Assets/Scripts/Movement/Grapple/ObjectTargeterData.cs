using UnityEngine;

[CreateAssetMenu(fileName = "ObjectTargeterData", menuName = CreateAssetPaths.ObjectTargeterData, order = 0)]
public class ObjectTargeterData : ScriptableObject, ObjectTargeter.IData
{
    [field: SerializeField] public float Distance { get; private set; } = 30f;
    [field: SerializeField] public float Angle { get; private set; } = 30f;
    [field: SerializeField] public bool Debug { get; private set; } = false;
}