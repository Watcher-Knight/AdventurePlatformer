using ArchitectureLibrary;
using UnityEngine;

[AddComponentMenu("Test")]
public class TestMono : MonoBehaviour
{
    [SerializeField] private int Number;
    [SerializeField] private EventTag Test;
    [SerializeField] private string Text;
    [Display] private int NumberPlus2 => Number + 5;
    [Button] private void MyMethod() => Debug.Log("Success!");
    [Button] private void Method2(decimal value) => Debug.Log(value);
    
    public interface IData
    {
        int Number { get; }
        EventTag EventTag { get; }
        string Text { get; }
    }
}