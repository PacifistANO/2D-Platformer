using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Player), typeof(Animator), typeof(InputPlayer))]
public class HitterPlayer : Hitter
{
    private InputPlayer _inputPlayer;
    private float _timeAfterLastAttack;

    private void OnEnable()
    {
        _attackPoint = transform.GetChild(0);
        _damage = GetComponent<Player>().Damage;
        _animator = GetComponent<Animator>();
        _inputPlayer = GetComponent<InputPlayer>();
        _timeAfterLastAttack = _delayBetweenAttacks;
    }

    private void Update()
    {
        _timeAfterLastAttack += Time.deltaTime;

        if (_timeAfterLastAttack >= _delayBetweenAttacks)
        {
            Hitting();
        }
    }

    private void FixedUpdate()
    {
        Collider2D target = Physics2D.OverlapCircle(_attackPoint.position, _attackRadius, _targetLayerMask);

        if (target != null)
            _target = target.GetComponent<Enemy>();
        else if (_target != null)
            _target = null;
    }

    private void Hitting()
    {
        if (_inputPlayer.IsAttacked == true)
        {
            _animator.SetTrigger(HumanAnimator.Parameters.Attack);
            _timeAfterLastAttack = 0;

            if (_target != null)
            {
                Hit();
            }
        }
    }
}
