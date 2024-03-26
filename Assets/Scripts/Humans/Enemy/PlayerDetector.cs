using UnityEngine;

[RequireComponent(typeof(EnemyMover))]
public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayerMask;
    [SerializeField] private Vector2 _detectorSize = Vector2.one;
    [SerializeField] private Vector2 _detectorOffset = Vector2.zero;
    [SerializeField] private Color _gizmoColor;

    private Transform _detectorOrigin;
    private EnemyMover _enemyMover;

    private void Start()
    {
        _detectorOrigin = transform.parent;
        _enemyMover = GetComponent<EnemyMover>();
    }

    private void FixedUpdate()
    {
        Collider2D collider = Physics2D.OverlapBox((Vector2)_detectorOrigin.position + _detectorOffset, _detectorSize, 0, _playerLayerMask);

        if (collider != null)
            _enemyMover.SetTarget(collider.transform);
        else
            _enemyMover.SetTarget(null);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmoColor;
        Gizmos.DrawCube((Vector2)_detectorOrigin.position + _detectorOffset, _detectorSize);
    }
}
