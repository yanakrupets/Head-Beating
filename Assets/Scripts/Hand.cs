using System.Collections.Generic;
using System.Linq;
using Enums;
using Serializable;
using UnityEngine;
using UnityEngine.VFX;

public class Hand : MonoBehaviour
{
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Charge = Animator.StringToHash("Charge");
    
    private const string DummyTag = "Dummy";
    
    [SerializeField] private Animator animator;
    [SerializeField] private Collider handCollider;
    [SerializeField] private VisualEffect visualEffect;
    [SerializeField] private HitData[] hitData;

    private Dictionary<HitType, HitData> _hitDataDictionary;
    private HitType _currentHitType;

    private void Awake()
    {
        _hitDataDictionary = hitData.ToDictionary(x => x.HitType);
    }

    public void Hit()
    {
        _currentHitType = HitType.Normal;
        handCollider.enabled = true;
        StopCharge();
        animator.SetTrigger(Attack);
    }

    public void ChargedHit()
    {
        _currentHitType = HitType.Charged;
        handCollider.enabled = true;
        StopCharge();
        animator.SetTrigger(Attack);
    }
    
    public void StartCharge()
    {
        animator.SetBool(Charge, true);
    }

    public void StopCharge()
    {
        animator.SetBool(Charge, false);
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

    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag(DummyTag) && collision.gameObject.TryGetComponent<Dummy>(out var dummy)) 
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
