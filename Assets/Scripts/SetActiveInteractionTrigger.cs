using UnityEngine;
using StarterAssets;
using System.Collections;

public class ObjectInteractionTrigger : MonoBehaviour
{
    [SerializeField] private Animator _leverAnimator;
    [SerializeField] private SphereCollider _triggerCollider;
    [SerializeField] private float _interactionCooldown = 2f;
    [SerializeField] private GameObject[] _linkedObjects;
    
    private static readonly int LeverToggleParam = Animator.StringToHash("LeverToggle");
    private ThirdPersonController _player;
    private bool _playerInTrigger;
    private bool _canInteract = true;
    
    private void Start()
    {
        if (_leverAnimator == null || _triggerCollider == null)
        {
            Debug.LogError("ObjectInteractionTrigger: Required components not assigned!", this);
        }

        // Проверка привязанных объектов
        if (_linkedObjects != null && _linkedObjects.Length > 0)
        {
            foreach (var obj in _linkedObjects)
            {
                if (obj == null)
                {
                    Debug.LogWarning("ObjectInteractionTrigger: One of the linked objects is null!", this);
                }
            }
        }
    }

    private void Update()
    {
        CheckPlayerInTrigger();
        
        if (_playerInTrigger && 
            _player != null && 
            _player.GetComponent<StarterAssetsInputs>().interaction && 
            _canInteract &&
            _player.CanInteract())
        {
            OnInteract();
            StartCoroutine(InteractionCooldown());
        }
    }

    private IEnumerator InteractionCooldown()
    {
        _canInteract = false;
        yield return new WaitForSeconds(_interactionCooldown);
        _canInteract = true;
    }

    private void CheckPlayerInTrigger()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        var controller = player.GetComponent<CharacterController>();
        if (controller == null) return;

        Vector3 point = player.transform.position + controller.center;
        bool wasInTrigger = _playerInTrigger;
        _playerInTrigger = Vector3.Distance(point, _triggerCollider.bounds.center) <= _triggerCollider.radius + controller.radius;

        if (_playerInTrigger != wasInTrigger)
        {
            if (_playerInTrigger)
            {
                _player = player.GetComponent<ThirdPersonController>();
            }
            else
            {
                _player = null;
            }
        }
    }

    public void OnInteract()
    {
        if (_leverAnimator != null)
        {
            // Получаем новое состояние рычага (инвертируем текущее)
            bool newState = !_leverAnimator.GetBool(LeverToggleParam);
            
            // Устанавливаем состояние для рычага
            _leverAnimator.SetBool(LeverToggleParam, newState);
            
            // Инвертируем активность для всех связанных объектов
            if (_linkedObjects != null)
            {
                foreach (var obj in _linkedObjects)
                {
                    if (obj != null)
                    {
                        // Инвертируем текущее состояние активности объекта
                        obj.SetActive(!obj.activeSelf);
                    }
                }
            }
        }
    }
}