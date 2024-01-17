using UnityEngine;
using UnityEngine.SceneManagement;
using ArchitectureLibrary;

[AddComponentMenu("Custom/Scene Loader")]
public class SceneLoader : MonoBehaviour, IInvokeable
{
    [SerializeField] private bool loadCurrentScene = false;
    [SerializeField][DrawIf("loadCurrentScene", false)] private string scene;

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