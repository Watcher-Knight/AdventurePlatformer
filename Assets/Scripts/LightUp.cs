using UnityEngine;
using ArchitectureLibrary;

[AddComponentMenu("Custom/Light Up")]
public class LightUp : EventListener
{
    private new Renderer renderer;

    private void Start()
    {
        gameObject.TryGetComponent<Renderer>(out renderer);
    }


    public void Select(bool value = true)
    {
        if (renderer != null)
        {
            if (value)
            {
                renderer.material.color = Color.red;
            }
            else
            {
                renderer.material.color = Color.white;
            }
        }
    }

    public override void Invoke()
    {
        renderer.material.color = Color.red;
    }
    public override void Cancel()
    {
        renderer.material.color = Color.white;
    }
}