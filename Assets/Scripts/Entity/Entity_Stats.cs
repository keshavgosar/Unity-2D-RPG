using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    public Stat_SetupSO defaultStatSetup;

    public Stat_ResourceGroup resources;
    public Stat_MajorGroup major;
    public Stat_OffenseGroup offense;
    public Stat_DefenceGroup defence;

    public float GetElementalDamage(out ElementType element, float scaleFactor = 1f)
    {
        float fireDamage = offense.fireDamage.GetValue();
        float iceDamage = offense.iceDamage.GetValue();
        float lightningDamage = offense.lightningDamage.GetValue();
        float bonusElementalDamage = major.intelligence.GetValue(); // Bonus elemental damage from Intelligence +1 per INT

        float highestDamage = fireDamage;
        element = ElementType.Fire;

        if (highestDamage < iceDamage)
        {
            highestDamage = iceDamage;
            element = ElementType.Ice;
        }

        if (highestDamage < lightningDamage)
        {
            highestDamage = lightningDamage;
            element = ElementType.Lightning;
        }

        if (highestDamage <= 0)
        {
            element = ElementType.None;
            return 0;
        }

        float bonusFire = (element == ElementType.Fire) ? 0 : fireDamage * 0.5f;
        float bonusIce = (element == ElementType.Ice) ? 0 : iceDamage * 0.5f;
        float bonusLightning = (element == ElementType.Lightning) ? 0 : lightningDamage * 0.5f;

        float weakerElementsDamage = bonusFire + bonusIce + bonusLightning;
        float finalDamage = highestDamage + weakerElementsDamage + bonusElementalDamage;

        return finalDamage * scaleFactor;
    }

    public float GetElementalResistance(ElementType element)
    {
        float baseResistance = 0;
        float bonusResistance = major.intelligence.GetValue() * 0.5f; // Bonus resistance from intelligence +0.5% per INT;

        switch (element)
        {
            case ElementType.Fire:
                baseResistance = defence.fireRes.GetValue();
                break;
            case ElementType.Ice:
                baseResistance = defence.iceRes.GetValue();
                break;
            case ElementType.Lightning:
                baseResistance = defence.lightningRes.GetValue();
                break;
        }

        float resistance = baseResistance + bonusResistance;
        float resistanceCap = 75f; //Resistance will capped to 75%
        float finalResistance = Mathf.Clamp(resistance, 0, resistanceCap) / 100; // convert the value to 0 to 1 multiplier;

        return finalResistance;
    }

    public float GetPhysicalDamage(out bool isCrit, float scaleFactor = 1f)
    {
        float baseDamage = offense.damage.GetValue();
        float bonusDamage = major.strength.GetValue(); // bonus damage from strength +1 per STR
        float totalBaseDamage = baseDamage + bonusDamage;

        float baseCritChance = offense.critChance.GetValue();
        float bonusCritChance = major.agility.GetValue() * 0.3f; // Bonus crit chance from Agility: +0.3% per AGI
        float critChance = baseCritChance + bonusCritChance;

        float baseCritPower = offense.critPower.GetValue();
        float bonusCritPower = major.strength.GetValue() * 0.5f; // Bonus crit chance from strngth +0.5% per STR
        float critPower = (baseCritPower + bonusCritPower) / 100; // Toral crit power multiplier (eg. 150/100 = 1.5f)

        isCrit = Random.Range(0, 100) < critChance;
        float finalDamage = isCrit ? totalBaseDamage * critPower : totalBaseDamage;

        return finalDamage * scaleFactor;
    }

    public float GetArmorMitigation(float armorReduction)
    {
        float baseArmor = defence.armor.GetValue();
        float bonusArmor = major.vitality.GetValue(); // Bonus armor from vitality: +1 per VIT
        float totalArmor = baseArmor + bonusArmor;

        float reductionMultiplier = Mathf.Clamp(1 - armorReduction, 0, 1);
        float effectiveArmor = totalArmor * reductionMultiplier;

        float mitigation = effectiveArmor / (effectiveArmor + 100); // Mitigation formula;
        float mitigationCap = 0.85f; // Max mitigation will be capped to 85%;

        float finalMitigation = Mathf.Clamp(mitigation, 0, mitigationCap);

        return finalMitigation;
    }

    public float GetArmorReduction()
    {
        // Total armor reduction as multiplier (e.g 30 / 100 = 0.3f)
        float finalReduction = offense.armorReduction.GetValue() / 100;
        return finalReduction;
    }

    public float GetEvasion()
    {
        float baseEvasion = defence.evasion.GetValue();
        float bonusEvasion = major.agility.GetValue() * 0.5f; // each agility point gives 0.5% of evasion

        float totalEvasion = baseEvasion + bonusEvasion;
        float evasionCap = 85f; // Evasion will be capped at 85%

        float finalEvasion = Mathf.Clamp(totalEvasion, 0, evasionCap);

        return finalEvasion;
    }
    public float GetMaxHealth()
    {
        float baseMaxHealth = resources.maxHealth.GetValue();
        float bonusMaxHealth = major.vitality.GetValue() * 5;

        float finalMaxHealth = baseMaxHealth + bonusMaxHealth;
        return finalMaxHealth;
    }
   
    public Stat GetStatByType(StatType type)
    {
        switch (type)
        {
            case StatType.MaxHealth: return resources.maxHealth;
            case StatType.HealthRegen: return resources.healthRegen;

            case StatType.Strength: return major.strength;
            case StatType.Agility: return major.agility;
            case StatType.Intelligence: return major.intelligence;
            case StatType.Vitality: return major.vitality;

            case StatType.AttackSpeed: return offense.attackSpeed;
            case StatType.Damage: return offense.damage;
            case StatType.CritChance: return offense.critChance;
            case StatType.CritPower: return offense.critPower;
            case StatType.ArmorReduction: return offense.armorReduction;

            case StatType.FireDamage: return offense.fireDamage;
            case StatType.IceDamage: return offense.iceDamage;
            case StatType.LightningDamage: return offense.lightningDamage;

            case StatType.Armor: return defence.armor;
            case StatType.Evasion: return defence.evasion;

            case StatType.IceResistance: return defence.iceRes;
            case StatType.LightningResistance: return defence.lightningRes;
            case StatType.FireResistance: return defence.fireRes;

            default:
                Debug.Log($"StatType {type} not implemented yet.");
                return null;
        }
    }

    
    [ContextMenu("Update Default Stat Setup")]
    public void ApplyDefaultStatSetup()
    {
        if (defaultStatSetup == null)
        {
            Debug.Log("No Default Setup assign!!!");
            return;
        }

        // Resources
        resources.maxHealth.SetBaseValue(defaultStatSetup.maxHealth);
        resources.healthRegen.SetBaseValue(defaultStatSetup.healthRegen);
        
        // Physical Damage
        offense.critPower.SetBaseValue(defaultStatSetup.critPower);
        offense.critChance.SetBaseValue(defaultStatSetup.critChance);
        offense.armorReduction.SetBaseValue(defaultStatSetup.armorReduction);
        offense.attackSpeed.SetBaseValue(defaultStatSetup.attackSpeed);
        offense.damage.SetBaseValue(defaultStatSetup.damage);

        // Elemental Damage
        offense.iceDamage.SetBaseValue(defaultStatSetup.iceDamage);
        offense.lightningDamage.SetBaseValue(defaultStatSetup.lightningDamage);
        offense.fireDamage.SetBaseValue(defaultStatSetup.fireDamage);

        // Defence
        defence.armor.SetBaseValue(defaultStatSetup.armor);
        defence.evasion.SetBaseValue(defaultStatSetup.evasion);

        // Elemental Resistance
        defence.iceRes.SetBaseValue(defaultStatSetup.iceResistance);
        defence.fireRes.SetBaseValue(defaultStatSetup.fireResistance);
        defence.lightningRes.SetBaseValue(defaultStatSetup.lightningResistance);

        // Major Stats
        major.strength.SetBaseValue(defaultStatSetup.strength);
        major.agility.SetBaseValue(defaultStatSetup.agility);
        major.vitality.SetBaseValue(defaultStatSetup.vitality);
        major.intelligence.SetBaseValue(defaultStatSetup.intelligence);
    }

}
