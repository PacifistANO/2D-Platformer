using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private CharacterHealth _health;

    protected CharacterHealth Health => _health;

    protected virtual void OnHealthChanged() { }

    protected float ChangeValue(float currentValue, float finalValue, float delta = 50)
    {
        currentValue = Mathf.MoveTowards(currentValue, finalValue, delta * Time.deltaTime);
        return currentValue;
    }
}
