using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;
    [SerializeField] private float _jumpForce = 7.5f;
    [SerializeField] private GameObject _ladder;

    private Animator _animator;
    private Rigidbody2D _rigidbody2d;
    private GroundSensor_Player _groundSensor;
    private bool _isGrounded = false;
    private float _delayToIdle = 0.0f;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _groundSensor = transform.Find("GroundSensor").GetComponent<GroundSensor_Player>();
    }

    private void Update()
    {
        GroundCheck();

        float inputX = Input.GetAxis("Horizontal");

        Move(inputX);
        Jump();

        _animator.SetFloat(PlayerAnimatorController.Parameters.AirSpeedY, _rigidbody2d.velocity.y);
    }

    private void FlipX(float inputX)
    {
        if (inputX > 0)
            GetComponent<SpriteRenderer>().flipX = false;
        else if (inputX < 0)
            GetComponent<SpriteRenderer>().flipX = true;
    }

    private void GroundCheck()
    {
        if (!_isGrounded && _groundSensor.State())
        {
            _isGrounded = true;
            _animator.SetBool(PlayerAnimatorController.Parameters.Grounded, _isGrounded);
        }

        if (_isGrounded && !_groundSensor.State())
        {
            _isGrounded = false;
            _animator.SetBool(PlayerAnimatorController.Parameters.Grounded, _isGrounded);
        }
    }

    private void Move(float inputX)
    {
        _rigidbody2d.velocity = new Vector2(inputX * _speed, _rigidbody2d.velocity.y);
        FlipX(inputX);

        if (Mathf.Abs(inputX) > Mathf.Epsilon && _isGrounded)
        {
            _delayToIdle = 0.05f;
            _animator.SetInteger(PlayerAnimatorController.Parameters.AnimState, 1);
        }

        else
        {
            _delayToIdle -= Time.deltaTime;

            if (_delayToIdle < 0)
                _animator.SetInteger(PlayerAnimatorController.Parameters.AnimState, 0);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown("space") && _isGrounded)
        {
            _animator.SetTrigger(PlayerAnimatorController.Parameters.Jump);
            _isGrounded = false;
            _animator.SetBool(PlayerAnimatorController.Parameters.Grounded, _isGrounded);
            _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, _jumpForce);
            _groundSensor.Disable(0.2f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == _ladder.tag && _isGrounded)
        {
            _rigidbody2d.gravityScale = 0;
            float inputY = Input.GetAxis("Vertical");
            _rigidbody2d.velocity = new Vector2(0, inputY * _speed);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == _ladder.tag)
            _rigidbody2d.gravityScale = 1;
    }
}
