using UnityEngine;

[RequireComponent(typeof(EnemyPursuit))]
public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayerMask;
    [SerializeField] private Vector2 _detectorSize = Vector2.one;
    [SerializeField] private Vector2 _detectorOffset = Vector2.zero;
    [SerializeField] private Color _gizmoColor;

    private Transform _detectorOrigin;
    private EnemyPursuit _enemyPursuit;
    private bool _detected;

    private void Start()
    {
        _detectorOrigin = transform.parent;
        _enemyPursuit = GetComponent<EnemyPursuit>();
    }

    private void FixedUpdate()
    {
        Collider2D collider = Physics2D.OverlapBox((Vector2)_detectorOrigin.position + _detectorOffset, _detectorSize, 0, _playerLayerMask);

        if (collider != null)
        {
            _enemyPursuit.DetectTarget(collider.transform);
            _detected = true;
        }
        else
        {
            if(_detected == true)
            {
                _enemyPursuit.DetectTarget(null);
                _detected = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmoColor;
        Gizmos.DrawCube((Vector2)_detectorOrigin.position + _detectorOffset, _detectorSize);
    }
}
