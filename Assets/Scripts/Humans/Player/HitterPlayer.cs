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
        _inputPlayer = GetComponent<InputPlayer>();
    }

    private void Start()
    {
        StartCoroutine(Hitting());
    }

    private void FixedUpdate()
    {
        Collider2D target = Physics2D.OverlapCircle(AttackPoint.position, AttackRadius, TargetLayerMask);

        if (target != null)
            Target = target.GetComponent<Enemy>();
        else if (Target != null)
            Target = null;
    }

    private void OnDisable()
    {
        StopCoroutine(Hitting());
    }

    private IEnumerator Hitting()
    {
        WaitForSeconds waitingTime = new WaitForSeconds(DelayBetweenAttacks);

        while (true)
        {
            if (_inputPlayer.IsAttacked == true)
            {
                Animator.SetTrigger(HumanAnimator.Parameters.Attack);

                if (Target != null)
                {
                    Hit();
                }

                yield return waitingTime;
            }
            else
                yield return null;
        }
    }
}
