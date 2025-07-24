using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Collider handCollider;

    public void Hit()
    {
        handCollider.enabled = true;
        animator.SetTrigger("Attack");
    }
    
    public void OnAnimationEnd()
    {
        handCollider.enabled = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dummy") && other.TryGetComponent<Dummy>(out var dummy))
        {
            dummy.TakeHit();
        }
    }
}
