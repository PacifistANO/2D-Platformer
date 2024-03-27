using UnityEngine;

[RequireComponent (typeof(Animator))]
public class Enemy : Human 
{
    private void Start()
    {
        Animator = GetComponent<Animator>();
    }
}

