using ArchitectureLibrary;
using UnityEngine;
using UnityEditor;
using System.IO;

[AddComponentMenu("Test")]
public class TestMono : MonoBehaviour
{
    [SerializeField] private Vector2 direction = Vector2.zero;
    [ContextMenu("Test")]
    private void Test()
    {
        Debug.Log(Converter.GetDirection(direction));
    }
}

public class Test
{
    [MenuItem("Test/TestLog")]
    static void MyTest()
    {
        Debug.Log(SceneView.lastActiveSceneView.pivot);
    }
}