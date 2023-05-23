using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PulsarDevKit.Scripts.Debug;

public class ProgressBar : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider _progressSlider;
    [SerializeField] private Image _fillImage;
    [SerializeField] private TMP_Text _percentageText;
    [Space]
    [SerializeField] private Color _defaultColor = Color.grey;

    [SerializeField] private bool _isFlashing = false;
    [SerializeField] private Color _flashColor = Color.clear;
    [SerializeField] private float _flashSpeed = 1.0f;

    private void Start()
    {
        if (!_progressSlider) PulseLogger.Error("Progress Slider has returned null!");
        if (!_fillImage) PulseLogger.Error("Fill Image has returned null!");
        if (!_percentageText) PulseLogger.Error("Percentage Text has returned null!");

        _fillImage.color = _defaultColor;
    }

    private void Update()
    {
        UpdateFlash();
    }

    public void SetProgressValue(float currentValue, float maxValue)
    {
        float percentage = currentValue / maxValue;
        _progressSlider.value = percentage;
        _percentageText.text = $"{Mathf.RoundToInt(percentage * 100)}%";
    }

    public void SetFillColor(Color color)
    {
        _defaultColor = color;
        _fillImage.color = color;
    }

    public void SetFlashActive(bool isActive)
    {
        _isFlashing = isActive;

        if (!isActive) ResetFillAlpha();
    }

    public void SetFlashSpeed(float speed)
    {
        _flashSpeed = speed;
    }

    public void SetFlashColor(Color color)
    {
        _flashColor = color;
    }

    private void UpdateFlash()
    {
        if (!_isFlashing) return;
        float t = Mathf.PingPong(Time.time * _flashSpeed, 1.0f);
        Color flashColor = Color.Lerp(_defaultColor, _flashColor, t);
        _fillImage.color = flashColor;
    }

    private void ResetFillAlpha()
    {
        Color fillColor = _fillImage.color;
        fillColor.a = 1.0f;
        _fillImage.color = fillColor;
    }
}
