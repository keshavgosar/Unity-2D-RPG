using System;
using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    public event Action<float> OnDoingPhysicalDamage;

    private Entity_SFX sfx;
    private Entity_VFX vfx;
    private Entity_Stats stats;

    public DamageScaleData basicAttackScale;

    [Header("Target Detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1;
    [SerializeField] private LayerMask whatIsTarget;

    private void Awake()
    {
        vfx = GetComponent<Entity_VFX>();
        stats = GetComponent<Entity_Stats>();
        sfx = GetComponent<Entity_SFX>();
    }
    public void PerformAttack()
    {
        bool targetGotHit = false;

        foreach (var target in GetDetectedColliders(whatIsTarget))
        {
            IDamagable damagable = target.GetComponent<IDamagable>();

            if (damagable == null)
                continue;

            AttackData attackData = stats.GetAttackData(basicAttackScale);
            Entity_StatusHandler statusHandler = target.GetComponent<Entity_StatusHandler>();

            float physicalDamage = attackData.physicalDamage;
            float elementalDamage = attackData.elementalDamage;
            ElementType element = attackData.element;

            targetGotHit = damagable.TakeDamage(physicalDamage, elementalDamage, element, transform);

            if (element != ElementType.None)
                statusHandler?.ApplyStatusEffect(element, attackData.effectData);

            if (targetGotHit)
            {
                OnDoingPhysicalDamage?.Invoke(physicalDamage);
                vfx.CreateOnHitVFX(target.transform, attackData.isCrit, element);
                sfx?.PlayAttackHit();
            }
        }
        if (targetGotHit == false)
            sfx?.PlayAttackMiss();
    }

    public void PerformAttckOnTarget(Transform target, DamageScaleData damageScaleData = null)
    {
        bool targetGotHit = false;


        IDamagable damagable = target.GetComponent<IDamagable>();

        if (damagable == null)
            return;

        DamageScaleData damageScale = damageScaleData == null ? basicAttackScale : damageScaleData;
        AttackData attackData = stats.GetAttackData(damageScale);
        Entity_StatusHandler statusHandler = target.GetComponent<Entity_StatusHandler>();

        float physicalDamage = attackData.physicalDamage;
        float elementalDamage = attackData.elementalDamage;
        ElementType element = attackData.element;

        targetGotHit = damagable.TakeDamage(physicalDamage, elementalDamage, element, transform);

        if (element != ElementType.None)
            statusHandler?.ApplyStatusEffect(element, attackData.effectData);

        if (targetGotHit)
        {
            OnDoingPhysicalDamage?.Invoke(physicalDamage);
            vfx.CreateOnHitVFX(target.transform, attackData.isCrit, element);
            sfx?.PlayAttackHit();
        }

        if (targetGotHit == false)
            sfx?.PlayAttackMiss();
    }


    protected Collider2D[] GetDetectedColliders(LayerMask whatToDetect)
    {
        return Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, whatToDetect);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }
}
