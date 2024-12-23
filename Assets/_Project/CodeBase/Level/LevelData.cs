using UnityEngine;

namespace CodeBase.Level
{
    [CreateAssetMenu(menuName = "Data/LevelData", fileName = "LevelData", order = 0)]
    public class LevelData : ScriptableObject
    {
        [field: SerializeField] public LevelStageConfig[] Stages { get; private set; }
    }
}