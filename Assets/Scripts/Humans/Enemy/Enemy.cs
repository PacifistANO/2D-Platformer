using UnityEngine;

[RequireComponent(typeof(EnemyMover), typeof(HitterEnemy))]
public class Enemy : Human
{
    private EnemyMover _mover;
    private HitterEnemy _hitter;

    private void OnEnable()
    {
        _mover = GetComponent<EnemyMover>();
        _hitter = GetComponent<HitterEnemy>();
    }

    private void Update()
    {
        if (_hitter.Target != null)
        {
            TransitToAttack();
        }
        else
        {
            if (_mover.enabled == false)
                TransitToMove();
        }
    }

    private void TransitToAttack()
    {
        _mover.enabled = false;
        _hitter.StartAttack();
    }

    public void TransitToMove()
    {
        _hitter.StopAttack();
        _mover.enabled = true;
    }
}


