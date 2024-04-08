using UnityEngine;

public abstract class Human : MonoBehaviour
{
    [SerializeField] private int _damage;

    protected Animator Animator;
    protected CharacterHealth CharacterHealth;

    public int Damage => _damage;

    private void Start()
    {
        Animator = GetComponent<Animator>();
        CharacterHealth = GetComponent<CharacterHealth>();
    }

    public void TakeDamage(int damage)
    {
        Animator.SetTrigger(HumanAnimator.Parameters.Hurt);
        CharacterHealth.DecreaseHealth(damage);

        if (CharacterHealth.Value <= 0)
            Die();
    }

    private void Die()
    {
        if (TryGetComponent(out Rigidbody2D rigidbody2D))
            rigidbody2D.bodyType = RigidbodyType2D.Static;

        foreach (Behaviour behaviour in GetComponents<Behaviour>())
        {
            if (behaviour != Animator)
                behaviour.enabled = false;
        }
        
        Animator.SetTrigger(HumanAnimator.Parameters.Death);
    }
}