using UnityEngine;
using StarterAssets;
using System.Collections;

public class InteractionTrigger : MonoBehaviour
{
    [SerializeField] private Animator _leverAnimator;
    [SerializeField] private SphereCollider _triggerCollider;
    [SerializeField] private float _interactionCooldown = 2f;
    [SerializeField] private Animator[] _linkedAnimators;
    [SerializeField] private GameObject _hintCanvas;

    private static readonly int _leverToggleParam = Animator.StringToHash("LeverToggle");
    private ThirdPersonController _player;
    private bool _playerInTrigger;
    private bool _canInteract = true;

    private void Start()
    {
        if (_leverAnimator == null || _triggerCollider == null)
        {
            Debug.LogError("InteractionTrigger: Required components not assigned!", this);
        }

        if (_linkedAnimators != null && _linkedAnimators.Length > 0)
        {
            foreach (var animator in _linkedAnimators)
            {
                if (animator == null)
                {
                    Debug.LogWarning("InteractionTrigger: One of the linked animators is null!", this);
                }
            }
        }

        if (_hintCanvas != null)
        {
            _hintCanvas.SetActive(false);
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

                if (_hintCanvas != null)
                {
                    _hintCanvas.SetActive(true);
                }
            }
            else
            {
                _player = null;

                if (_hintCanvas != null)
                {
                    _hintCanvas.SetActive(false);
                }
            }
        }
    }

    public void OnInteract()
    {
        if (_leverAnimator != null)
        {
            bool newState = !_leverAnimator.GetBool(_leverToggleParam);
            
            _leverAnimator.SetBool(_leverToggleParam, newState);
            
            if (_linkedAnimators != null)
            {
                foreach (var animator in _linkedAnimators)
                {
                    if (animator != null)
                    {
                        animator.SetBool(_leverToggleParam, newState);
                    }
                }
            }
        }
    }
}