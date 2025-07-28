using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Serializable
{
    [Serializable]
    public struct BubbleColorSet
    {
        [field: SerializeField] public Color Color { get; private set; }
        [field: SerializeField] public Color DotsColor { get; private set; }
        [field: SerializeField] public Color[] TextColors { get; private set; }
    
        public Color GetRandomTextColor()
        {
            return TextColors[Random.Range(0, TextColors.Length)];
        }
    }
}