using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Animator _animator;
    private Transform _currentTarget;
    private Transform[] _targets;
    private int _currentTargetId;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _currentTargetId = 0;
        _currentTarget = _targets[_currentTargetId];
        FlipX(_currentTarget.position.x);
    }

    private void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, _currentTarget.position, _speed * Time.deltaTime);
        _animator.SetInteger(EnemyAnimatorController.Parameters.AnimState, 1);

        if (transform.position == _currentTarget.position)
        {
            SetTarget();
            FlipX(_currentTarget.position.x);
        }

    }

    private void SetTarget()
    {
        _currentTargetId = (_currentTargetId + 1) % _targets.Length;
        _currentTarget = _targets[_currentTargetId];
    }

    private void FlipX(float targetPosition)
    {
        if (targetPosition > transform.position.x)
            GetComponent<SpriteRenderer>().flipX = true;
        else
            GetComponent<SpriteRenderer>().flipX = false;
    }

    public void InitTargets(Transform[] targets)
    {
        _targets = targets;
    }
}
