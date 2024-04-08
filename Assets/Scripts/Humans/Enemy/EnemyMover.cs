using UnityEngine;

[RequireComponent(typeof(Animator), typeof(EnemyPatrolling), typeof(EnemyPursuit))]
public class EnemyMover : MonoBehaviour
{
    private float _speed;
    private Animator _animator;
    private Fliper _fliper;
    private int direction = 1;
    private float _minDistanceToTarget = 0.1f;
    private Transform _currentTarget;
    private EnemyPatrolling _enemyPatrolling;
    private EnemyPursuit _enemyPursuit;

    private void OnEnable()
    {
        _enemyPursuit = GetComponent<EnemyPursuit>();
        _enemyPursuit.TargetDetected += SetTarget;
    }

    private void OnDisable()
    {
        _enemyPursuit.TargetDetected -= SetTarget;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _enemyPatrolling = GetComponent<EnemyPatrolling>();
        _fliper = new Fliper();
        _speed = _enemyPatrolling.Speed;
        _currentTarget = _enemyPatrolling.CurrentTarget;
        ChooseDirection();
    }

    private void FixedUpdate()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        _animator.SetInteger(HumanAnimator.Parameters.AnimState, 1);
        transform.Translate(Vector2.right * direction * _speed * Time.deltaTime, Space.World);

        if (Vector2.Distance(transform.position, _currentTarget.position) < _minDistanceToTarget)
        {
            _enemyPatrolling.SetNextPoint(ref _currentTarget);
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

    private void SetTarget(Transform target, float speed)
    {
        if (target != null)
        {
            _currentTarget = target;
            _speed = speed;
        }
        else
        {
            _currentTarget = _enemyPatrolling.CurrentTarget;
            _speed = _enemyPatrolling.Speed;
        }

        ChooseDirection();
    }
}
