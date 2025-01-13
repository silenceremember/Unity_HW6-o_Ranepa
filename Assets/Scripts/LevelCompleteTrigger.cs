using UnityEngine;
using System.Collections;
using StarterAssets;

public class LevelCompleteTrigger : MonoBehaviour
{
    [Header("Player References")]
    [SerializeField] private CharacterController playerController;
    
    [Header("Level Complete Settings")]
    [SerializeField] private GameObject[] objectsToToggle;
    [SerializeField] private float menuDelay = 2f;
    [SerializeField] private PauseManager pauseManager;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other == playerController)
        {
            // Инвертируем состояние всех объектов
            foreach (var obj in objectsToToggle)
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
        yield return new WaitForSeconds(menuDelay);
        if (pauseManager != null)
        {
            pauseManager.OnVictory();
        }
    }
}