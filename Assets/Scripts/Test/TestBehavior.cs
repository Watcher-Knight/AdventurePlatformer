using ArchitectureLibrary;
using UnityEngine;

[AddComponentMenu("Test")]
public class TestBehavior : MonoBehaviour
{
    [SerializeField] private Animator Animator;

    [Button] private void Test()
    {
        Animator.Play("Entry", 0);
    }
}