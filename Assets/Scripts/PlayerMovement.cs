using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _movementInput; // target position
    // private Vector2 _smoothedMovementInput; // current position
    // private Vector2 _movementInputSmoothedVelocity; // current velocity, which is modified every time the function is called
    [SerializeField] private float _speed;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        // _rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }
    private void FixedUpdate()
    {
        // Vector2.SmoothDamp(currentPosition, targetPosition, ref currentVelocity, smoothTime);
        // _smoothedMovementInput = Vector2.SmoothDamp(_smoothedMovementInput, _movementInput, ref _movementInputSmoothedVelocity, 0.1f);
        // _rigidbody.velocity = _smoothedMovementInput * _speed;
        _rigidbody.velocity = _movementInput * _speed;
    }
    private void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();
    }
}
