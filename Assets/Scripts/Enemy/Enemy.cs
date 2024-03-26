using UnityEngine;

public class Enemy : Human 
{
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
}

