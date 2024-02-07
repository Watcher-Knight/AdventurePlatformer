using UnityEngine;
using UnityEngine.SceneManagement;

//[AddComponentMenu("Custom/Scene Loader")]
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private bool loadCurrentScene = false;
    [SerializeField] private string scene;

    public void Invoke()
    {
        if (loadCurrentScene)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            SceneManager.LoadScene(scene);
        }
    }
}