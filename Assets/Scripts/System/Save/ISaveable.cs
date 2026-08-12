using UnityEngine;

namespace FarmJam2026
{
    public interface ISaveable
    {
        string Name { get; }
        void Save(ref SaveData data);
        void Load(SaveData data);
    }
}
