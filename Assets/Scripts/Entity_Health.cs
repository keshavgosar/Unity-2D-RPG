using UnityEngine;

public class Entity_Health : MonoBehaviour, IDamagable
{
    private Entity_VFX entityVfx;
    private Entity entity;

    [SerializeField] protected float currentHP;
    [SerializeField] protected float maxHP = 100;
    [SerializeField] protected bool isDead;

    [Header("On Damage Knockback")]
    [SerializeField] private Vector2 knockbackPower = new Vector2(1.5f, 2.5f);
    [SerializeField] private Vector2 heavyKnockbackPower = new Vector2(7f, 7f);
    [SerializeField] private float knockbackDuration = 0.2f;
    [SerializeField] private float heavyKnockbackDuration = 0.5f;

    [Header("On Heavy Damage")]
    [SerializeField] private float heavyDamageThreshold = 0.3f; // Percentage of health have to loose to consider the damage as heavy;

    protected virtual void Awake()
    {
        entityVfx = GetComponent<Entity_VFX>();
        entity = GetComponent<Entity>();

        currentHP = maxHP;
    }

    public virtual void TakeDamage(float damage, Transform damageDealer)
    {
        if (isDead)
            return;

        Vector2 knockback = CalculateKnockback(damage, damageDealer);
        float duration = CalculateDuration(damage);

        if(entity)
        {
            entity.ReciveKnockback(knockback, duration);
        }
        
        if(entityVfx)
        {
            entityVfx.PlayOnDamageVfx();
        }

        ReduceHp(damage);
    }

    protected void ReduceHp(float damage)
    {
        currentHP -= damage;

        if (currentHP < 0) 
            Die();
    }

    private void Die()
    {
        isDead = true;
        entity.EntityDeath();
    }

    private Vector2 CalculateKnockback(float damage, Transform damageDealer)
    {
        int direction = transform.position.x > damageDealer.position.x ? 1 : -1;

        Vector2 knockback = IsHeavyDamage(damage) ? heavyKnockbackPower : knockbackPower;

        knockback.x *= direction;

        return knockback;
    }

    private float CalculateDuration(float damage) => IsHeavyDamage(damage) ? heavyKnockbackDuration : knockbackDuration;

    private bool IsHeavyDamage(float damage) => damage / maxHP > heavyDamageThreshold;
}
