using UnityEngine;
using StarterAssets;

public class JumpBooster : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] private float jumpHeightBonus = 0.5f;

    [Header("Player References")]
    [SerializeField] private ThirdPersonController playerController;
    [SerializeField] private CharacterController characterController;

    [Header("Booster Objects")]
    [SerializeField] private Collider triggerCollider;
    [SerializeField] private GameObject visualObject;
    
    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, что в триггер вошел именно наш игрок
        if (other == characterController)
        {
            // Увеличиваем высоту прыжка
            playerController.JumpHeight += jumpHeightBonus;
            
            // Деактивируем объект
            visualObject.SetActive(false);
            
            // Отключаем коллайдер
            triggerCollider.enabled = false;
        }
    }

    private void OnValidate()
    {
        // Автоматически получаем коллайдер, если он не назначен
        if (triggerCollider == null)
        {
            triggerCollider = GetComponent<Collider>();
        }

        // Автоматически получаем визуальный объект, если он не назначен
        if (visualObject == null)
        {
            visualObject = gameObject;
        }
    }
}