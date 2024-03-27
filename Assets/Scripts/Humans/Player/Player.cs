using UnityEngine;

[RequireComponent (typeof(Animator), typeof(CharacterHealth))]
public class Player : Human 
{
    private void Start()
    {
        Animator = GetComponent<Animator>();
        CharacterHealth = GetComponent<CharacterHealth>();
    }
}