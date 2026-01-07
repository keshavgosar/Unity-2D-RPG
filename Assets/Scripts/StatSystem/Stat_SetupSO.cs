using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Default Stat Setup", fileName = "Default Stat Setup")]
public class Stat_SetupSO : ScriptableObject
{
    [Header("Resources")]
    public float maxHealth = 100f;
    public float healthRegen;

    [Header("Offence - Physical Damage")]
    public float attackSpeed = 1f;
    public float damage = 10f;
    public float critChance;
    public float critPower;
    public float armorReduction;

    [Header("Offence - Elemental Damage")]
    public float fireDamage;
    public float iceDamage;
    public float lightningDamage;

    [Header("Defence - Physical Damage")]
    public float armor;
    public float evasion;

    [Header("Defence - Elemental Damage")]
    public float fireResistance;
    public float iceResistance;
    public float lightningResistance;

    [Header("Major Stats")]
    public float strength;
    public float vitality;
    public float agility;
    public float intelligence;
}
