using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    [SerializeField] private RagdollActivator _ragdollActivator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillZone"))
        {
            _ragdollActivator.Die();
        }
    }
}
