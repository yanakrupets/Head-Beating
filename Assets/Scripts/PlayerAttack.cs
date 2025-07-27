using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Hands References")]
    [SerializeField] private Hand rightHand;
    [SerializeField] private Hand leftHand;
    
    [Header("Settings")]
    [SerializeField] private float chargeDuration = 2f;
    [SerializeField] private float clickThreshold = 0.1f;
    [SerializeField] private Vector2 rotationSpeedRange;
    [SerializeField] private float rotationAngleY = 20f;
    [SerializeField] private float returnSpeed = 15f;
    
    [Header("Input Action")]
    [SerializeField] private InputActionReference attackActionReference;
    
    private Hand _currentHand;
    private float _pressTime;
    private bool _isCharging;
    private bool _isFullCharge;
    
    private Quaternion _initialRotation;
    private Quaternion _targetRotation;
    private bool _isReturning;

    private void Start()
    {
        _currentHand = rightHand;
    }

    private void OnEnable()
    {
        attackActionReference.action.performed += HandleAttackStart;
        attackActionReference.action.canceled += HandleAttackStop;
        
        attackActionReference.action.Enable();
    }

    private void OnDisable()
    {
        attackActionReference.action.performed -= HandleAttackStart;
        attackActionReference.action.canceled -= HandleAttackStop;
        
        attackActionReference.action.Disable();
    }
    
    private void HandleAttackStart(InputAction.CallbackContext context)
    {
        _pressTime = Time.time;
        _isReturning = false;
        _initialRotation = transform.rotation;
    }
    
    private void HandleAttackStop(InputAction.CallbackContext context)
    {
        var pressDuration = Time.time - _pressTime;
        
        if (pressDuration < clickThreshold && !_isCharging)
        { 
            ExecuteNormalAttack();
        }
        else if (_isCharging)
        {
            _isCharging = false;
            _isReturning = true;
            _currentHand.StopCharge();
        }
        else if (_isFullCharge)
        {
            _isFullCharge = false;
            _isReturning = true;
            ExecuteChargedAttack();
        }
    }
    
    private void Update()
    {
        HandleRotation();
        
        if (!_isCharging && attackActionReference.action.IsPressed() && 
            Time.time - _pressTime >= clickThreshold && !_isFullCharge)
        {
            StartCharging();
        }
        
        if (_isCharging && Time.time - _pressTime >= chargeDuration && !_isFullCharge)
        {
            _isCharging = false;
            _isFullCharge = true;
        }
    }
    
    private void HandleRotation()
    {
        if (_isCharging)
        {
            var chargeProgress = Mathf.Clamp01((Time.time - _pressTime) / chargeDuration);
            var currentSpeed = Mathf.Lerp(rotationSpeedRange.x, rotationSpeedRange.y, chargeProgress);
            
            var rotationDirection = _currentHand == leftHand ? -1f : 1f;
            _targetRotation = _initialRotation * Quaternion.Euler(0, rotationAngleY * rotationDirection, 0);
            
            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, currentSpeed * Time.deltaTime);
        }
        else if (_isReturning)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, _initialRotation, returnSpeed * Time.deltaTime);
            _isReturning = Quaternion.Angle(transform.rotation, _initialRotation) >= 0.1f;
        }
    }
    
    private void StartCharging()
    {
        _isCharging = true;
        _currentHand.StartCharge();
    }

    private void ExecuteNormalAttack()
    {
        _currentHand.Hit();
        ChangeHand();
    }
    
    private void ExecuteChargedAttack()
    {
        _currentHand.ChargedHit();
        ChangeHand();
    }

    private void ChangeHand()
    {
        _currentHand = _currentHand == leftHand ? rightHand : leftHand;
    }
}
