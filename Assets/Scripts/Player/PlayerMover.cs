using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent (typeof(ControlPlayer))]
public class PlayerMover : MonoBehaviour
{
    private const string Ladder = nameof(Ladder);

    [SerializeField] private float _speed = 4.0f;
    [SerializeField] private float _jumpForce = 7.5f;
    [SerializeField] private PlayerGroundSensor _groundSensor;

    private Animator _animator;
    private Rigidbody2D _rigidbody2d;
    private SpriteRenderer _spriteRenderer;
    private ControlPlayer _controlPlayer;
    private bool _isGrounded = false;
    private bool _isPlaced = false;
    private float _delayToIdle = 0.0f;
    private int _gravityScaleOn = 1;
    private int _gravityScaleOff = 0;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _controlPlayer = GetComponent<ControlPlayer>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == Ladder)
            _isPlaced = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == Ladder)
            _isPlaced = false;
    }

    private void Update()
    {
        CollisionWithGroundCheck();
        _animator.SetFloat(PlayerAnimator.Parameters.AirSpeedY, _rigidbody2d.velocity.y);
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
        Climb();
    }

    private void FlipX(float inputX)
    {
        _spriteRenderer.flipX = inputX < 0;
    }

    private void CollisionWithGroundCheck()
    {
        _isGrounded = _groundSensor.IsCollided;
        _animator.SetBool(PlayerAnimator.Parameters.Grounded, _isGrounded);
    }

    private void Move()
    {
        _rigidbody2d.velocity = new Vector2(_controlPlayer.InputX * _speed, _rigidbody2d.velocity.y);
        FlipX(_controlPlayer.InputX);

        if (Mathf.Abs(_controlPlayer.InputX) > Mathf.Epsilon && _isGrounded == true)
        {
            _delayToIdle = Mathf.Epsilon;
            _animator.SetInteger(PlayerAnimator.Parameters.AnimState, 1);
        }
        else
        {
            _delayToIdle -= Time.deltaTime;

            if (_delayToIdle < 0)
                _animator.SetInteger(PlayerAnimator.Parameters.AnimState, 0);
        }
    }

    private void Jump()
    {
        if (_controlPlayer.JumpY > 0 && _isGrounded == true)
        {
            _animator.SetTrigger(PlayerAnimator.Parameters.Jump);
            _isGrounded = false;
            _animator.SetBool(PlayerAnimator.Parameters.Grounded, _isGrounded);
            _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, _jumpForce);
        }
    }

    private void Climb()
    {
        if (_isPlaced == true)
        {
            _rigidbody2d.gravityScale = _gravityScaleOff;
            _rigidbody2d.velocity = new Vector2(_controlPlayer.InputX * _speed, _controlPlayer.InputY * _speed);
        }
        else
        {
            _rigidbody2d.gravityScale = _gravityScaleOn;
        }
    }
}
