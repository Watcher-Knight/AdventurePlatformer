using UnityEditor;
using UnityEngine;
using ArchitectureLibrary;

//[CustomPropertyDrawer(typeof(TestMono))]
public class Test2Drawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        object targetObject = Properties.GetObject<int>(property);

        EditorGUI.LabelField(position, targetObject.ToString());
    }
}