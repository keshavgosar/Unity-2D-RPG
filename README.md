# 2D Action RPG – Nightfall Chronicles

A **2D pixel-art Action RPG** built using Unity.
This project focuses on modular system design, player progression, and RPG mechanics commonly used in commercial games.

---

## Game Overview

* **Genre:** 2D Action RPG
* **Perspective:** Side-scroller
* **Focus:** Combat, progression systems, RPG mechanics
* **Engine:** Unity

The game is designed with **extensibility in mind**, allowing new skills, enemies, items, and levels to be added with minimal changes to existing systems.

![GameplayOverviewGIF](https://github.com/user-attachments/assets/70870655-4ce3-4b70-bdd3-dc9c93a844a1)


---

## Core Gameplay Loop

1. Explore levels and fight enemies
2. Collect loot, buffs, and resources
3. Complete quests from NPCs
4. Upgrade skills and stats
5. Unlock abilities and progress through new areas

---

## Player Mechanics

### 1. Movement & Combat System

The player controller follows a **state-driven approach**:

* Idle
* Move
* Jump
* Basic Attack
* Wall Sliding
* Dash (unlocked by default, upgradeable later)

Combat and movement are tightly integrated to ensure responsive gameplay. Dash upgrades enhance distance, cooldown, or additional effects based on player choices.

---

## Stats System

The player stats are organized into structured categories for clarity and scalability:

### Major Stats

* Health
* Attack Power
* Movement Speed

### Defense Stats

* Armor
* Damage Reduction

### Elemental Stats

* Elemental Damage
* Elemental Resistance

### Resource Stats

* Max Health
* Health Regeneration

This layered approach allows buffs, equipment, and skills to affect specific stat groups without breaking balance.

---

## Skill System

The game features a **choice-based skill system**, where the player can unlock **only one primary spell path per run**.

### Available Skills

* Dash Upgrades
* Shard Spell
* Time Echo Spell
* Sword Throw Skill
* **Advanced Skill:** Domain Expansion

Each skill:

* Can be upgraded
* Has clear progression levels
* Interacts with player stats and combat systems

This design encourages **replayability and build diversity**.

<img width="1920" height="1080" alt="Screenshot (85)" src="https://github.com/user-attachments/assets/d9ad5a47-6140-44a9-a2b0-ff110d7f6430" />


---

## Inventory System

* Equipment items
* Consumables
* Buff items
* Crafting materials

The inventory system supports:

* Item pickup
* Item usage
* Equipment handling
* Integration with merchants and crafting


<img width="1920" height="1080" alt="Screenshot (84)" src="https://github.com/user-attachments/assets/b49732f9-3aba-4864-8272-267865f4a647" />

---

## NPC System

The game includes two core NPCs:

### Merchant

* Gives quests using the quest system
* Buys and sells items
* Provides teleport scrolls and consumables

### Blacksmith

* Item storage system
* Crafting system
* Allows crafting equipment (e.g., swords) using materials

NPC logic is modular, making it easy to add more NPCs with different roles.


<img width="1920" height="1080" alt="Screenshot (81)" src="https://github.com/user-attachments/assets/d1e8cb57-50eb-4757-9527-06d1439d412e" />

---

## Quest & Dialogue System

* Interactive dialogue-driven quests
* Merchant-driven quest progression
* Modular quest structure

The dialogue system supports:

* Player interaction
* Quest assignment
* Narrative delivery

This system is designed so additional quests and branching dialogue can be added easily.


<img width="1920" height="1080" alt="Screenshot (79)" src="https://github.com/user-attachments/assets/d264057a-b6e9-405c-9dc2-10f84884e3eb" />

---

## Chests & Buff System

* Chests drop **randomized loot**
* Buff items temporarily enhance:

  * Stats
  * Attack power
  * Combat performance

Buffs are time-based and stack safely using controlled modifiers.

---

## Enemy System

The game includes **five enemy types**, each with unique behavior patterns:

* Skeleton (melee)
* Archer (ranged)
* Slime (divide itself)
* Mage (spell-based)
* **Boss:** Reaper

Enemy AI is designed with reusable logic for future enemy expansion.

---

## Auto Save & Checkpoint System

* Game progress is **automatically saved** on exit
* On death, the player respawns at the **nearest checkpoint**

This ensures a smooth player experience without manual save management.

---

## Level Travel System

* Players move between levels using:

  * Travel arrows
  * Sign boards

This allows controlled progression while keeping level transitions intuitive.

---

## Teleport System

* Teleport scrolls can be:

  * Found as drops
  * Purchased from the merchant

Using a scroll opens a **portal** that allows instant travel to:

* Camp
* Related safe areas

This system integrates with inventory, NPCs, and level management.

---

## System Design Philosophy

* Modular architecture
* Data-driven design
* Easy extensibility
* Clean separation of systems

Each system is implemented independently but communicates through well-defined interfaces.

---

## Future Improvements

* Additional skill paths
* More enemy variations
* Expanded boss mechanics
* Deeper crafting system
* Enhanced UI/UX

---

## Download

You can download and play the packaged version from Itch.io:
https://keshav567.itch.io/nightfall-chronicalls

---

Thank you for taking the time to review this project.
