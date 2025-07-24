using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 10f;
    
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    [Header("Input Action Reference")]
    [SerializeField] private InputActionReference moveActionReference;
    
    private CharacterController _controller;
    private Vector3 _velocity;
    private bool _isGrounded;
    private Vector2 _moveInput;

    private const float Gravity = -9.81f;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        moveActionReference.action.performed += HandleMovementInput;
        moveActionReference.action.canceled += HandleMovementInput;
        
        moveActionReference.action.Enable();
    }

    private void OnDisable()
    {
        moveActionReference.action.performed -= HandleMovementInput;
        moveActionReference.action.canceled -= HandleMovementInput;
        
        moveActionReference.action.Disable();
    }
    
    private void HandleMovementInput(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        var movement = (transform.right * _moveInput.x) + (transform.forward * _moveInput.y);
        _controller.Move(movement * (moveSpeed * Time.deltaTime));

        _velocity.y += Gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }
}
