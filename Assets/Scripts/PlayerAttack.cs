using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Hands References")]
    [SerializeField] private Hand rightHand;
    [SerializeField] private Hand leftHand;
    
    [Header("Attack Settings")]
    [SerializeField] private float chargeDuration = 2f;
    [SerializeField] private float clickThreshold = 0.1f;
    
    [Header("Input Action Reference")]
    [SerializeField] private InputActionReference attackActionReference;
    
    private Hand _currentHand;
    private float _pressTime;
    private bool _isCharging;
    private bool _isFullCharge;

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
            _currentHand.StopCharge();
        }
        else if (_isFullCharge)
        {
            _isFullCharge = false;
            _currentHand.FullCharge(_isFullCharge);
            ExecuteChargedAttack();
        }
    }
    
    private void Update()
    {
        if (IsReadyToCharge())
        {
            _isCharging = true;
            _currentHand.StartCharge();
        }
        
        if (IsFullCharged())
        {
            _isCharging = false;
            _isFullCharge = true;
            _currentHand.FullCharge(_isFullCharge);
        }
    }

    private bool IsReadyToCharge()
        => !_isCharging
           && attackActionReference.action.IsPressed()
           && Time.time - _pressTime >= clickThreshold
           && !_isFullCharge;

    private bool IsFullCharged()
        => _isCharging && Time.time - _pressTime >= chargeDuration && !_isFullCharge;

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
