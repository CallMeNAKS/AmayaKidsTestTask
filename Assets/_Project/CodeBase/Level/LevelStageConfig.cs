using CodeBase.Card;
using UnityEngine;

namespace CodeBase.Level
{
    [CreateAssetMenu(menuName = "Data/LevelStage", fileName = "LevelStage", order = 0)]
    public class LevelStageConfig : ScriptableObject
    {
        [field: SerializeField] public int Rows { get; private set; }
        [field: SerializeField] public int Columns { get; private set; }
        [field: SerializeField] public CardPack CardPack { get; private set; }
    }
}