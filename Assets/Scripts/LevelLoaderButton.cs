using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private string _sceneToLoad;

    public void LoadLevelByName()
    {
        SceneManager.LoadScene(_sceneToLoad);
    }
}