using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;

public class RagdollActivator : MonoBehaviour
{
    [Header("Death Menu Settings")]
    [SerializeField] private PauseManager _pauseManager;
    [SerializeField] private float _deathMenuDelay = 2f;

    [Header("VFX Settings")]
    [SerializeField] private GameObject _deathVFX;
    [SerializeField] private float _vfxDuration = 2f;

    [Header("Ragdoll Wizard Fields")]
    [SerializeField] private Transform _pelvis;
    [SerializeField] private Transform _leftHips;
    [SerializeField] private Transform _leftKnee;
    [SerializeField] private Transform _leftFoot;
    [SerializeField] private Transform _rightHips;
    [SerializeField] private Transform _rightKnee;
    [SerializeField] private Transform _rightFoot;
    [SerializeField] private Transform _leftArm;
    [SerializeField] private Transform _leftElbow;
    [SerializeField] private Transform _rightArm;
    [SerializeField] private Transform _rightElbow;
    [SerializeField] private Transform _middleSpine;
    [SerializeField] private Transform _head;

    [Header("Main Components")]
    [SerializeField] private Collider _mainCollider;
    [SerializeField] private Animator _animator;

    [Header("Scripts to Disable on Death")]
    [SerializeField] private BasicRigidBodyPush _basicRigidBodyPush;
    [SerializeField] private ThirdPersonController _thirdPersonController;
    [SerializeField] private StarterAssetsInputs _starterAssetsInputs;

    private Rigidbody[] _ragdollRigidbodies;
    private Collider[] _ragdollColliders;

    private void Awake()
    {
        var wizardTransforms = new List<Transform>
        {
            _pelvis, _leftHips, _leftKnee, _leftFoot,
            _rightHips, _rightKnee, _rightFoot,
            _leftArm, _leftElbow, _rightArm, _rightElbow,
            _middleSpine, _head
        };

        var rbList = new List<Rigidbody>();
        var colList = new List<Collider>();

        foreach (var bone in wizardTransforms)
        {
            if (bone == null) continue;
            
            var rb = bone.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rbList.Add(rb);
            }

            var col = bone.GetComponent<Collider>();
            if (col != null)
            {
                col.enabled = false;
                colList.Add(col);
            }
        }

        _ragdollRigidbodies = rbList.ToArray();
        _ragdollColliders = colList.ToArray();

        if (_deathVFX != null)
            _deathVFX.SetActive(false);
    }

    [ContextMenu("Die")]
    public void Die()
    {
        if (_animator != null) 
            _animator.enabled = false;

        if (_mainCollider != null)
            _mainCollider.enabled = false;

        if (_basicRigidBodyPush != null)
            _basicRigidBodyPush.enabled = false;

        if (_thirdPersonController != null)
            _thirdPersonController.enabled = false;

        if (_starterAssetsInputs != null)
            _starterAssetsInputs.enabled = false;

        foreach (var rb in _ragdollRigidbodies)
            rb.isKinematic = false;

        foreach (var col in _ragdollColliders)
            col.enabled = true;

        if (_deathVFX != null)
        {
            _deathVFX.SetActive(true);
            if (_vfxDuration > 0)
                StartCoroutine(DisableVFX());
        }

        StartCoroutine(ShowDeathMenu());
    }

    private IEnumerator DisableVFX()
    {
        yield return new WaitForSeconds(_vfxDuration);
        if (_deathVFX != null)
            _deathVFX.SetActive(false);
    }

    private IEnumerator ShowDeathMenu()
    {
        yield return new WaitForSeconds(_deathMenuDelay);
        if (_pauseManager != null)
            _pauseManager.OnDeath();
    }
}