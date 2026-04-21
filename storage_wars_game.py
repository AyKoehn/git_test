#!/usr/bin/env python3
"""Storage Wars-style auction prototype (CLI).

Run:
  python3 storage_wars_game.py
"""

from __future__ import annotations

import random
from dataclasses import dataclass
from typing import Dict, List, Tuple

RARITY_TABLE = [
    ("Common", 0.50, "#9AA0A6", 1.0),
    ("Uncommon", 0.27, "#34A853", 1.8),
    ("Rare", 0.13, "#4285F4", 3.2),
    ("Epic", 0.06, "#A142F4", 6.5),
    ("Legendary", 0.025, "#F9AB00", 13.0),
    ("Mythic", 0.013, "#D93025", 28.0),
    ("Divine", 0.002, "Rainbow Holographic", 75.0),
]

ROUND_MULTIPLIERS = {1: 2.00, 2: 1.75, 3: 1.50, 4: 1.30, 5: 1.15}
ROUND_CLUES = {
    1: "Unit size + neighborhood quality + 1 vague tag",
    2: "2 partial objects + dust/age estimate",
    3: "Category hint (electronics/furniture/collectibles/etc.)",
    4: "Condition trend + odor/moisture risk",
    5: "One high-confidence hint (brand/era/material)",
}

MAP_TYPES = [
    "Urban Lockup",
    "Suburban Garage Row",
    "Industrial Depot",
    "Coastal Container Yard",
    "Desert Hangars",
    "Mountain Facilities",
    "Luxury Vault Annex",
    "Abandoned Mall Storage",
]

ITEM_BASE_VALUES = {
    "box of tools": 5_000,
    "vintage lamp": 12_000,
    "sealed electronics crate": 25_000,
    "antique trunk": 40_000,
    "sports memorabilia": 30_000,
    "designer wardrobe": 55_000,
    "collector comic set": 18_000,
    "mystery safe": 80_000,
}


@dataclass(frozen=True)
class Character:
    name: str
    passive: str
    active: str


CHARACTERS: List[Character] = [
    Character("Rex \"The Shark\" Dalton", "+5% starting bankroll", "Pressure Bid"),
    Character("Maya \"Microscope\" Lin", "+10% clue clarity", "X-Ray Peek"),
    Character("Tino \"Quickflip\" Morales", "+8% sale on low tiers", "Flash Liquidation"),
    Character("Ivy \"Cold Read\" Voss", "Opponent timing tells", "Poker Face"),
    Character("Bruno \"Heavy Hands\" Kane", "-15% repair costs", "Salvage Surge"),
    Character("Selene \"Lucky Star\" Aria", "+0.2% Rare+ chance", "Fortune Window"),
    Character("Dex \"Accountant\" Pierce", "-10% auction fees", "Risk Audit"),
    Character("Nova \"Bluff Queen\" Reyes", "Obfuscated bid increments", "Fakeout"),
    Character("Hank \"Warehouse Dog\" Turner", "+1 clue keyword/round", "Sniff Test"),
    Character("Zuri \"Collector\" Hale", "+12% collectible value", "Curator's Eye"),
    Character("Orion \"Tech Rat\" Vega", "-15% ability cooldown", "Signal Jam"),
    Character("Kael \"Last Call\" Mercer", "+5% effective bid from 2nd", "Final Hammer"),
]


@dataclass
class ItemRoll:
    name: str
    rarity: str
    color: str
    value: int


def choose_character() -> Character:
    print("Choose your character:")
    for idx, c in enumerate(CHARACTERS, start=1):
        print(f"  {idx:2}. {c.name} | Passive: {c.passive} | Active: {c.active}")
    while True:
        raw = input("Enter number (1-12): ").strip()
        if raw.isdigit() and 1 <= int(raw) <= len(CHARACTERS):
            return CHARACTERS[int(raw) - 1]
        print("Invalid choice. Try again.")


def weighted_rarity_roll() -> Tuple[str, str, float]:
    p = random.random()
    cumulative = 0.0
    for rarity, rate, color, mult in RARITY_TABLE:
        cumulative += rate
        if p <= cumulative:
            return rarity, color, mult
    rarity, _, color, mult = RARITY_TABLE[-1]
    return rarity, color, mult


