using UnityEditor;
using UnityEngine;
using ArchitectureLibrary;

[CustomPropertyDrawer(typeof(Test))]
public class TestDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        property.FindPropertyRelative("Value").intValue = EditorGUILayout.IntField(label, property.FindPropertyRelative("Value").intValue);
    }
}