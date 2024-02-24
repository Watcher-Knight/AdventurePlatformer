using ArchitectureLibrary;
using UnityEngine;

[AddComponentMenu(ComponentPaths.PlayerCamera)]
public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private PlayerData PlayerData;
    [SerializeField] private float FollowingDelay = 0.5f;

    private void FixedUpdate()
    {
        float x = transform.position.x + (PlayerData.Position.x - transform.position.x) * Mathf.Pow(Time.fixedDeltaTime, FollowingDelay);
        float y = transform.position.y + (PlayerData.Position.y - transform.position.y) * Mathf.Pow(Time.fixedDeltaTime, FollowingDelay);

        transform.position = new(x, y, -10);
    }
}