using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);
    private const string Jump = nameof(Jump);
    private const string Ladder = nameof(Ladder);

    [SerializeField] private float _speed = 4.0f;
    [SerializeField] private float _jumpForce = 7.5f;
    [SerializeField] private PlayerGroundSensor _groundSensor;

    private Animator _animator;
    private Rigidbody2D _rigidbody2d;
    private bool _isGrounded = false;
    private float _delayToIdle = 0.0f;
    private int _gravityScaleOn = 1;
    private int _gravityScaleOff = 0;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CollisionWithGroundCheck();
        Move();
        ToJump();

        _animator.SetFloat(PlayerAnimator.Parameters.AirSpeedY, _rigidbody2d.velocity.y);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == Ladder && _isGrounded)
        {
            _rigidbody2d.gravityScale = _gravityScaleOff;
            float inputY = Input.GetAxis(Vertical);
            _rigidbody2d.velocity = new Vector2(0, inputY * _speed);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == Ladder)
            _rigidbody2d.gravityScale = _gravityScaleOn;
    }

    private void FlipX(float inputX)
    {
        if (inputX > 0)
            GetComponent<SpriteRenderer>().flipX = false;
        else if (inputX < 0)
            GetComponent<SpriteRenderer>().flipX = true;
    }

    private void CollisionWithGroundCheck()
    {
        if (_isGrounded == false && _groundSensor.IsCollided == true)
        {
            _isGrounded = true;
        }
        else if (_isGrounded == true && _groundSensor.IsCollided == false)
        {
            _isGrounded = false;
        }

        _animator.SetBool(PlayerAnimator.Parameters.Grounded, _isGrounded);
    }

    private void Move()
    {
        float inputX = Input.GetAxis(Horizontal);
        _rigidbody2d.velocity = new Vector2(inputX * _speed, _rigidbody2d.velocity.y);
        FlipX(inputX);

        if (Mathf.Abs(inputX) > Mathf.Epsilon && _isGrounded == true)
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

    private void ToJump()
    {
        if (Input.GetAxis(Jump) > 0 && _isGrounded == true)
        {
            _animator.SetTrigger(PlayerAnimator.Parameters.Jump);
            _isGrounded = false;
            _animator.SetBool(PlayerAnimator.Parameters.Grounded, _isGrounded);
            _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, _jumpForce);
        }
    }
}
