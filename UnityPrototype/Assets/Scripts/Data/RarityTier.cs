using UnityEngine;

namespace StorageWars.Data
{
    public enum RarityTier
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
        Mythic,
        Divine
    }

    [System.Serializable]
    public struct RarityConfig
    {
        public RarityTier tier;
        [Range(0f, 1f)] public float dropRate;
        public Color color;
        public float valueMultiplier;
    }
}
