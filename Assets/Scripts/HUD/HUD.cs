using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    [Header("Clock")]
    [SerializeField] private TMP_Text _clockText;
    [SerializeField] private TMP_Text _daysText;
    [SerializeField] private TMP_Text _seasonText;

    [Header("Health Bar")]
    [SerializeField] private ProgressBar _healthBar;
    [SerializeField] private Gradient _healthColorGradient;
    [SerializeField] private Gradient _healthFlashColorGradient;

    [Header("Stamina Bar")]
    [SerializeField] private ProgressBar _staminaBar;
    [SerializeField] private Gradient _staminaColorGradient;
    [SerializeField] private Gradient _staminaFlashColorGradient;

    private Color _healthColor;
    private Color _healthFlashColor;
    private Color _staminaColor;
    private Color _staminaFlashColor;

    private void Start()
    {
        UpdateHealth(95, 100);
    }

    private void Update()
    {
        UpdateClock(WorldTimeManager.Instance.Minutes, WorldTimeManager.Instance.Hours, WorldTimeManager.Instance.Days);
        Test();
    }

    private void UpdateClock(int minutes, int hours, int days)
    {
        _clockText.text = string.Format("{0:D2}:{1:00}", hours, minutes);
        _daysText.text = $"Day: {days}";
    }

    public void UpdateSeason(string season)
    {

    }

    public void UpdateHealth(float currentHealth, float maxHealth, float flashThreshold = 20.0f, float flashSpeed = 3.0f)
    {
        UpdateProgressBar
        (
            _healthBar,
            currentHealth,
            maxHealth,
            _healthColorGradient,
            _healthFlashColorGradient,
            flashThreshold,
            flashSpeed
        );
    }

    public void UpdateStamina(float currentStamina, float maxStamina, float flashThreshold = 20.0f, float flashSpeed = 3.0f)
    {
        UpdateProgressBar
        (
            _staminaBar,
            currentStamina,
            maxStamina,
            _staminaColorGradient,
            _staminaFlashColorGradient,
            flashThreshold,
            flashSpeed
        );
    }

    private void UpdateProgressBar(ProgressBar progressBar, float currentValue, float maxValue, Gradient colorGradient, Gradient flashColorGradient, float flashThreshold, float flashSpeed)
    {
        float normalized = currentValue % maxValue / maxValue;
        Color color = colorGradient.Evaluate(normalized);
        Color flashColor = flashColorGradient.Evaluate(normalized);

        progressBar.SetFlashActive(flashThreshold > 0 && currentValue < flashThreshold);
        progressBar.SetFillColor(color);
        progressBar.SetFlashColor(flashColor);
        progressBar.SetFlashSpeed(flashSpeed);
        progressBar.SetProgressValue(currentValue, maxValue);
    }

    private void Test()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) UpdateHealth(50, 100);
        if (Input.GetKeyDown(KeyCode.Alpha2)) UpdateHealth(10, 100);
        if (Input.GetKeyDown(KeyCode.Alpha3)) UpdateHealth(90, 100);
        if (Input.GetKeyDown(KeyCode.Alpha4)) UpdateHealth(100, 100);
    }
}
