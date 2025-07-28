using Enums;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.VFX;

public class HitVFXController : MonoBehaviour
{
    [SerializeField] private VisualEffect hitVisualEffect;
    [SerializeField] private VisualEffect chargedHitVisualEffect;
    [SerializeField] private float radius;
    
    [Space]
    [SerializeField] private BubbleData bubbleData;

    public void Play(HitType hitType)
    {
        switch (hitType)
        {
            case HitType.Normal:
                PlayHit();
                break;
            case HitType.Charged:
                PlayChargedHit();
                break;
        }
    }
    
    private void PlayHit()
    {
        hitVisualEffect.Stop();
        hitVisualEffect.Reinit();
        
        SetRandomHitPosition();
        SetRandomHitSetting();
        
        hitVisualEffect.Play();
    }
    
    private void PlayChargedHit()
    {
        chargedHitVisualEffect.Stop();
        chargedHitVisualEffect.Reinit();

        SetRandomChargedHitSetting();
        
        chargedHitVisualEffect.Play();
    }

    private void SetRandomHitPosition()
    {
        var randomPoint = Random.insideUnitCircle.normalized * radius * Mathf.Sqrt(Random.value);
        hitVisualEffect.transform.localPosition = randomPoint;
    }

    private void SetRandomHitSetting()
    {
        var set = bubbleData.GetRandomSet();
        hitVisualEffect.SetVector4("Color", set.Color);
        hitVisualEffect.SetVector4("Dots Color", set.DotsColor);
        
        var textColor = set.GetRandomTextColor();
        hitVisualEffect.SetVector4("Text Color", textColor);
        
        hitVisualEffect.SetTexture("Bubble Texture2D", bubbleData.GetRandomBubbleTexture());
        hitVisualEffect.SetTexture("Text Texture2D", bubbleData.GetRandomTextTexture());
    }
    
    private void SetRandomChargedHitSetting()
    {
        var set = bubbleData.GetRandomSet();
        chargedHitVisualEffect.SetVector4("Color", set.Color);
        chargedHitVisualEffect.SetVector4("Dots Color", set.DotsColor);
    }
}
