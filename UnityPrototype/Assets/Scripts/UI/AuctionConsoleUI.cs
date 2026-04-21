using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using StorageWars.Auction;

namespace StorageWars.UI
{
    public class AuctionConsoleUI : MonoBehaviour
    {
        [SerializeField] private AuctionManager auctionManager;

        private void Awake()
        {
            auctionManager.OnRoundStarted += HandleRoundStarted;
            auctionManager.OnBidsUpdated += HandleBidsUpdated;
            auctionManager.OnAuctionEnded += HandleAuctionEnded;
        }

        private void Start()
        {
            auctionManager.StartAuction();
        }

        public void SubmitBid(string rawInput)
        {
            if (string.IsNullOrWhiteSpace(rawInput))
            {
                auctionManager.HoldPlayerBid();
                return;
            }

            if (!int.TryParse(rawInput, out var bid))
            {
                Debug.LogWarning("Invalid bid number.");
                return;
            }

            var accepted = auctionManager.TryPlacePlayerBid(bid);
            if (!accepted)
                Debug.LogWarning("Bid rejected: must be >= current high bid and <= bankroll.");
        }

        private void HandleRoundStarted(int round, string clue, float multiplier)
        {
            Debug.Log($"Round {round} started. Clue: {clue}. Need {multiplier:0.00}x over 2nd place.");
        }

        private void HandleBidsUpdated(List<BidderState> bidders)
        {
            var lines = bidders.Select(b => $"{b.bidderId}: ${b.currentBid}");
            Debug.Log("Bids => " + string.Join(" | ", lines));
        }

        private void HandleAuctionEnded(BidderState winner)
        {
            Debug.Log($"Auction ended. Winner: {winner.bidderId} at ${winner.currentBid}");

            if (!winner.isPlayer)
                return;

            var rewards = auctionManager.GenerateLootRewards();
            var total = rewards.Sum(x => x.finalValue);
            Debug.Log($"Player rewards total value: ${total}");
        }

        private void OnDestroy()
        {
            auctionManager.OnRoundStarted -= HandleRoundStarted;
            auctionManager.OnBidsUpdated -= HandleBidsUpdated;
            auctionManager.OnAuctionEnded -= HandleAuctionEnded;
        }
    }
}
