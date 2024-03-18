using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Animator _animator;
    private Transform[] _targets;
    private Transform _currentTarget;
    private int _currentTargetId;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _currentTargetId = 0;
        _currentTarget = _targets[_currentTargetId];
        FlipX(_currentTarget.position.x);
    }

    private void FixedUpdate()
    {
        MoveToTarget();
    }

    public void InitTargets(Transform[] targets)
    {
        _targets = targets;
    }

    private void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, _currentTarget.position, _speed * Time.deltaTime);
        _animator.SetInteger(EnemyAnimator.Parameters.AnimState, 1);

        if (transform.position == _currentTarget.position)
        {
            ChangeCurrentTarget();
            FlipX(_currentTarget.position.x);
        }
    }

    private void ChangeCurrentTarget()
    {
        _currentTargetId = ++_currentTargetId % _targets.Length;
        _currentTarget = _targets[_currentTargetId];
    }

    private void FlipX(float targetPosition)
    {
        bool direction = targetPosition > transform.position.x;
        transform.rotation = Quaternion.Euler(0, 180 * Convert.ToInt32(direction), 0);
    }
}
