using ArchitectureLibrary;
using UnityEngine;

[AddComponentMenu(ComponentPaths.PlayerLifeCycleBehavior)]
public class PlayerLifeCycleBehavior : MonoBehaviour, IEventListener, IEventListener<Vector2>
{
    [SerializeField] EventTag HazardEventTag;
    [SerializeField] EventTag RespawnPointEventTag;
    [SerializeField] EventTag ResetTag;
    [SerializeField][AutoAssign] Transform Transform;

    private Vector2 RespawnPoint = Vector2.zero;

    private void Awake()
    {
        RespawnPoint = Transform.position;
    }

    public void Invoke(EventTag tag)
    {
        if (tag == HazardEventTag) Die();
    }
    public void Invoke(EventTag tag, Vector2 value)
    {
        if (tag == RespawnPointEventTag) RespawnPoint = value;
    }

    private void Die()
    {
        Transform.position = RespawnPoint;
        Transform.SendMessage(ResetTag);
    }
}