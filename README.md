# Diablo 2 Unity clone

A group project for an advanced graphics class with the objective of using a modern game engine to
create an action role playing game. Learned and applied many concepts including:
- Player movement
- Camera follow
- Combat system and projectiles
- Leveling system
- Game GUI
- Animations and sound

## Scene walkthrough

![](./misc/Screenshot5.png)

Player starts on a path near the edge of a forest

![](./misc/Screenshot3.png)

Follows the path to a burning town. Enemy mobs scattered throughout.

![](./misc/Screenshot2.png)

Special attack on an enemy

![](./misc/Screenshot1.png)

Magic attack is a projectile that pierces.

![](./misc/Screenshot4.png)

This is where the entrance to the boss fight would be, if we had one.

## Summary
The scene is a mountainous/forested terrain with a path winding
through it, from one end to the other. Along the path are abandoned houses and hordes
of enemies. The lighting is dark and gloomy to mimic a setting sun with smoke in the air.

The game is this one scene with a player character and two types of
enemies. The player character has an idle and run animations and a basic attack, a
special attack (more damage and stun), and a magic attack (more damage and long
range). The enemies also have idle and walk/run animations and a basic attack.

When in combat, a health bar for the current enemy is displayed at the top of the screen. An
action bar is always displayed at the bottom of the screen and contains the following
info: health, mana, experience, and keys for the special and magic attack. Mana is used
when casting a magic attack. 

When health reaches 0, the player “dies” but the game
does not end because the player re­spawns with full health after 10 seconds. There are
no health or mana potions so they both constantly replenish and do so more quickly
when not in combat. The player starts at level 0 and earns experience by killing
enemies. At level up the following stats are automatically increased, either linearly or
exponentially depending on the stat: player maximum health, player damage, enemy
maximum health, enemy damage, experience required for next level, and experience
gained from enemy kill.

There is a background track
that loops with music borrowed from the original Diablo II game. There are various
enemy sounds (attack, pain, roar, death), player sounds (attack, pain, death), attack
sounds (blade sounds, magic casting sounds), and footsteps for the player.


All models and animations were downloaded free from the Unity asset store.