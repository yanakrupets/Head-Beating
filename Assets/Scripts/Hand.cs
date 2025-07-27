using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Hand : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Collider handCollider;
    [SerializeField] private VisualEffect visualEffect;
    [SerializeField] private HitData[] hitData;

    private Dictionary<HitType, HitData> _hitDataDictionary;
    private HitType _currentHitType;

    private void Awake()
    {
        _hitDataDictionary = new Dictionary<HitType, HitData>();
        foreach (var data in hitData)
        {
            _hitDataDictionary[data.HitType] = data;
        }
    }

    public void Hit()
    {
        _currentHitType = HitType.Normal;
        handCollider.enabled = true;
        StopCharge();
        animator.SetTrigger("Attack");
    }

    public void ChargedHit()
    {
        _currentHitType = HitType.Charged;
        handCollider.enabled = true;
        StopCharge();
        animator.SetTrigger("Attack");
    }
    
    public void StartCharge()
    {
        animator.SetBool("Charge", true);
    }

    public void StopCharge()
    {
        animator.SetBool("Charge", false);
    }

    public void FullCharge(bool isCharged)
    {
        if (isCharged)
        {
            visualEffect.Play();
        }
        else
        {
            visualEffect.Stop();
            visualEffect.Reinit();
        }
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
            dummy.TakeHit(hitDirection, _hitDataDictionary[_currentHitType]);
            
            var contact = collision.contacts[0];
            if (collision.gameObject.TryGetComponent<DecalPainter>(out var decalPainter))
            {
                decalPainter.Paint(contact.point, contact.normal);
            }
        }
    }
}
