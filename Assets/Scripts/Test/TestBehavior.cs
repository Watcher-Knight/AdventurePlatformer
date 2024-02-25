using ArchitectureLibrary;
using UnityEngine;

[AddComponentMenu("Test")]
public class TestBehavior : MonoBehaviour
{
    //[SerializeField][AutoAssign] Collider2D Collider;

    [Button] private void Test(Collider2D collider)
    {
        Debug.Log(collider.bounds.center - new Vector3(0, collider.bounds.extents.y, 0));
    }
}