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
    SwordThrow_Bounce  // Bounce sword will bounce between enemies
}
