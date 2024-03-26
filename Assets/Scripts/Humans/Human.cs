using UnityEngine;

public abstract class Human : MonoBehaviour
{
    [SerializeField] protected int _health;
    [SerializeField] protected int _damage;

    protected Animator _animator;

    public int Damage => _damage;

    public void TakeDamage(int damage)
    {
        _animator.SetTrigger(HumanAnimator.Parameters.Hurt);
        _health -= damage;

        if (_health <= 0)
            Die();
    }

    private void Die()
    {
        _animator.SetTrigger(HumanAnimator.Parameters.Death);

        if (TryGetComponent(out Rigidbody2D rigidbody2D))
            rigidbody2D.bodyType = RigidbodyType2D.Static;

        foreach (Behaviour behaviour in GetComponents<Behaviour>())
        {
            if (behaviour != GetComponent<Animator>())
                behaviour.enabled = false;
        }
    }
}