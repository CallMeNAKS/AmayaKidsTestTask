using UnityEngine;

namespace CodeBase.Card
{
    [CreateAssetMenu(menuName = "Data/Card/CardPack", fileName = "CardPack", order = 0)]
    public class CardPack : ScriptableObject
    {
        [System.Serializable]
        public struct Card
        {
            public string Name;
            public Sprite ItemImage;
        }

        [field: SerializeField] public Card[] Cards { get; private set; }
        [field: SerializeField] public string QuestDescription { get; private set; }
    }
}