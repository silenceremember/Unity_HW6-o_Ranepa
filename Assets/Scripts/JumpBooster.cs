using UnityEngine;
using StarterAssets;

public class JumpBooster : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] private float _jumpHeightBonus = 0.5f;

    [Header("Player References")]
    [SerializeField] private ThirdPersonController _playerController;
    [SerializeField] private CharacterController _characterController;

    [Header("Booster Objects")]
    [SerializeField] private Collider _triggerCollider;
    [SerializeField] private GameObject _visualObject;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other == _characterController)
        {
            _playerController.JumpHeight += _jumpHeightBonus;
            
            _visualObject.SetActive(false);
            
            _triggerCollider.enabled = false;
        }
    }

    private void OnValidate()
    {
        if (_triggerCollider == null)
        {
            _triggerCollider = GetComponent<Collider>();
        }

        if (_visualObject == null)
        {
            _visualObject = gameObject;
        }
    }
}