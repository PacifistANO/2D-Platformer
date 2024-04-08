using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Player), typeof(Animator), typeof(InputPlayer))]
public class HitterPlayer : Hitter
{
    private InputPlayer _inputPlayer;

    private void OnEnable()
    {
        AttackPoint = transform.GetChild(0);
        Damage = GetComponent<Player>().Damage;
        Animator = GetComponent<Animator>();
        WaitingTime = new WaitForSeconds(DelayBetweenAttacks);
        _inputPlayer = GetComponent<InputPlayer>();
    }

    private void Update()
    {
        if (_inputPlayer.IsAttacked == true && Attacking == null)
        {
            Attack();
        }
    }

    private void Attack()
    {
        Collider2D collider = Physics2D.OverlapCircle(AttackPoint.position, AttackRadius, TargetLayerMask);

        if (collider != null && collider.TryGetComponent(out Enemy enemy))
        {
            Target = enemy;
        }

        Attacking = StartCoroutine(Hitting());
    }

    private IEnumerator Hitting()
    {
        Animator.SetTrigger(HumanAnimator.Parameters.Attack);

        if (Target != null)
        {
            Hit();
            Target = null;
        }

        yield return WaitingTime;
        Attacking = null;
    }
}
