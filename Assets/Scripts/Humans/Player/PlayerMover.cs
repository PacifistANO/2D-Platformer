using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(InputPlayer))]
[RequireComponent(typeof(PlayerGroundSensor))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;
    [SerializeField] private float _jumpForce = 7.5f;

    private PlayerGroundSensor _groundSensor;
    private Animator _animator;
    private Rigidbody2D _rigidbody2d;
    private InputPlayer _controlPlayer;
    private Fliper _fliper;
    private bool _isGrounded = false;
    private bool _isPreparedToClimb = false;
    private int _gravityScaleOn = 1;
    private int _gravityScaleOff = 0;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _controlPlayer = GetComponent<InputPlayer>();
        _groundSensor = GetComponent<PlayerGroundSensor>();
        _fliper = new Fliper();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Ladder ladder))
            _isPreparedToClimb = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Ladder ladder))
            _isPreparedToClimb = false;
    }

    private void Update()
    {
        ConfirmGroundCollision();
        _animator.SetFloat(HumanAnimator.Parameters.AirSpeedY, _rigidbody2d.velocity.y);
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
        Climb();
    }

    private void ConfirmGroundCollision()
    {
        _isGrounded = _groundSensor.IsCollided;
        _animator.SetBool(HumanAnimator.Parameters.Grounded, _isGrounded);
    }

    private void Move()
    {
        _rigidbody2d.velocity = new Vector2(_controlPlayer.InputXY.x * _speed, _rigidbody2d.velocity.y);

        if (Mathf.Abs(_controlPlayer.InputXY.x) > Mathf.Epsilon && _isGrounded == true)
        {
            transform.rotation = _fliper.FlipX(0, _controlPlayer.InputXY.x);
            _animator.SetInteger(HumanAnimator.Parameters.AnimState, 1);
        }
        else
            _animator.SetInteger(HumanAnimator.Parameters.AnimState, 0);
    }

    private void Jump()
    {
        if (_controlPlayer.JumpY > 0 && _isGrounded == true)
        {
            _animator.SetTrigger(HumanAnimator.Parameters.Jump);
            _isGrounded = false;
            _animator.SetBool(HumanAnimator.Parameters.Grounded, _isGrounded);
            _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, _jumpForce);
        }
    }

    private void Climb()
    {
        if (_isPreparedToClimb == true)
        {
            _rigidbody2d.gravityScale = _gravityScaleOff;
            _rigidbody2d.velocity = new Vector2(_controlPlayer.InputXY.x * _speed, _controlPlayer.InputXY.y * _speed);
        }
        else
            _rigidbody2d.gravityScale = _gravityScaleOn;
    }
}
