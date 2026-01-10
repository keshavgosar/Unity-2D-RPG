using UnityEngine;

public class Skill_ObjectBase : MonoBehaviour
{
    [SerializeField] protected LayerMask whatIsEnemy;
    [SerializeField] protected Transform targetCheck;
    [SerializeField] protected float checkRadius = 1;

    protected void DamageEnemiesInRadius(Transform t, float radius)
    {
        foreach(var target in EnemiesAround(t, radius))
        {
            IDamagable damagable = target.GetComponent<IDamagable>();

            if(damagable == null)
                continue;

            damagable.TakeDamage(1, 1, ElementType.None, transform);
        }
    }

    protected Transform FindClosestTarget()
    {
        Transform target = null;
        float  closestDistance = Mathf.Infinity;

        foreach(var enemy in EnemiesAround(transform, 10))
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if(distance < closestDistance)
            {
                target = enemy.transform;
                closestDistance = distance;
            }
        }

        return target;
    }

    protected Collider2D[] EnemiesAround(Transform t, float radius)
    {
        return Physics2D.OverlapCircleAll(t.position, radius, whatIsEnemy);
    }

    protected virtual void OnDrawGizmos()
    {
        if (targetCheck == null)
            targetCheck = transform;

        Gizmos.DrawSphere(targetCheck.position, checkRadius);
    }
}
