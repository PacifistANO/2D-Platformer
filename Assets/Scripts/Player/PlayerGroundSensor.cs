using UnityEngine;

public class PlayerGroundSensor : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    private bool _isCollided;

    public bool IsCollided => _isCollided;

    private void FixedUpdate()
    {
        if (Physics2D.Raycast(transform.position, -transform.up, 0.1f, _layerMask))
        {
            _isCollided = true;
        }
        else
        {
            _isCollided = false;
        }
    }
}
