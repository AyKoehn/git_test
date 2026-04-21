using UnityEngine;

namespace StorageWars.Data
{
    [CreateAssetMenu(fileName = "MapData", menuName = "StorageWars/Map")]
    public class MapData : ScriptableObject
    {
        public string mapName;
        [TextArea] public string description;
        [Range(0.8f, 1.5f)] public float lootValueModifier = 1f;
        [Range(0f, 0.2f)] public float rareDropBonus = 0f;
    }
}
