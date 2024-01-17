using UnityEngine;
using ArchitectureLibrary;

public class StateMachineAction : StateMachineBehaviour
{
    [SerializeField] EventTag tag;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Events.SendMessage(animator, tag);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Events.SendMessage(animator, tag, true);
    }
}