using UnityEngine;
using UnityEngine.Rendering;

public class SkillObject_SwordPeirce : SkillObject_Sword
{
    private int amountToPierce;

    public override void SetupSword(Skill_SwordThrow swordManger, Vector2 direction)
    {
        base.SetupSword(swordManger, direction);
        amountToPierce = swordManager.pierceAmount;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        bool groundHit = collision.gameObject.layer == LayerMask.NameToLayer("Ground");
        if (amountToPierce <= 0 || groundHit)
        {
            DamageEnemiesInRadius(transform, .3f);
            StopSword(collision);
            return;
        }

        amountToPierce--;
        DamageEnemiesInRadius(transform, .3f);
    }
}
