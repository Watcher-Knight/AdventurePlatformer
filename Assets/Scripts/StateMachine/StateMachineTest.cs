using UnityEngine;
using ArchitectureLibrary;

public class StateMachineTest : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        Debug.Log("Enter");
    }
}