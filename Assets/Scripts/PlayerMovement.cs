using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    private bool _playerFacingRight = true;
    private bool _isJumping;
    private float _moveHorizontal;
    private float _moveVertical;
    public Animator animator;


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
        // I might not need to use it if I improve my movement code with Unity New Input System
        _moveHorizontal = Input.GetAxisRaw("Horizontal");
        _moveVertical = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        Move();  
    }

    private void Move()
    {
        // might want to change it to Moving the character by finding the target velocity
        // with: https://github.com/Brackeys/2D-Character-Controller/blob/master/CharacterController2D.cs
        if(_moveHorizontal > 0 || _moveHorizontal < 0)
        {
            // I might not need to use it if I improve my movement code with Unity New Input System??
            // for now, >> Rigidbody.AddForce(Vector2 Force for x & y axis, ForceMode);
            _rigidbody.AddForce(new Vector2(_moveHorizontal * _moveSpeed, 0f), ForceMode2D.Impulse);
            animator.SetFloat("MoveSpeed", Mathf.Abs(_moveHorizontal * _moveSpeed));

            if (_moveHorizontal > 0 && !_playerFacingRight)
            {
                Flip();
            }
            else if(_moveHorizontal < 0 && _playerFacingRight)
            {
                Flip();
            }

        // I need to change this: if is moving and shooting -> FirePoint Y gets reduced??
        //  GetComponent<Weapon>()._firePoint.transform.y = -0.70f;

        } 
        else 
        {
            animator.SetFloat("MoveSpeed", 0); 
        }

        if(!_isJumping && _moveVertical > 0)
        {
            // I might not need to use it if I improve my movement code with Unity New Input System??
            _rigidbody.AddForce(new Vector2(0f, _moveVertical * _jumpForce), ForceMode2D.Impulse);
        } 
    }

    private void Flip()
    {
        _playerFacingRight = !_playerFacingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            _isJumping = false;
            animator.SetBool("IsJumping", false);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            _isJumping = true;
            animator.SetBool("IsJumping", true);
        }
    }
}
