using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    // private Vector2 _movementInput; // target position
    // private Vector2 _smoothedMovementInput; // current position
    // private Vector2 _movementInputSmoothedVelocity; // current velocity, which is modified every time the function is called
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    private bool _isJumping;
    private float _moveHorizontal;
    private float _moveVertical;
    public Animator _animator;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
        void Start()
    {
        _isJumping = false;
    }

    private void Update()
    {
        // this gives us a number (-1 or 1) on keypress to indicate moving directions
        _moveHorizontal = Input.GetAxisRaw("Horizontal");
        _moveVertical = Input.GetAxisRaw("Vertical");

        
    }
    private void FixedUpdate()
    {
        // Rigidbody.AddForce(Vector2 Force for x & y axis, ForceMode);
        if(_moveHorizontal > 0 || _moveHorizontal < 0)
        {
            _rigidbody.AddForce(new Vector2(_moveHorizontal * _moveSpeed, 0f), ForceMode2D.Impulse);
            _animator.SetFloat("MoveSpeed", Mathf.Abs(_moveHorizontal * _moveSpeed));
        } else 
        {
            _animator.SetFloat("MoveSpeed", 0); 
        }

        if(!_isJumping && _moveVertical > 0)
        {
            _rigidbody.AddForce(new Vector2(0f, _moveVertical * _jumpForce), ForceMode2D.Impulse);  
        }
        // Vector2.SmoothDamp(currentPosition, targetPosition, ref currentVelocity, smoothTime);
        // _smoothedMovementInput = Vector2.SmoothDamp(_smoothedMovementInput, _movementInput, ref _movementInputSmoothedVelocity, 0.1f);
        // _rigidbody.velocity = _smoothedMovementInput * _moveSpeed;
        // _rigidbody.velocity = _movementInput * _moveSpeed;

        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            _isJumping = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            _isJumping = true;
        }
    }
}
