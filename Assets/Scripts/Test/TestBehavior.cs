using ArchitectureLibrary;
using UnityEngine;

[AddComponentMenu("Test")]
public class TestBehavior : MonoBehaviour
{
    [SerializeField] private AnimatorBoolParameter Parameter;
    [SerializeField] private AnimatorIntParameter IntParameter;

    [Button] private void TestBool(bool value)
    {
        Parameter.SetValue(value);
    }
    [Button] private void TestInt(int value)
    {
        IntParameter.SetValue(value);
    }
}