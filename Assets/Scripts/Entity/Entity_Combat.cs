using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    private Entity_VFX vfx;
    private Entity_Stats stats;
 
    
    [Header("Target Detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1;
    [SerializeField] private LayerMask whatIsTarget;

    [Header("Status Effect Details")]
    [SerializeField] private float defaultDuration = 3;
    [SerializeField] private float chillSlowMultiplier = 0.2f;
    [SerializeField] private float electrifyChargeBuildUp = 0.4f;
    [Space]
    [SerializeField] private float fireScale = 0.8f;
    [SerializeField] private float lightningScale = 2.5f;

    private void Awake()
    {
        vfx = GetComponent<Entity_VFX>();
        stats = GetComponent<Entity_Stats>();
    }
    public void PerformAttack()
    {
        foreach (var target in GetDetectedColliders())
        {
            IDamagable damagable = target.GetComponent<IDamagable>();

            if (damagable == null) 
                continue;

            float elementalDamage = stats.GetElementalDamage(out ElementType element);
            float damage = stats.GetPhysicalDamage(out bool isCrit);

            bool targetGotHit = damagable.TakeDamage(damage, elementalDamage, element, transform);

            if (element != ElementType.None)
                ApplyStatusEffect(target.transform, element);

            if (targetGotHit)
            {
                vfx.UpdateOnHitColor(element);
                vfx.CreateOnHitVFX(target.transform, isCrit);
            }
            
        }
    }

    public void ApplyStatusEffect(Transform target, ElementType element, float scaleFactor = 1f)
    {
        Entity_StatusHandler statusHandler = target.GetComponent<Entity_StatusHandler>();

        if (statusHandler == null)
            return;


        if (element == ElementType.Ice && statusHandler.CanBeApplied(ElementType.Ice))
            statusHandler.ApplyChillEffect(defaultDuration, chillSlowMultiplier);

        if(element == ElementType.Fire && statusHandler.CanBeApplied(ElementType.Fire))
        {
            scaleFactor = fireScale;
            float fireDamage = stats.offense.fireDamage.GetValue() * scaleFactor;
            statusHandler.ApplyBurnEffect(defaultDuration, fireDamage);
        }

        if (element == ElementType.Lightning && statusHandler.CanBeApplied(ElementType.Lightning))
        {
            scaleFactor = lightningScale;
            float lightninghDamage = stats.offense.lightningDamage.GetValue() * scaleFactor;
            statusHandler.ApplyElectrifyEffect(defaultDuration, lightninghDamage, electrifyChargeBuildUp);
        }
    }

    protected Collider2D[] GetDetectedColliders()
    {
        return Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, whatIsTarget);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }
}
