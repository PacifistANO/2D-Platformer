using System;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(InputPlayer))]
[RequireComponent (typeof(PlayerGroundSensor))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;
    [SerializeField] private float _jumpForce = 7.5f;

    private PlayerGroundSensor _groundSensor;
    private Animator _animator;
    private Rigidbody2D _rigidbody2d;
    private InputPlayer _controlPlayer;
    private bool _isGrounded = false;
    private bool _isPreparedToClimb = false;
    private float _delayToIdle = 0.0f;
    private int _gravityScaleOn = 1;
    private int _gravityScaleOff = 0;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _controlPlayer = GetComponent<InputPlayer>();
        _groundSensor = GetComponent<PlayerGroundSensor>(); 
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
        bool direction = inputX < 0;
        transform.rotation = Quaternion.Euler(0, 180 * Convert.ToInt32(direction), 0);
    }

    private void ConfirmGroundCollision()
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
        if (_isPreparedToClimb == true)
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
