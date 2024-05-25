using UnityEngine;
using UnityEngine.UI;

public class HealthBarDisplay : HealthDisplay
{
    [SerializeField] private Slider _healthBar;

    private void OnEnable()
    {
        Health.HealthChanged += OnHealthChanged;
    }

    private void Start()
    {
        _healthBar.maxValue = Health.MaxValue;
        _healthBar.value = Health.Value;
    }

    private void OnDisable()
    {
        Health.HealthChanged -= OnHealthChanged;
    }

    protected override void OnHealthChanged()
    {
        _healthBar.value = Health.Value;
    }
}
