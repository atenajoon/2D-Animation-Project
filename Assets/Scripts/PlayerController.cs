using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Get access to the PlayerActionControls to detect the inputs. this field will contain the actions wrapper instance
    private PlayerActionControls playerActionControls;
    private Rigidbody2D _rigidbody;
    private bool _isGrounded;

    [SerializeField] private float _moveSpeed, _jumpForce;
    
    void Awake()
    {
        // Instantiate the actions wrapper class
        playerActionControls = new PlayerActionControls();
        _rigidbody = GetComponent<Rigidbody2D>();
        _isGrounded = false;

        // For the "jump" action, we add a callback method for the "performed" phase
        playerActionControls.Land.Jump.performed += OnJump;
    }

    void Update()
    {
        // Read the movement input value each frame
        float movementInput = playerActionControls.Land.Move.ReadValue<float>();

        // Multiply the input value by the movement speed and deltaTime
        float horizontalMovement = movementInput * _moveSpeed * Time.deltaTime;

        // Move the player using Translate for smoother movement
        transform.Translate(new Vector3(horizontalMovement, 0, 0));
    }


    private void OnJump(InputAction.CallbackContext context)
    {
        // This is the "jump" action callback method
        if(_isGrounded)
        {
            _rigidbody.AddForce(new Vector2(0f, _jumpForce), ForceMode2D.Impulse);
            _isGrounded = false;
            Debug.Log("jump");
        }
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
