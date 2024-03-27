using UnityEngine;

public abstract class Human : MonoBehaviour
{
    [SerializeField] protected int Health;

    [SerializeField] private int _damage;

    protected Animator Animator;

    public int Damage => _damage;

    public void TakeDamage(int damage)
    {
        Animator.SetTrigger(HumanAnimator.Parameters.Hurt);
        Health -= damage;

        if (Health <= 0)
            Die();
    }

    private void Die()
    {
        Animator.SetTrigger(HumanAnimator.Parameters.Death);

        if (TryGetComponent(out Rigidbody2D rigidbody2D))
            rigidbody2D.bodyType = RigidbodyType2D.Static;

        foreach (Behaviour behaviour in GetComponents<Behaviour>())
        {
            if (behaviour != Animator)
                behaviour.enabled = false;
        }
    }
}