using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);
    private const string Jump = nameof(Jump);

    private float _inputX;
    private float _inputY;
    private float _jumpY;
    private bool _isAttacked;

    public float InputX => _inputX;
    public float InputY => _inputY;
    public float JumpY => _jumpY;
    public bool IsAttacked => _isAttacked;

    private void Update()
    {
        _inputX = Input.GetAxis(Horizontal);
        _inputY = Input.GetAxis(Vertical);
        _jumpY = Input.GetAxis(Jump);
        _isAttacked = Input.GetKeyDown(KeyCode.E);
    }
}
