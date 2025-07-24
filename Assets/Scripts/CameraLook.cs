using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Transform playerTransform;
    
    [Header("Input Action Reference")]
    [SerializeField] private InputActionReference lookActionReference;

    private float _xRotation = 0f;

    private void OnEnable()
    {
        lookActionReference.action.performed += HandleLookInput;
        
        lookActionReference.action.Enable();
    }

    private void OnDisable()
    {
        lookActionReference.action.performed -= HandleLookInput;
        
        lookActionReference.action.Disable();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void HandleLookInput(InputAction.CallbackContext context)
    {
        var lookInput = context.ReadValue<Vector2>() * mouseSensitivity * Time.deltaTime;
        
        _xRotation -= lookInput.y;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        
        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        playerTransform.Rotate(Vector3.up, lookInput.x);
    }
}
