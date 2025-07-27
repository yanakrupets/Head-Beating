using UnityEngine;

public class Dummy : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private VFXController vfxController;
    [SerializeField] private SoundController soundController;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float returnSpeed = 5f;
    [SerializeField] private float damping = 2f;
    
    private Vector3 _originalPosition;
    private Quaternion _originalRotation;

    void Start() 
    {
        rb = GetComponent<Rigidbody>();
        
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        
        _originalPosition = transform.position;
        _originalRotation = transform.rotation;
    }
    
    void FixedUpdate() 
    {
        Vector3 force = (_originalPosition - transform.position) * returnSpeed;
        rb.AddForce(force - rb.velocity * damping);
    
        Quaternion deltaRot = _originalRotation * Quaternion.Inverse(transform.rotation);
        deltaRot.ToAngleAxis(out var angle, out var axis);
        rb.AddTorque(axis.normalized * (angle * 0.1f) - rb.angularVelocity * damping);
    }
    
    public void TakeHit(Vector3 hitDirection, float hitForce)
    {
        rb.AddForce(hitDirection.normalized * hitForce, ForceMode.Impulse);
        
        rb.AddTorque(new Vector3(
            Random.Range(-30f, 30f),
            Random.Range(-30f, 30f),
            Random.Range(-30f, 30f)) * hitForce * 0.1f);
        
        soundController.PlayRandomSound();
        
        animator.SetTrigger("Take Hit");
        vfxController.Play();
    }
}
