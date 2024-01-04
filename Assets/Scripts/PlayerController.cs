using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // getting access to the PlayerActionControls to detect the inputs. this field will contain the actions wrapper instance
    private PlayerActionControls playerActionControls;

    [SerializeField] private float _moveSpeed, _jumpForce;
    
    void Awake()
    {
        // instantiate the actions wrapper class
        playerActionControls = new PlayerActionControls();

        // for the "jump" action, we add a callback method for the "performed" phase
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
        // this is the "jump" action callback method
        Debug.Log("jump");
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
