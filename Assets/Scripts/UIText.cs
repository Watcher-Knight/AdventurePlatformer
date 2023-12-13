using UnityEngine;
using TMPro;
using ArchitectureLibrary;

public class UIText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void Display(Variable message) => text.text = message.ToString();
}
