using System.Collections.Generic;
using UnityEngine;
using StorageWars.Data;

namespace StorageWars.Loot
{
    [CreateAssetMenu(fileName = "LootTable", menuName = "StorageWars/Loot Table")]
    public class LootTable : ScriptableObject
    {
        public List<LootItemData> items = new();
        public List<RarityConfig> rarityConfigs = new();

        public RolledLoot Roll(System.Random random, float mapValueModifier, float mapRareBonus)
        {
            var item = items[random.Next(items.Count)];
            var rarity = RollRarity(random, mapRareBonus, out var multiplier);
            var condition = Mathf.Lerp(0.65f, 1.30f, (float)random.NextDouble());
            var finalValue = Mathf.RoundToInt(item.baseValue * multiplier * condition * mapValueModifier);

            return new RolledLoot
            {
                item = item,
                rarity = rarity,
                finalValue = finalValue
            };
        }

        private RarityTier RollRarity(System.Random random, float mapRareBonus, out float valueMultiplier)
        {
            valueMultiplier = 1f;
            float total = 0f;
            for (var i = 0; i < rarityConfigs.Count; i++)
            {
                var r = rarityConfigs[i];
                var adjusted = r.dropRate;
                if (r.tier >= RarityTier.Rare)
                    adjusted += mapRareBonus;
                total += adjusted;
            }

            var roll = (float)random.NextDouble() * total;
            float cumulative = 0f;
            for (var i = 0; i < rarityConfigs.Count; i++)
            {
                var r = rarityConfigs[i];
                var adjusted = r.dropRate;
                if (r.tier >= RarityTier.Rare)
                    adjusted += mapRareBonus;

                cumulative += adjusted;
                if (roll <= cumulative)
                {
                    valueMultiplier = r.valueMultiplier;
                    return r.tier;
                }
            }

            var fallback = rarityConfigs[rarityConfigs.Count - 1];
            valueMultiplier = fallback.valueMultiplier;
            return fallback.tier;
        }
    }
}
