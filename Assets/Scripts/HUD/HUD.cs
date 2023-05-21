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
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private TMP_Text _healthPercentText; 

    [Header("Stamina Bar")]
    [SerializeField] private Slider _staminaSlider;
    [SerializeField] private TMP_Text _staminaPercentText; 


    private void Update()
    {
        UpdateClock(WorldTimeManager.Instance.Minutes, WorldTimeManager.Instance.Hours, WorldTimeManager.Instance.Days);
    }

    private void UpdateClock(int minutes, int hours, int days)
    {
        _clockText.text = string.Format("{0:D2}:{1:00}", hours, minutes);
        _daysText.text = $"Day: {days}";
    }

    public void UpdateSeason(string season)
    {
        
    }

    public void UpdateHealthValue(float currentHealth, float maxHealth)
    {

    }

    public void UpdateHungerValue(float currentHunger, float maxHunger)
    {

    }
}
