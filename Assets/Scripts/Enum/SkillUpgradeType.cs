using UnityEngine;

public enum SkillUpgradeType
{
    None,

    // ------- Dash Tree -------
    Dash,                        // Dash to avoid damage
    Dash_CloneOnStart,           // Create a clone when dash starts
    Dash_CloneOnStartAndArrival, // Create a clone when dash start and ends
    Dash_ShardOnStart,           // Create a shard when dash starts
    Dash_ShardOnStartAndArrival, // Create ashard when dash start and ends

    // ----- Shard Tree --------
    Shard,                // The shard explodes when touched by an enemy or time goes up
    Shard_MoveToEnemy,    // Shard will move towards nearest enemy
    Shard_Multicast,     // Shard ability can have up to N carges. You can cast them all in raw
    Shard_Teleport,       // You can swap places with the last shard you created
    Shard_TeleportHpRewind, // When you swap places with shard, your HP % is same as it waws when you created shard.

    // ----- Sword Throw --------
    SwordThrow,        // You can throw sword to damage enemies from range
    SwordThrow_Spin,   // Your sword will spin at one point and damage enemies, Like a chainsaw 
    SwordThrow_Pierce, // Pierce sword will pierce N targets
    SwordThrow_Bounce,  // Bounce sword will bounce between enemies

    // ----- Time Echo ------
    TimeEcho,                 // Create a clone of a player. It can take damage from enemies.
    TimeEcho_SingleAttack,    // Time Echo can perform a single attack
    TimeEcho_MultiAttack,     // Time Echo can perform N attacks.
    TimeEcho_ChanceToDuplicate, // Time Echo has a chance to create another time echo when attacks

    TimeEcho_HealWisp,        // When Time echo dies it creates a wips that flies towards the player to heal it.
                              // Heal is - to percantage of damage taken when died
    TimeEcho_CleanseWisp,     // Wisp will now remove negative effects from player
    TimeEcho_CooldownWisp,    // Wisp will reduce cooldown of all skills by N seconds.

    // ------- Domain Expansion ------
    Domain_SlowingDown, // Create an area in wich you slow down enemies by 90-100%. You can freely move and fight.
    Domain_EchoSpam,    // You can no longer move, but you spam enemy with Time Echo ability
    Domain_ShardSpam    // You can no longer move, but you spam enemy with Time Shard ability
}
