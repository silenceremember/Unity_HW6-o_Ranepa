using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] private CharacterController playerController;
    [SerializeField] private Collider triggerZone;
    [SerializeField] private String _sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterController>() == playerController)
        {
            SceneManager.LoadScene(_sceneToLoad);
        }
    }
}