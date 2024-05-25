using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothHealthDisplay : HealthDisplay
{
    [SerializeField] private Slider _smoothHealthBar;

    private Coroutine _changeHealthBar;

    private void OnEnable()
    {
        Health.HealthChanged += OnHealthChanged;
    }

    private void Start()
    {
        _smoothHealthBar.maxValue = Health.Value;
        _smoothHealthBar.value = Health.Value;
    }

    private void OnDisable()
    {
        Health.HealthChanged -= OnHealthChanged;
    }
    
    protected override void OnHealthChanged()
    {
        if (_changeHealthBar != null)
            StopCoroutine(_changeHealthBar);

        _changeHealthBar = StartCoroutine(ChangeHealthBar());
    }

    private IEnumerator ChangeHealthBar()
    {
        float healthValue = _smoothHealthBar.value;

        while (_smoothHealthBar.value != Health.Value)
        {
            healthValue = ChangeValue(healthValue, Health.Value);
            _smoothHealthBar.value = healthValue;

            yield return null;
        }
    }
}
