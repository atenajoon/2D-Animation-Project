using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform _firePoint;
    public GameObject _bulletPrefab;

    // Get access to the PlayerActionControls to detect the inputs. this field will contain the actions wrapper instance
    private PlayerActionControls playerActionControls;
    private Rigidbody2D _rigidbody;
    private bool _isGrounded;
    private bool _playerFacingRight = true;
    private bool _isFiring = false;
    private float fireTimer = 0f; // Timer to control firing rate
    public float fireRate = 0.1f; // Time delay between firing bullets
    public Animator animator;

    [SerializeField] private float _moveSpeed, _jumpForce;
    
    void Awake()
    {
        // Instantiate the actions wrapper class
        playerActionControls = new PlayerActionControls();
        _rigidbody = GetComponent<Rigidbody2D>();
        _isGrounded = false;

        // For the "jump" action, we add a callback method for the "performed" phase
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
        fireTimer += Time.fixedDeltaTime;
    
        if (fireTimer >= fireRate)
        {
            if(_isFiring)
                Fire();

            fireTimer = 0f; // Reset the timer
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
