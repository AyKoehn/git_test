using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using StorageWars.Data;
using StorageWars.Loot;

namespace StorageWars.Auction
{
    public class AuctionManager : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private AuctionRules rules;
        [SerializeField] private LootTable lootTable;
        [SerializeField] private List<MapData> maps;
        [SerializeField] private int itemsPerUnit = 4;

        [Header("Runtime")]
        [SerializeField] private int startingPlayerBankroll = 250000;
        [SerializeField] private int minAiBankroll = 180000;
        [SerializeField] private int maxAiBankroll = 280000;

        private readonly List<BidderState> _bidders = new();
        private readonly System.Random _random = new();

        public int CurrentRound { get; private set; }
        public MapData CurrentMap { get; private set; }

        public event Action<int, string, float> OnRoundStarted;
        public event Action<List<BidderState>> OnBidsUpdated;
        public event Action<BidderState> OnAuctionEnded;

        public void StartAuction()
        {
            CurrentMap = maps[_random.Next(maps.Count)];
            CurrentRound = 1;
            _bidders.Clear();

            _bidders.Add(new BidderState("PLAYER", startingPlayerBankroll, true));
            _bidders.Add(new BidderState("AI_AVA", _random.Next(minAiBankroll, maxAiBankroll), false));
            _bidders.Add(new BidderState("AI_JAX", _random.Next(minAiBankroll, maxAiBankroll), false));
            _bidders.Add(new BidderState("AI_MILO", _random.Next(minAiBankroll, maxAiBankroll), false));

            BeginRound();
        }

        public bool TryPlacePlayerBid(int amount)
        {
            var player = _bidders.First(b => b.isPlayer);
            var highest = _bidders.Max(b => b.currentBid);
            if (amount < highest || amount > player.bankroll)
                return false;

            player.currentBid = amount;
            SimulateAiBids();
            EndRoundCheck();
            return true;
        }

        public void HoldPlayerBid()
        {
            SimulateAiBids();
            EndRoundCheck();
        }

        public List<RolledLoot> GenerateLootRewards()
        {
            var rewards = new List<RolledLoot>();
            for (var i = 0; i < itemsPerUnit; i++)
                rewards.Add(lootTable.Roll(_random, CurrentMap.lootValueModifier, CurrentMap.rareDropBonus));
            return rewards;
        }

        private void BeginRound()
        {
            var clue = rules.ClueForRound(CurrentRound);
            var multiplier = rules.MultiplierForRound(CurrentRound);
            OnRoundStarted?.Invoke(CurrentRound, clue, multiplier);
            OnBidsUpdated?.Invoke(_bidders.OrderByDescending(b => b.currentBid).ToList());
        }

        private void SimulateAiBids()
        {
            foreach (var ai in _bidders.Where(b => !b.isPlayer))
            {
                var top = _bidders.Max(b => b.currentBid);
                var cap = Mathf.RoundToInt(ai.bankroll * UnityEngine.Random.Range(0.55f, 0.95f));
                if (cap <= top)
                    continue;

                var jump = _random.Next(3000, 30000);
                ai.currentBid = Math.Min(top + jump, cap);
            }
        }

        private void EndRoundCheck()
        {
            var ordered = _bidders.OrderByDescending(b => b.currentBid).ToList();
            OnBidsUpdated?.Invoke(ordered);

            var first = ordered[0];
            var second = ordered[1];
            var needed = Mathf.CeilToInt(second.currentBid * rules.MultiplierForRound(CurrentRound));
            var winner = first.currentBid >= needed && first.currentBid > 0;

            if (winner || CurrentRound >= 5)
            {
                OnAuctionEnded?.Invoke(first);
                return;
            }

            CurrentRound++;
            BeginRound();
        }
    }
}
