using UnityEngine;
using UnityEngine.VFX;

public class VFXController : MonoBehaviour
{
    [SerializeField] private VisualEffect visualEffect;
    [SerializeField] private float radius;
    
    [SerializeField] private BubbleColorData bubbleColorData;

    public void Play()
    {
        visualEffect.Stop();
        visualEffect.Reinit();
        
        SetRandomPosition();
        SetRandomSetting();
        
        visualEffect.Play();
    }

    private void SetRandomPosition()
    {
        var randomPoint = Random.insideUnitCircle.normalized * radius * Mathf.Sqrt(Random.value);
        visualEffect.transform.localPosition = randomPoint;
    }

    private void SetRandomSetting()
    {
        var set = bubbleColorData.GetRandomSet();
        visualEffect.SetVector4("Color", set.color);
        visualEffect.SetVector4("Dots Color", set.dotsColor);
        
        var textColor = bubbleColorData.GetRandomTextColor();
        visualEffect.SetVector4("Text Color", textColor);
    }
}
