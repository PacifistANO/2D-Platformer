using UnityEngine;

public class PlayerGroundSensor : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    private bool _isCollided;
    private float _checkDistance = 0.1f;

    public bool IsCollided => _isCollided;

    private void FixedUpdate()
    {
        if (Physics2D.Raycast(transform.position, -transform.up, _checkDistance, _layerMask))
        {
            _isCollided = true;
        }
        else
        {
            _isCollided = false;
        }
    }
}
