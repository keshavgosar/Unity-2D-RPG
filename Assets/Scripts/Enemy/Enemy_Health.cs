using UnityEngine;

public class Enemy_Health : Entity_Health
{
    private Enemy enemy;
    private Player_QuestManager questManager;

    protected override void Start()
    {
        base.Start();

        enemy = GetComponent<Enemy>();
        questManager = Player.instance.questManager;
    }

    public override bool TakeDamage(float damage, float elementalDamage,ElementType element, Transform damageDealer)
    {
        if (canTakeDamage == false)
            return false;

        bool wasHit = base.TakeDamage(damage, elementalDamage, element, damageDealer);

        if (!wasHit) 
            return false;

        if (damageDealer.CompareTag("Player"))
        {
            enemy.TryEnterBatlleState(damageDealer);
        }

        return true;
    }

    protected override void Die()
    {
        base.Die();

        questManager.AddProgress(enemy.questTargetId);
    }
}
