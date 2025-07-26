using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.VFX;

public class VFXController : MonoBehaviour
{
    [SerializeField] private VisualEffect visualEffect;
    [SerializeField] private float radius;
    
    [FormerlySerializedAs("bubbleColorData")] [SerializeField] private BubbleData bubbleData;

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
        var set = bubbleData.GetRandomSet();
        visualEffect.SetVector4("Color", set.color);
        visualEffect.SetVector4("Dots Color", set.dotsColor);
        
        var textColor = set.GetRandomTextColor();
        visualEffect.SetVector4("Text Color", textColor);
        
        visualEffect.SetTexture("Bubble Texture2D", bubbleData.GetRandomBubbleTexture());
        visualEffect.SetTexture("Text Texture2D", bubbleData.GetRandomTextTexture());
    }
}
