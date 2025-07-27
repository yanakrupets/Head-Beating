using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Collider handCollider;
    [SerializeField] private float hitForce = 10f;

    public void Hit()
    {
        handCollider.enabled = true;
        animator.SetTrigger("Attack");
    }
    
    public void OnAnimationEnd()
    {
        handCollider.enabled = false;
    }

    void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Dummy") && collision.gameObject.TryGetComponent<Dummy>(out var dummy)) 
        {
            var hitDirection = transform.position - collision.transform.position;
            dummy.TakeHit(hitDirection, hitForce);
            
            var contact = collision.contacts[0];
            if (collision.gameObject.TryGetComponent<DecalPainter>(out var decalPainter))
            {
                decalPainter.Paint(contact.point, contact.normal);
            }
        }
    }
}
