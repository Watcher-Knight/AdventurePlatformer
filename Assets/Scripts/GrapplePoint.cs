using UnityEngine;
using ArchitectureLibrary;
using UnityEngine.Serialization;

[AddComponentMenu(ComponentPaths.GrapplePoint)]
public class GrapplePoint : MonoBehaviour, IEventListener
{
    [SerializeField][FormerlySerializedAs("_renderer")][AutoAssign] private Renderer Renderer;
    [SerializeField][FormerlySerializedAs("selectionTag")] private EventTag SelectionTag;
    [SerializeField][FormerlySerializedAs("deselectionTag")] private EventTag DeselectionTag;

    public void Invoke(EventTag tag)
    {
        if (tag == SelectionTag)Renderer.material.color = Color.red;
        if (tag == DeselectionTag)Renderer.material.color = Color.white;
    }
}