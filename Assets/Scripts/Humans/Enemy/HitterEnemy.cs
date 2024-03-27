using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyMover), typeof(Animator), typeof(Enemy))]
public class HitterEnemy : Hitter
{
    private EnemyMover _enemyMover;
    private float _delayBeforeDamage;

    private void OnEnable()
    {
        AttackPoint = transform.GetChild(0);
        Damage = GetComponent<Enemy>().Damage;
        Animator = GetComponent<Animator>();
        _enemyMover = GetComponent<EnemyMover>();
        _delayBeforeDamage = 0.5f;
    }

    private void FixedUpdate()
    {
        Collider2D target = Physics2D.OverlapCircle(AttackPoint.position, AttackRadius, TargetLayerMask);

        if (target != null)
        {
            Target = target.GetComponent<Player>();

            StartAttack();
        }
        else
            StopAttack();
    }

    private void OnDisable()
    {
        if (Attack != null)
            StopCoroutine(Attack);
    }

    private void StartAttack()
    {
        Animator.SetInteger(HumanAnimator.Parameters.AnimState, 0);
        _enemyMover.enabled = false;

        if (Attack == null)
            Attack = StartCoroutine(Hitting());
    }

    private void StopAttack()
    {
        if (Attack != null)
        {
            StopCoroutine(Attack);
            Attack = null;
        }

        if (_enemyMover.enabled == false)
            _enemyMover.enabled = true;
    }

    private IEnumerator Hitting()
    {
        var waitingTime = new WaitForSeconds(DelayBetweenAttacks);

        while (Target != null)
        {
            Animator.SetTrigger(HumanAnimator.Parameters.Attack);
            Invoke(nameof(Hit), _delayBeforeDamage);
            yield return waitingTime;
        }
    }
}
