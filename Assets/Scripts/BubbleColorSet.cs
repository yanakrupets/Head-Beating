using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct BubbleColorSet
{
    public Color color;
    public Color dotsColor;
    public Color[] textColors;
    
    public Color GetRandomTextColor()
    {
        return textColors[Random.Range(0, textColors.Length)];
    }
}