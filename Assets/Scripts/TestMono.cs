using ArchitectureLibrary;
using UnityEngine;

[AddComponentMenu("Test")]
public class TestMono : MonoBehaviour
{
    [SerializeField] private int number;
    [SerializeField] private EventTag test;
    [SerializeField] private string text;

    public interface IData
    {
        int Number { get; }
        EventTag EventTag { get; }
        string Text { get; }
    }
}

[CreateAssetMenu(fileName = "TestMono", menuName = "AdventurePlatformer/TestMono", order = 0)]
public class TestMonoData : ScriptableObject, TestMono.IData
{
    [field: SerializeField] public int Number { get; set; }
    public EventTag EventTag { get; }
    public string Text { get; }
}