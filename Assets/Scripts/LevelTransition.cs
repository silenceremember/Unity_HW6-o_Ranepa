using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] private CharacterController _playerController;
    [SerializeField] private Collider _triggerZone;
    [SerializeField] private String _sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterController>() == _playerController)
        {
            SceneManager.LoadScene(_sceneToLoad);
        }
    }
}