using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightDimmer : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _startDimDistance = 100f;
    [SerializeField] private float _fullDimDistance = 60f;

    private Light _light;
    private float _baseIntensity;

    private void Start()
    {
        _light = GetComponent<Light>();
        _baseIntensity = _light.intensity;
    }

    private void Update()
    {
        float distance = Vector3.Distance(_player.position, transform.position);
        
        if (distance <= _startDimDistance)
        {
            float dimAmount = Mathf.InverseLerp(_fullDimDistance, _startDimDistance, distance);
            _light.intensity = _baseIntensity * dimAmount;
        }
        else
        {
            _light.intensity = _baseIntensity;
        }
    }
}