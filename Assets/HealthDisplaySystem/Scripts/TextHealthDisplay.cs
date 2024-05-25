using System.Collections;
using TMPro;
using UnityEngine;

public class TextHealthDisplay : HealthDisplay
{
    [SerializeField] private TMP_Text _hpCount;
    [SerializeField] private TMP_Text _hpChangeCounter;

    private Coroutine _changeAlpha;

    private void OnEnable()
    {
        Health.HealthChanged += OnHealthChanged;
    }

    private void Start()
    {
        _hpCount.text = (Health.Value + "/" + Health.MaxValue);
    }

    private void OnDisable()
    {
        Health.HealthChanged -= OnHealthChanged;
    }
    
    protected override void OnHealthChanged()
    {
        _hpCount.text = (Health.Value + "/" + Health.MaxValue);

        if (_changeAlpha != null)
            StopCoroutine(_changeAlpha);

        _changeAlpha = StartCoroutine(ChangeAlpha());
    }

    private IEnumerator ChangeAlpha()
    {
        _hpChangeCounter.color = Color.yellow;
        _hpChangeCounter.text = Health.HealthChangerValue.ToString();
        float alphaValue = _hpChangeCounter.alpha;

        while (_hpChangeCounter.alpha != 0)
        {
            alphaValue = ChangeValue(alphaValue, 0, 3);
            _hpChangeCounter.alpha = alphaValue;

            yield return null;
        }
    }
}
