using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    [SerializeField] private RagdollActivator _ragdollActivator;

    private void OnTriggerEnter(Collider other)
    {
        // Например, если триггер сработал при столкновении с объектом, 
        // помеченным тэгом "KillZone"
        if (other.CompareTag("KillZone"))
        {
            _ragdollActivator.Die();
        }
    }
}
