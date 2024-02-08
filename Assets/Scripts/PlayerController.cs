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
     public int health = 3;
    private bool _isGrounded = false;
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

    private void OnMove()
    {
        // Read the movement input value each frame
        float _movementInput = playerActionControls.Land.Move.ReadValue<float>();

        // Horizontal movement of the player character
        float horizontalMovement = _movementInput * _moveSpeed;
        _rigidbody.velocity = new Vector2(horizontalMovement, _rigidbody.velocity.y);

        // Flip the character sprite to the move direction
        if (_movementInput > 0 && !_playerFacingRight)
        {
            Flip();
        }
        else if(_movementInput < 0 && _playerFacingRight)
        {
            Flip();
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        // if (context.performed) {}
           

        // This is the "jump" action callback method
        if(_isGrounded)
        {
            _rigidbody.AddForce(new Vector2(0f, _jumpForce), ForceMode2D.Impulse);
            _isGrounded = false;
            animator.SetBool("IsJumping", true);
        }
    }

    void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Button action Type only has Performed phase, once at pressing the key and once at release
            // so I flip the _isFiring value on each Performed callback to simutale Started and Canceled callbacks
            _isFiring = !_isFiring;
            animator.SetBool("IsShooting", _isFiring);
        }
    }

    private void Flip()
    {
        _playerFacingRight = !_playerFacingRight;

        // Flip the character by changing localScale.x
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
        
        // Flip the FirePoint direction
        Vector3 firePointScale = _firePoint.localScale;
        firePointScale.x *= -1;
        _firePoint.localScale = firePointScale;
    }

    private void Fire()
    {
        // Determine the rotation based on the orientation of the fire point
        Quaternion bulletRotation = (_firePoint.localScale.x > 0) ? _firePoint.rotation : Quaternion.Euler(0f, 180f, 0f);

        // Instantiate(whatToSpawn, whereToSpawn, adjustedRotation)
        Instantiate(_bulletPrefab, _firePoint.position, bulletRotation);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player is grounded when colliding with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
            animator.SetBool("IsJumping", false);
        }

        // check if the player is hit by Enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // ** add a safeTime after each hit **
            // ** add Auch audio **
            if(health <= 0)
                Die();

            health--;
            Debug.Log("Health: " + health);
        }
    }

    private void Die()
    {
        Debug.Log("Player Died");
        Destroy(gameObject);
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
