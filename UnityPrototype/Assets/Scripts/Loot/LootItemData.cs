using UnityEngine;
using StorageWars.Data;

namespace StorageWars.Loot
{
    [CreateAssetMenu(fileName = "LootItemData", menuName = "StorageWars/Loot Item")]
    public class LootItemData : ScriptableObject
    {
        public string itemName;
        public int baseValue = 1000;
        public Sprite icon;
        public string[] tags;
    }

    [System.Serializable]
    public struct RolledLoot
    {
        public LootItemData item;
        public RarityTier rarity;
        public int finalValue;
    }
}
