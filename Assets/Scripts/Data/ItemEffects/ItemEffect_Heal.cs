using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Item Data/Item Effect/ Heal Effect", fileName = "Item Effect data - Heal")]
public class ItemEffect_Heal : ItemEffect_DataSO
{
    [SerializeField] private float healPercent = .1f;
    public override void ExecuteEffects()
    {
        Player player = FindFirstObjectByType<Player>();
        
        float healAmount = player.stats.GetMaxHealth() * healPercent;
        player.health.IncreaseHealth(healAmount);
    }
}

