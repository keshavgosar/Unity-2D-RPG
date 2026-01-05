using UnityEngine;

public class Enemy_Health : Entity_Health
{
    private Enemy enemy => GetComponent<Enemy>();

    public override bool TakeDamage(float damage, float elementalDamage, Transform damageDealer)
    {
        bool wasHit = base.TakeDamage(damage, elementalDamage, damageDealer);

        if (!wasHit) 
            return false;

        if (damageDealer.CompareTag("Player"))
        {
            enemy.TryEnterBatlleState(damageDealer);
        }

        return true;
    }
}
