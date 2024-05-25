using System;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private float _value;

    private float _maxValue;
    private float _minValue;
    private float _healthChangerValue;

    public Action HealthChanged;

    public float Value => _value;
    public float MaxValue => _maxValue;
    public float HealthChangerValue => _healthChangerValue;

    private void Awake()
    {
        _maxValue = _value;
        _minValue = 0;
    }

    public void Increase(float addition)
    {
        HealthChange(addition,false);
    }

    public void Degrease(float reduction)
    {
        HealthChange(reduction, true);
    }

    private void HealthChange(float value, bool isDamaged)
    {
        if (value < 0)
            return;

        _healthChangerValue = value;
        _value = Mathf.Clamp(isDamaged ? _value -= _healthChangerValue : _value += _healthChangerValue, _minValue, _maxValue);

        HealthChanged?.Invoke();
    }
}
