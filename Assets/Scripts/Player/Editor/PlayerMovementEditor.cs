using UnityEditor;
using ArchitectureLibrary;

[CustomEditor(typeof(PlayerMovement))]
public class PlayerMovementEditor : CustomInspector
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MovementStateMachine stateMachine = GetField<MovementStateMachine>("StateMachine");

        if (stateMachine != null)
        {
            MovementStateMachine.State state = stateMachine.CurrentState;
            EditorGUILayout.LabelField("CurrentState", state.ToString());
        }
    }
}