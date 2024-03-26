using UnityEngine;

public class Player : Human 
{
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void AddHealth(Healer healer)
    {
        _health += healer.HealthIncrease;
    }
}