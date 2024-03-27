using UnityEngine;

[RequireComponent (typeof(Animator))]
public class Player : Human 
{
    private void Start()
    {
        Animator = GetComponent<Animator>();
    }

    public void AddHealth(Healer healer)
    {
        Health += healer.HealthIncrease;
    }
}