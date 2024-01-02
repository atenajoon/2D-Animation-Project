using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // getting access to the PlayerActionControls to detect the inputs
    private PlayerActionControls playerActionControls;

    
    void Awake()
    {
        playerActionControls = new PlayerActionControls();
    }

    private void OnEnable()
    {
        playerActionControls.Enable();
    }

    private void OnDisable()
    {
        playerActionControls.Disable();
    }
    void Start()
    {
        
    }

    void Update()
    {
        // read the movement value
        // float movementInput = playerActionControls.Land.Move.ReadValue<float>();

        // Vector3 moveInput = playerActionControls.Land.Move.ReadValue<float>();
        // Debug.Log(sidewaysMoveInput);

        // float jumpInput = playerActionControls.Land.Jump.ReadValue<float>();
        // if (jumpInput == 1) 
        if(playerActionControls.Land.Jump.triggered)
        Debug.Log("jump");

        // float moveInput = playerActionControls.Land.Move.ReadValue<float>();
        // if (moveInput == 1) 
        
        if(playerActionControls.Land.Move.triggered)
        Debug.Log("move");

    }
}
