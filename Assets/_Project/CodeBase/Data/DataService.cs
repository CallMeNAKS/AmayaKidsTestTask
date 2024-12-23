using CodeBase.Level;
using UnityEngine;

namespace _Project.CodeBase.Data
{
    public class DataService : MonoBehaviour, IDataService
    {
        [field: SerializeField] public LevelData LevelData { get; private set; }
    }
}