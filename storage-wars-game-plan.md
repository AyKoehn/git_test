# Storage Wars-Style Multiplayer Game Plan

## Game Concept
Players bid against each other on abandoned storage units without seeing all items inside. Each auction match has **5 rounds**, where every round reveals stronger clues, pushes bids higher, and lowers the required lead multiplier for the top bidder.

## 12 Playable Characters (with Special Abilities)
Each character has one passive and one active ability to create distinct playstyles.

1. **Rex "The Shark" Dalton**
   - Passive: Starts each match with +5% bankroll.
   - Active: **Pressure Bid**: For one round, rivals pay a 3% fee on any bid increase.
2. **Maya "Microscope" Lin**
   - Passive: +10% clue clarity from all previews.
   - Active: **X-Ray Peek**: Reveal one hidden item category in a target unit.
3. **Tino "Quickflip" Morales**
   - Passive: +8% sale price on Common/Uncommon items.
   - Active: **Flash Liquidation**: Instantly sell 3 items at fair market value.
4. **Ivy "Cold Read" Voss**
   - Passive: Sees the last bid action delay of opponents (timing tell).
   - Active: **Poker Face**: Hide your next two bid actions from enemy indicators.
5. **Bruno "Heavy Hands" Kane**
   - Passive: Repairs are 15% cheaper.
   - Active: **Salvage Surge**: 20 seconds of boosted scrap value from junk pulls.
6. **Selene "Lucky Star" Aria**
   - Passive: +0.2% absolute chance for Rare+ drops.
   - Active: **Fortune Window**: Next opened crate has improved rarity odds.
7. **Dex "Accountant" Pierce**
   - Passive: Auction fees/taxes reduced by 10%.
   - Active: **Risk Audit**: Shows projected min/max profit range before final round bid.
8. **Nova "Bluff Queen" Reyes**
   - Passive: Opponents see your bid increments as slightly randomized values.
   - Active: **Fakeout**: Simulates a high bid jump (no bankroll spend) once per auction.
9. **Hank "Warehouse Dog" Turner**
   - Passive: +1 extra clue keyword every round.
   - Active: **Sniff Test**: Reveal item condition trend (poor/average/pristine).
10. **Zuri "Collector" Hale**
    - Passive: +12% value on antique and collectible tags.
    - Active: **Curator's Eye**: Mark one found item to guarantee top appraisal bracket.
11. **Orion "Tech Rat" Vega**
    - Passive: Cooldowns on abilities reduced by 15%.
    - Active: **Signal Jam**: Prevent one opponent from using actives this round.
12. **Kael "Last Call" Mercer**
    - Passive: If currently 2nd place in a bid, next bid gets a +5% effective strength.
    - Active: **Final Hammer**: In round 5, gain one protected rebid opportunity.

## Core Gameplay Loop
1. **Preview phase**: Players inspect storage units with limited visible information.
2. **Auction phase (5 rounds)**: Players place bids while clue quality improves each round.
3. **Win check**: Top bid must meet the round's required multiplier over 2nd place.
4. **Reveal phase**: Winning bidder opens crates/containers and receives loot by rarity.
5. **Sell phase**: Items sold to NPC buyers or listed on player marketplace.
6. **Progression**: Earn reputation, unlock maps, characters, cosmetics, and leagues.

## 5-Round Auction Rules + Required Lead Multipliers
To win in a given round, 1st place must exceed 2nd place by at least this multiplier:

- **Round 1:** **2.00x** (example: 100k beats 50k)
- **Round 2:** **1.75x**
- **Round 3:** **1.50x**
- **Round 4:** **1.30x**
- **Round 5:** **1.15x**

If multiplier is not met, bidding continues to the next round.

### Clue Progression by Round
- **Round 1 (Teaser):** Unit size, neighborhood quality, and 1 vague tag.
- **Round 2 (Surface):** 2 partially visible objects and dust/age estimate.
- **Round 3 (Pattern):** 1 item category hint (e.g., electronics/furniture/collectibles).
- **Round 4 (Condition):** Condition trend (poor/average/pristine) and odor/moisture risk.
- **Round 5 (Specific):** 1 high-confidence hint (brand/era/material) before final bids.

## Multiplayer Modes
- **Live Auction (4-12 players):** Timed real-time bidding and bluffing.
- **League Seasons:** Ranked points based on net profit, efficiency, and placement.
- **Private Rooms:** Friends-only matches with custom rule presets.

## Storage Unit Maps / Types
Multiple map themes keep strategy fresh with different risk profiles:

1. **Urban Lockup (City)**: High competition, medium loot variance.
2. **Suburban Garage Row**: Balanced risk/reward, frequent furniture/appliances.
3. **Industrial Depot**: Heavy machinery parts, high repair costs, big jackpots.
4. **Coastal Container Yard**: Water damage risk, rare imported collectibles.
5. **Desert Hangars**: Low moisture, high antique probability.
6. **Mountain Facilities**: Outdoor wear goods, sports/hunting collectibles.
7. **Luxury Vault Annex**: Very high entry fees, best Legendary+ potential.
8. **Abandoned Mall Storage**: Fashion/electronics mix, volatile resale prices.

## Item Rarity Tiers, Drop Rates, and Colors
Item values are determined by rarity tier multipliers and condition.

| Tier | Drop Rate | Color | Value Multiplier (vs Common base) |
|---|---:|---|---:|
| Common | 50.00% | Gray `#9AA0A6` | 1.0x |
| Uncommon | 27.00% | Green `#34A853` | 1.8x |
| Rare | 13.00% | Blue `#4285F4` | 3.2x |
| Epic | 6.00% | Purple `#A142F4` | 6.5x |
| Legendary | 2.50% | Gold `#F9AB00` | 13.0x |
| Mythic | 1.30% | Crimson `#D93025` | 28.0x |
| Divine | 0.20% | Rainbow Holographic (animated shader) | 75.0x |

**Drop-rate balancing notes**
- Rates are global baselines; maps and abilities can apply small modifiers.
- Divine should remain extremely rare and protected by server-authoritative roll logic.
- Add pity-protection carefully (optional, high-level playlists only) to avoid economy inflation.

## Economy and Balance
- Include item classes: junk, collectibles, antiques, tools, electronics, memorabilia.
- Use weighted random generation + map-specific loot tables.
- Apply repair/cleaning fees to force meaningful profit decisions.
- Control inflation through listing fees, sink events, and dynamic NPC demand.

## Anti-Cheat and Fairness
- Server-authoritative auctions, ability casts, and loot roll outcomes.
- Validate all bid increments and enforce round multiplier rules server-side.
- Add anti-collusion detection for repeated coordinated bidding patterns.
- Matchmaking by level, MMR, and bankroll bracket.

## MVP Build (First Playable)
- 6-player live auction lobby.
- 5-round auction system with clue progression + multiplier checks.
- 12 playable characters with at least 1 passive + 1 active each.
- 4 launch maps from the map list above.
- 7-tier rarity table with colored UI presentation.
- Basic sell-to-NPC and simple progression (level/cash/rank).

## First 4 Milestones
1. **Auction Core**: Build round system, timers, multiplier win checks, and networking.
2. **Characters + Abilities**: Implement all 12 kits with cooldown/status systems.
3. **Loot + Economy**: Add rarity tiers, map loot tables, valuation, and selling loop.
4. **Content + Ranked**: Expand maps, tune balance, and launch seasonal matchmaking.
