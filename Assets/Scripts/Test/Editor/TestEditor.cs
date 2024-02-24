using UnityEditor;
using UnityEngine;
using ArchitectureLibrary;

//[CustomEditor(typeof(TestMono))]
public class TestDrawer : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();
    }
}