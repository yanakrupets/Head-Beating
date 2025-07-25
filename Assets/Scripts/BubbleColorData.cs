using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Bubble Color Data", menuName = "Data/Bubble Color Data")]
public class BubbleColorData : ScriptableObject
{
    [SerializeField] private BubbleColorSet[] bubbleColorSets;
    [SerializeField] private Color[] textColors;

    public BubbleColorSet GetRandomSet()
    {
        return bubbleColorSets[Random.Range(0, bubbleColorSets.Length)];
    }

    public Color GetRandomTextColor()
    {
        return textColors[Random.Range(0, textColors.Length)];
    }
}