using ArchitectureLibrary;
using UnityEngine;

[AddComponentMenu(ComponentPaths.RespawnPointBehavior)]
public class RespawnPointBehavior : MonoBehaviour
{
    [SerializeField] private EventTag EventTag;
    [SerializeField][AutoAssign] private Collider2D Collider;

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.SendMessage(EventTag, Collider.bounds.BottomCenter());
    }
}