using UnityEngine;
using StarterAssets;
using System.Collections;

public class JumpBooster : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] private float _jumpHeightBonus = 0.5f;

    [Header("Player References")]
    [SerializeField] private ThirdPersonController _playerController;
    [SerializeField] private CharacterController _characterController;

    [Header("Booster Objects")]
    [SerializeField] private Collider _triggerCollider;
    [SerializeField] private MeshRenderer _visualObject;
    [SerializeField] private GameObject _pickupVFX;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other == _characterController)
        {
            _playerController.JumpHeight += _jumpHeightBonus;
            
            _visualObject.enabled = false;
            _triggerCollider.enabled = false;

            if (_pickupVFX != null)
            {
                StartCoroutine(ShowPickupVFX());
            }
        }
    }

    private IEnumerator ShowPickupVFX()
    {
        _pickupVFX.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        _pickupVFX.SetActive(false);
    }

    private void OnValidate()
    {
        if (_triggerCollider == null)
        {
            _triggerCollider = GetComponent<Collider>();
        }

        if (_visualObject == null)
        {
            _visualObject = GetComponent<MeshRenderer>();
        }
    }
}