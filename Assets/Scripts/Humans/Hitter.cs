using System.Collections;
using UnityEngine;

public abstract class Hitter : MonoBehaviour
{
    [SerializeField] protected float _attackRadius;
    [SerializeField] protected float _delayBetweenAttacks;
    [SerializeField] protected LayerMask _targetLayerMask;
    [SerializeField] protected Color _gizmoColor;

    protected Transform _attackPoint;
    protected Human _target;
    protected Coroutine _attack;
    protected Animator _animator;
    protected WaitForSeconds _waitingTime;
    protected int _damage;

    protected void Hit()
    {
        _target.TakeDamage(_damage);
    }

    private void OnDrawGizmos()
    {
        if (_attackPoint != null)
        {
            Gizmos.color = _gizmoColor;
            Gizmos.DrawSphere(_attackPoint.position, _attackRadius);
        }
    }
}