def roll_unit_items(map_name: str, item_count: int = 4) -> List[ItemRoll]:
    results: List[ItemRoll] = []
    names = list(ITEM_BASE_VALUES.keys())
    map_bonus = 1.1 if map_name in {"Luxury Vault Annex", "Industrial Depot"} else 1.0

    for _ in range(item_count):
        item_name = random.choice(names)
        base = ITEM_BASE_VALUES[item_name]
        rarity, color, mult = weighted_rarity_roll()
        condition = random.uniform(0.65, 1.30)
        value = int(base * mult * condition * map_bonus)
        results.append(ItemRoll(item_name, rarity, color, value))
    return results


def ai_bid(current_bid: int, bankroll: int, aggression: float) -> int:
    max_willing = int(bankroll * aggression)
    if max_willing <= current_bid:
        return current_bid
    jump = random.randint(5_000, 30_000)
    return min(current_bid + jump, max_willing)


def run_auction(player_name: str) -> None:
    map_name = random.choice(MAP_TYPES)
    print(f"\n=== New Auction: {map_name} ===")

    player_bank = 250_000
    ai_names = ["Ava", "Jax", "Milo"]
    ai_banks = {n: random.randint(180_000, 280_000) for n in ai_names}
    bids: Dict[str, int] = {player_name: 0, **{n: 0 for n in ai_names}}

    winner = None

    for rnd in range(1, 6):
        print(f"\n-- Round {rnd} --")
        print(f"Clue: {ROUND_CLUES[rnd]}")
        print(f"Win condition: top bid must be >= {ROUND_MULTIPLIERS[rnd]:.2f}x 2nd place")

        top_bid = max(bids.values())
        print(f"Current top bid: ${top_bid:,}")

        raw = input("Your next bid (blank to hold): ").strip().replace(",", "")
        if raw:
            try:
                proposed = int(raw)
                if top_bid <= proposed <= player_bank:
                    bids[player_name] = proposed
                else:
                    print("Bid ignored (must be >= top and <= bankroll).")
            except ValueError:
                print("Invalid number; you hold this round.")

        for n in ai_names:
            bids[n] = ai_bid(max(bids.values()), ai_banks[n], aggression=random.uniform(0.5, 0.95))

        ordered = sorted(bids.items(), key=lambda kv: kv[1], reverse=True)
        first_name, first_bid = ordered[0]
        second_bid = ordered[1][1]

        print("Round standings:")
        for name, amount in ordered:
            print(f"  {name:>12}: ${amount:,}")

        needed = int(second_bid * ROUND_MULTIPLIERS[rnd])
        if first_bid >= needed and first_bid > 0:
            winner = first_name
            print(f"\n{first_name} wins in round {rnd}! (${first_bid:,} vs ${second_bid:,})")
            break
        else:
            print(f"No winner yet. Need >= ${needed:,} to end this round.")

    if winner is None:
        ordered = sorted(bids.items(), key=lambda kv: kv[1], reverse=True)
        winner = ordered[0][0]
        print(f"\nNo multiplier closeout by round 5. Highest bid wins: {winner}.")

    if winner != player_name:
        print("You lost this auction. Better luck next unit.")
        return

    paid = bids[player_name]
    loot = roll_unit_items(map_name)
    total_value = sum(i.value for i in loot)
    profit = total_value - paid

    print("\nYou won the unit! Loot reveal:")
    for item in loot:
        print(f"- {item.name} | {item.rarity} ({item.color}) | ${item.value:,}")

    print(f"\nPaid: ${paid:,}")
    print(f"Total item value: ${total_value:,}")
    print(f"Net: ${profit:,}")


def print_rarity_table() -> None:
    print("\nRarity tiers:")
    for rarity, rate, color, mult in RARITY_TABLE:
        print(f"- {rarity:<10} rate={rate*100:>5.2f}% color={color:<24} value={mult:.1f}x")


def main() -> None:
    print("Storage Wars Prototype")
    player = choose_character()
    print(f"\nYou selected: {player.name}")
    print(f"Passive: {player.passive} | Active: {player.active}")
    print_rarity_table()
    run_auction("PLAYER")


if __name__ == "__main__":
    random.seed()
    main()
