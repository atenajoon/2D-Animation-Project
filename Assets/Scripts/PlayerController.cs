using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerActionControls playerActionControls; // Get access to the PlayerActionControls to detect the inputs. this field will contain the actions wrapper instance
    public Animator animator;
    public Transform _firePoint;
    public GameObject _bulletPrefab;
    private Rigidbody2D _rigidbody;
    private bool _isGrounded;
    private bool _playerFacingRight = true;
    private bool _isFiring = false; // equivalent to GetButtonDown()/GetButtonUp()
    private float _fireTimer = 0f; // Timer to control firing rate
    private float _fireRate = 0.1f; // Time delay between firing bullets
    [SerializeField] private float _moveSpeed, _jumpForce;
    
    void Awake()
    {
        // Instantiate the actions wrapper class
        playerActionControls = new PlayerActionControls();
        _rigidbody = GetComponent<Rigidbody2D>();
        _isGrounded = false;

        // For the jump & fire actions, adding a callback method for the "performed" phase
        playerActionControls.Land.Jump.performed += OnJump;

        playerActionControls.Land.Fire.performed += OnFire;
    }

    void Update()
    {
        OnMove();
    }

    void FixedUpdate()
    {
        // Check if enough time has passed to fire another bullet
        _fireTimer += Time.fixedDeltaTime;
    
        if (_fireTimer >= _fireRate)
        {
            if(_isFiring)
                Fire();

            _fireTimer = 0f; // Reset the timer
        }
    }


     void OnFire(InputAction.CallbackContext context)
     {
        if (context.performed)
        {
            _isFiring = !_isFiring;
            animator.SetBool("IsShooting", _isFiring);
        }
     }

    private void Fire()
    {
        // Instantiate(whatToSpawn, whereToSpawn, rotation)
        Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
    }
    private void OnMove()
    {
        // Read the movement input value each frame
        float movementInput = playerActionControls.Land.Move.ReadValue<float>();

        // Multiply the input value by the movement speed and deltaTime
        float horizontalMovement = movementInput * _moveSpeed * Time.deltaTime;

        // Move the player using Translate for smoother movement
        transform.Translate(new Vector3(horizontalMovement, 0, 0));

        // instead of changing the local scale in the Flip method I tried to change the rb.velocity, but the character wouldn't move anymore. Why?
        // _rigidbody.velocity = new Vector2(horizontalMovement, _rigidbody.velocity.y);

        // Flip the character sprite to the move direction
        if (movementInput > 0 && !_playerFacingRight)
        {
            Flip();
        }
        else if(movementInput < 0 && _playerFacingRight)
        {
            Flip();
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        // This is the "jump" action callback method
        if(_isGrounded)
        {
            _rigidbody.AddForce(new Vector2(0f, _jumpForce), ForceMode2D.Impulse);
            _isGrounded = false;
        }
    }

    private void Flip()
    {
        _playerFacingRight = !_playerFacingRight;
        // transform.Rotate(0f, 180f, 0f);

        // Flip the character by changing localScale.x
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
        
        // Flip the FirePoint direction
        float rotationValue = (_firePoint.rotation.y == 0) ? 180f : 0f;
        _firePoint.rotation = Quaternion.Euler(0f, rotationValue, 0f);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player is grounded when colliding with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void OnEnable()
    {
        playerActionControls.Enable();
    }

    private void OnDisable()
    {
        playerActionControls.Disable();
    }
}
