using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    public void LoadLevelByName()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}