using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Animator _animator;
    private Transform[] _targets;
    private Transform _currentTarget;
    private Fliper _fliper;
    private int _currentTargetId;
    private int direction = 1;
    private float _minDistanceToTarget = 0.1f;
    private bool _playerDetected;
    private float _attackSpeed;
    private float _patrolSpeed;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _currentTargetId = 0;
        _currentTarget = _targets[_currentTargetId];
        _fliper = new Fliper();
        transform.rotation = _fliper.FlipX(_currentTarget.position.x, transform.position.x);
        _attackSpeed = _speed * 1.5f;
        _patrolSpeed = _speed;
    }

    private void FixedUpdate()
    {
        MoveToTarget();
    }

    public void InitTargets(Transform[] targets)
    {
        _targets = targets;
    }

    public void SetTarget(Transform target)
    {
        if (target != null)
        {
            _speed = _attackSpeed;
            _currentTarget = target;
            _playerDetected = true;
            ChooseDirection();
        }

        else if (_playerDetected == true)
        {
            _speed = _patrolSpeed;
            _playerDetected = false;
            ChangeCurrentTarget();
            ChooseDirection();
        }
    }

    private void MoveToTarget()
    {
        _animator.SetInteger(HumanAnimator.Parameters.AnimState, 1);
        transform.Translate(Vector2.right * direction * _speed * Time.deltaTime, Space.World);

        if (Vector2.Distance(transform.position, _currentTarget.position) < _minDistanceToTarget)
        {
            ChangeCurrentTarget();
            ChooseDirection();
        }
    }

    private void ChooseDirection()
    {
        if (_currentTarget.position.x > transform.position.x)
            direction = 1;
        else
            direction = -1;

        transform.rotation = _fliper.FlipX(_currentTarget.position.x, transform.position.x);
    }

    private void ChangeCurrentTarget()
    {
        _currentTargetId = ++_currentTargetId % _targets.Length;
        _currentTarget = _targets[_currentTargetId];
    }
}
