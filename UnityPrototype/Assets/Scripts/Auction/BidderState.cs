namespace StorageWars.Auction
{
    [System.Serializable]
    public class BidderState
    {
        public string bidderId;
        public int bankroll;
        public int currentBid;
        public bool isPlayer;

        public BidderState(string bidderId, int bankroll, bool isPlayer)
        {
            this.bidderId = bidderId;
            this.bankroll = bankroll;
            this.isPlayer = isPlayer;
            currentBid = 0;
        }
    }
}
