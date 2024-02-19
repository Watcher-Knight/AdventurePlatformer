using System.Collections.Generic;
using ArchitectureLibrary;
using UnityEngine;

[AddComponentMenu("Test")]
public class TestMono : MonoBehaviour
{
    [SerializeField] private int Number;
    [SerializeField] private EventTag Test;
    [SerializeField] private string Text;
    [Display] private int NumberPlus2 => Number + 5;

    public interface IData
    {
        int Number { get; }
        EventTag EventTag { get; }
        string Text { get; }
    }
}