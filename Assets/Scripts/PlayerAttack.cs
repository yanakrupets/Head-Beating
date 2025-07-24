using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Hands References")]
    [SerializeField] private Hand rightHand;
    [SerializeField] private Hand leftHand;
    
    [Header("Input Settings")]
    [SerializeField] private InputActionReference attackActionReference;
    
    private Hand _lastUsedHand;
    
    private void OnEnable()
    {
        attackActionReference.action.performed += HandleAttackInput;
        
        attackActionReference.action.Enable();
    }

    private void OnDisable()
    {
        attackActionReference.action.performed -= HandleAttackInput;
        
        attackActionReference.action.Disable();
    }
    
    private void HandleAttackInput(InputAction.CallbackContext context)
    {
        if (_lastUsedHand == leftHand)
        {
            rightHand.Hit();
            _lastUsedHand = rightHand;
        }
        else
        {
            leftHand.Hit();
            _lastUsedHand = leftHand;
        }
    }
}
