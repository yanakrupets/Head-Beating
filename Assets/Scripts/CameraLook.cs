using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class CameraLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Transform playerTransform;
    
    [Header("Input Action Reference")]
    [SerializeField] private InputActionReference cameraActionReference;

    private float _xRotation;

    private void OnEnable()
    {
        cameraActionReference.action.performed += HandleCameraInput;
        
        cameraActionReference.action.Enable();
    }

    private void OnDisable()
    {
        cameraActionReference.action.performed -= HandleCameraInput;
        
        cameraActionReference.action.Disable();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void HandleCameraInput(InputAction.CallbackContext context)
    {
        var lookInput = context.ReadValue<Vector2>() * mouseSensitivity * Time.deltaTime;
        
        _xRotation -= lookInput.y;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        
        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        playerTransform.Rotate(Vector3.up, lookInput.x);
    }
}
