# git_test

did this wrong
Hello Odin!

## New
- `storage-wars-game-plan.md`: starter design for a Storage Wars-style multiplayer game idea.
- `storage_wars_game.py`: playable CLI prototype with 12 characters, 5-round auctions, map variety, and tiered loot rarity.
- `UnityPrototype/`: Unity C# scaffold for the full game architecture (auction loop, loot, data assets).

## Run the prototype
```bash
python3 storage_wars_game.py
```

## How to play
1. Run `python3 storage_wars_game.py`.
2. Pick a character by entering a number from **1 to 12**.
3. For each auction round (1-5), type your bid amount (numbers only, e.g. `150000`) or press Enter to hold.
4. To win early in a round, your top bid must be above the round multiplier threshold vs 2nd place:
   - Round 1: 2.00x
   - Round 2: 1.75x
   - Round 3: 1.50x
   - Round 4: 1.30x
   - Round 5: 1.15x
5. If no one closes out by multiplier in round 5, highest final bid wins.
6. If you win the unit, loot is revealed and your profit/loss is shown.

## Quick example
```bash
python3 storage_wars_game.py
# input example:
# 1
# 100000
# 140000
# 170000
# 200000
# 220000
```

## Unity full-game scaffold
Start with `UnityPrototype/README.md` for setup and architecture.
