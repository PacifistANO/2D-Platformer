using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);
    private const string Jump = nameof(Jump);

    private Vector2 _inputXY;
    private float _jumpY;
    private bool _isAttacked;

    public Vector2 InputXY => _inputXY;
    public float JumpY => _jumpY;
    public bool IsAttacked => _isAttacked;

    private void Update()
    {
        _inputXY = new Vector2(Input.GetAxis(Horizontal),Input.GetAxis(Vertical));
        _jumpY = Input.GetAxis(Jump);
        _isAttacked = Input.GetKeyDown(KeyCode.E);
    }
}
