using System.Collections.Generic;
using UnityEngine;
using ArchitectureLibrary;

// [RequireComponent(typeof(Rigidbody2D))]
public class Follower : MonoBehaviour
{
    [SerializeField] private FloatVariable moveSpeed;
    [SerializeField] private Vector3Variable playerPosition;
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPosition.value, moveSpeed.value * Time.deltaTime);
    }
}
