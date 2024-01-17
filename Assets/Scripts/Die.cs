using UnityEngine;
using TMPro;
using ArchitectureLibrary;

[AddComponentMenu("Custom/Die")]
public class Die : EventListener, ICondition
{
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private VariableReference<string> deathMessage = "You Died";

    private bool isDead = false;
    public bool conditionValue => isDead;

    public override void Invoke()
    {
        isDead = true;
        Time.timeScale = 0;
        textMesh.text = deathMessage;
    }
}