using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightFlicker : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _intensity = 1f;
    [SerializeField] private float _minIntensity = 0f;
    
    private Light _light;
    private float _baseIntensity;
    private float _time;

    private void Start()
    {
        _light = GetComponent<Light>();
        _baseIntensity = _light.intensity;
    }

    private void Update()
    {
        _time += Time.deltaTime * _speed;
        
        float sinWave = (Mathf.Sin(_time) + 1f) * 0.5f;
        
        float newIntensity = Mathf.Lerp(_minIntensity, _intensity, sinWave);

        _light.intensity = _baseIntensity * newIntensity;
    }
}