using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerGroundSensor : MonoBehaviour
{
    private bool _isCollided;
    private const string Ground = nameof(Ground);

    public bool IsCollided => _isCollided;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == Ground)
            _isCollided = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == Ground)
            _isCollided = false;
    }
}
