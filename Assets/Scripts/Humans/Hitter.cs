using UnityEngine;

public abstract class Hitter : MonoBehaviour
{
    [SerializeField] protected float AttackRadius;
    [SerializeField] protected float DelayBetweenAttacks;
    [SerializeField] protected LayerMask TargetLayerMask;

    [SerializeField] private Color _gizmosColor;

    protected Transform AttackPoint;
    protected Coroutine Attacking;
    protected Animator Animator;
    protected WaitForSeconds WaitingTime;
    protected int Damage;

    public Human Target { get; protected set; }

    protected void Hit()
    {
        Target.TakeDamage(Damage);
    }

    private void OnDrawGizmos()
    {
        if (AttackPoint != null)
        {
            Gizmos.color = _gizmosColor;
            Gizmos.DrawSphere(AttackPoint.position, AttackRadius);
        }
    }
}