using UnityEngine;
using System.Collections;
using StarterAssets;

public class LevelCompleteTrigger : MonoBehaviour
{
    [Header("Player References")]
    [SerializeField] private CharacterController _playerController;
    
    [Header("Level Complete Settings")]
    [SerializeField] private GameObject[] _objectsToToggle;
    [SerializeField] private float _menuDelay = 2f;
    [SerializeField] private PauseManager _pauseManager;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other == _playerController)
        {
            foreach (var obj in _objectsToToggle)
            {
                if (obj != null)
                {
                    obj.SetActive(!obj.activeSelf);
                }
            }

            StartCoroutine(ShowVictoryMenu());
        }
    }

    private IEnumerator ShowVictoryMenu()
    {
        yield return new WaitForSeconds(_menuDelay);
        if (_pauseManager != null)
        {
            _pauseManager.OnVictory();
        }
    }
}