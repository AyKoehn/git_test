using UnityEngine;

namespace StorageWars.Auction
{
    [CreateAssetMenu(fileName = "AuctionRules", menuName = "StorageWars/Auction Rules")]
    public class AuctionRules : ScriptableObject
    {
        [Tooltip("Round index 1-5 maps to required lead multiplier.")]
        public float[] roundLeadMultipliers = { 2.00f, 1.75f, 1.50f, 1.30f, 1.15f };

        [TextArea] public string[] roundClues =
        {
            "Unit size + neighborhood quality + 1 vague tag",
            "2 partial objects + dust/age estimate",
            "Category hint",
            "Condition trend + odor/moisture risk",
            "One high-confidence hint"
        };

        public float MultiplierForRound(int round) => roundLeadMultipliers[Mathf.Clamp(round - 1, 0, roundLeadMultipliers.Length - 1)];
        public string ClueForRound(int round) => roundClues[Mathf.Clamp(round - 1, 0, roundClues.Length - 1)];
    }
}
