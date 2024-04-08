using UnityEngine;

public class EnemyPatrolling : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    private Transform[] _targets;
    private int _currentTargetId = 0;
    private Transform _currentTarget;

    public float Speed => _speed;
    public Transform CurrentTarget => _currentTarget;

    public void InitRoute(Transform[] targets)
    {
        _targets = targets;
        _currentTarget = _targets[_currentTargetId];
    }

    public void SetNextPoint(ref Transform currentTarget)
    {
        _currentTargetId = ++_currentTargetId % _targets.Length;
        _currentTarget = _targets[_currentTargetId];
        currentTarget = _currentTarget;
    }
}
