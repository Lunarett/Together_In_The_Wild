using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(WorldTimeManager), typeof(Light2D))]
public class TimeCycle : MonoBehaviour
{
    [SerializeField] private Gradient _timeCycleGradient;

    private WorldTimeManager _timeManager;
    private Light2D _light;
    private Color _lightColor;
    private float _dayDuration;

    private void Awake()
    {
        _timeManager = GetComponent<WorldTimeManager>();
        _light = GetComponent<Light2D>();
    }

    private void Start()
    {
        _lightColor = _light.color;
        _dayDuration = 86400f;
    }

    private void Update()
    {
        float normalizedTime = _timeManager.WorldTime % _dayDuration / _dayDuration;
        _lightColor = _timeCycleGradient.Evaluate(normalizedTime);
        _light.color = _lightColor;
    }
}
