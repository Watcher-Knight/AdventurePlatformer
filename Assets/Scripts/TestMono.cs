using ArchitectureLibrary;
using UnityEngine;

[AddComponentMenu("Test")]
public class TestMono : MonoBehaviour
{
    [SerializeField] private PercentReference value;

    [ContextMenu("Test")]
    public void Test()
    {
        Debug.Log(name);
    }
}