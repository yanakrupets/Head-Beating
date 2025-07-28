using Serializable;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Bubble Data", menuName = "Data/Bubble Data")]
    public class BubbleData : ScriptableObject
    {
        [SerializeField] private BubbleColorSet[] bubbleColorSets;
        [SerializeField] private Texture2D[] bubbleTextures;
        [SerializeField] private Texture2D[] textTextures;

        public BubbleColorSet GetRandomSet()
        {
            return bubbleColorSets[Random.Range(0, bubbleColorSets.Length)];
        }

        public Texture2D GetRandomBubbleTexture()
        {
            return bubbleTextures[Random.Range(0, bubbleTextures.Length)];
        }
    
        public Texture2D GetRandomTextTexture()
        {
            return textTextures[Random.Range(0, textTextures.Length)];
        }
    }
}