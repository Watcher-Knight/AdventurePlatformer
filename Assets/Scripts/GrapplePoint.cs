using UnityEngine;
using ArchitectureLibrary;

[AddComponentMenu(ComponentPaths.GrapplePoint)]
public class GrapplePoint : MonoBehaviour, IEventListener
{
    [SerializeField][AutoAssign] private Renderer Renderer;
    [SerializeField] private EventTag SelectionTag;
    [SerializeField] private EventTag DeselectionTag;

    public void Invoke(EventTag tag)
    {
        if (tag == SelectionTag)Renderer.material.color = Color.red;
        if (tag == DeselectionTag)Renderer.material.color = Color.white;
    }
}