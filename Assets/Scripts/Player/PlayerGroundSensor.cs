using UnityEngine;

public class PlayerGroundSensor : MonoBehaviour
{
    private bool _isCollided;

    public bool IsCollided => _isCollided;

    private void OnTriggerEnter2D(Collider2D other)
    {
        _isCollided = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _isCollided = false;
    }
}
