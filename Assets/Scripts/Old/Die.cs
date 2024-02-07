using UnityEngine;
using TMPro;

//[AddComponentMenu("Custom/Die")]
public class Die
{
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private string deathMessage = "You Died";

    private bool isDead = false;
    public bool conditionValue => isDead;

    public void Invoke()
    {
        isDead = true;
        Time.timeScale = 0;
        textMesh.text = deathMessage;
    }
}