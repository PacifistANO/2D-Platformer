using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyMover), typeof(Animator), typeof(Enemy))]
public class HitterEnemy : Hitter
{
    private EnemyMover _enemyMover;
    private float _delayBeforeDamage;

    private void OnEnable()
    {
        _attackPoint = transform.GetChild(0);
        _enemyMover = GetComponent<EnemyMover>();
        _damage = GetComponent<Enemy>().Damage;
        _animator = GetComponent<Animator>();
        _delayBeforeDamage = 0.4f;
    }

    private void FixedUpdate()
    {
        Collider2D target = Physics2D.OverlapCircle(_attackPoint.position, _attackRadius, _targetLayerMask);

        if (target != null)
        {
            _target = target.GetComponent<Player>();

            StartAttack();
        }
        else
            StopAttack();
    }

    private void OnDisable()
    {
        if (_attack != null)
            StopCoroutine(_attack);
    }

    private void StartAttack()
    {
        _animator.SetInteger(HumanAnimator.Parameters.AnimState, 0);
        _enemyMover.enabled = false;

        if (_attack == null)
            _attack = StartCoroutine(Hitting());
    }

    private void StopAttack()
    {
        if (_attack != null)
        {
            StopCoroutine(_attack);
            _attack = null;
        }

        if (_enemyMover.enabled == false)
            _enemyMover.enabled = true;
    }

    private IEnumerator Hitting()
    {
        var waitingTime = new WaitForSeconds(_delayBetweenAttacks);

        while (_target != null)
        {
            _animator.SetTrigger(HumanAnimator.Parameters.Attack);
            Invoke(nameof(Hit), _delayBeforeDamage);
            yield return waitingTime;
        }
    }
}
