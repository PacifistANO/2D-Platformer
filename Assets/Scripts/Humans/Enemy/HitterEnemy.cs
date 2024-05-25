using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Enemy))]
public class HitterEnemy : Hitter
{
    private float _delayBeforeDamage;

    private void Start()
    {
        AttackPoint = transform.GetChild(0);
        Damage = GetComponent<Enemy>().Damage;
        Animator = GetComponent<Animator>();
        _delayBeforeDamage = 0.5f;
    }

    private void FixedUpdate()
    {
        Collider2D target = Physics2D.OverlapCircle(AttackPoint.position, AttackRadius, TargetLayerMask);

        if (target != null)
            Target = target.GetComponent<Player>();
        else
            Target = null;
    }

    private void OnDisable()
    {
        Target = null;
    }

    public void StartAttack()
    {
        Animator.SetInteger(HumanAnimator.Parameters.AnimState, 0);

        if (Attacking == null)
            Attacking = StartCoroutine(Hitting());
    }

    public void StopAttack()
    {
        if (Attacking != null)
            Attacking = null;
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
