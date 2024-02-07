using UnityEngine;

//[AddComponentMenu("Custom/Light Up")]
public class LightUp : MonoBehaviour
{
    private new Renderer renderer;

    private void Start()
    {
        gameObject.TryGetComponent(out renderer);
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

    public void Invoke()
    {
        renderer.material.color = Color.red;
    }
    public void Cancel()
    {
        renderer.material.color = Color.white;
    }
}