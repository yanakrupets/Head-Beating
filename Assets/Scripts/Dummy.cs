using Serializable;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    private static readonly int Property = Animator.StringToHash("Take Hit");
    
    [SerializeField] private Animator animator;
    [SerializeField] private HitVFXController hitVFXController;
    [SerializeField] private SoundController soundController;
    [SerializeField] private Rigidbody rb;
    
    [Header("Movement Settings")]
    [SerializeField] private float returnSpeed = 5f;
    [SerializeField] private float damping = 2f;
    [SerializeField] private float rotationResponseFactor = 0.1f;
    
    private Vector3 _originalPosition;
    private Quaternion _originalRotation;

    private void Start() 
    {
        _originalPosition = transform.position;
        _originalRotation = transform.rotation;
    }
    
    private void FixedUpdate() 
    {
        var force = (_originalPosition - transform.position) * returnSpeed;
        rb.AddForce(force - rb.velocity * damping);
    
        var deltaRot = _originalRotation * Quaternion.Inverse(transform.rotation);
        deltaRot.ToAngleAxis(out var angle, out var axis);
        rb.AddTorque(axis.normalized * (angle * rotationResponseFactor) - rb.angularVelocity * damping);
    }
    
    public void TakeHit(Vector3 hitDirection, HitData data)
    {
        rb.AddForce(hitDirection.normalized * data.Force, ForceMode.Impulse);
        
        rb.AddTorque(new Vector3(
            Random.Range(-30f, 30f),
            Random.Range(-30f, 30f),
            Random.Range(-30f, 30f)) * data.Force * rotationResponseFactor);
        
        soundController.PlayRandomSound();
        animator.SetTrigger(Property);
        hitVFXController.Play(data.HitType);
    }
}
