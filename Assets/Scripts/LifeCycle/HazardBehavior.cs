using UnityEngine;
using ArchitectureLibrary;

[AddComponentMenu(ComponentPaths.HazardBehavior)]
public class HazardBehavior : MonoBehaviour
{
    [SerializeField] private EventTag EventTag;

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.SendMessage(EventTag);
    }
}