# Unity Prototype (Scaffold)

This folder contains a Unity-ready scaffold for turning the Storage Wars concept into a full game foundation.

## What's included
- `Assets/Scripts/Auction/`
  - `AuctionManager.cs`: 5-round auction flow, AI bids, round win checks using multiplier rules.
  - `AuctionRules.cs`: configurable round multipliers + clue strings.
  - `BidderState.cs`: runtime bidder model.
- `Assets/Scripts/Data/`
  - `CharacterData.cs`: ScriptableObject for playable characters.
  - `MapData.cs`: ScriptableObject for storage map types and modifiers.
  - `RarityTier.cs`: rarity enum + rarity config structure.
- `Assets/Scripts/Loot/`
  - `LootItemData.cs`: ScriptableObject for loot items.
  - `LootTable.cs`: weighted rarity roll + value generation.
- `Assets/Scripts/UI/`
  - `AuctionConsoleUI.cs`: minimal debug/console-style controller to drive the system.

## Setup in Unity
1. Create a new Unity 2022+ 3D project.
2. Copy the `Assets/Scripts` folder into your Unity project's `Assets` folder.
3. Create ScriptableObject assets:
   - **StorageWars/Auction Rules**
   - **StorageWars/Loot Table**
   - **StorageWars/Character** (create 12)
   - **StorageWars/Map** (create multiple map types)
   - **StorageWars/Loot Item** (create item database)
4. Populate `AuctionRules` multipliers as:
   - Round 1 = 2.00
   - Round 2 = 1.75
   - Round 3 = 1.50
   - Round 4 = 1.30
   - Round 5 = 1.15
5. Populate loot rarity configs:
   - Common 50.00%
   - Uncommon 27.00%
   - Rare 13.00%
   - Epic 6.00%
   - Legendary 2.50%
   - Mythic 1.30%
   - Divine 0.20% (rainbow holographic presentation in UI/shader)
6. Add an empty GameObject in scene with `AuctionManager` and `AuctionConsoleUI`.
7. Wire references in Inspector (rules, loot table, maps, manager).
8. Run Play Mode and call `SubmitBid` from your UI input hooks.

## Next steps toward full game
- Replace `AuctionConsoleUI` debug logs with real UI screens (character select, auction HUD, loot reveal, marketplace).
- Add multiplayer networking (Netcode for GameObjects, Mirror, or Photon).
- Implement character ability systems (cooldowns, effects, counters).
- Add persistence/economy backend (player inventory, seasons, anti-cheat).
