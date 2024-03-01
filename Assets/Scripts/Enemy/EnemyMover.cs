using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Transform _currentTarget;
    private Transform[] _targets;
    private int _currentTargetId;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
        _animator.SetInteger(EnemyAnimator.Parameters.AnimState, 1);

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
        _spriteRenderer.flipX = targetPosition > transform.position.x;
    }

    public void InitTargets(Transform[] targets)
    {
        _targets = targets;
    }
}
