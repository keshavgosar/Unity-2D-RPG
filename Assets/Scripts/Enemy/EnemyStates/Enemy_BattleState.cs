using UnityEngine;

public class Enemy_BattleState : EnemyState
{

    protected Transform player;
    protected Transform lastTarget;
    protected float lastTImeWasInBattle;
    protected float lastTimeAttacked;
    public Enemy_BattleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        UpdateBattleTimer();

        if (player == null)
        {
            player = enemy.GetPlayerReference();
        }

        if (ShouldRetreat())
        {
            rb.linearVelocity = new Vector2((enemy.retreatVelocity.x * enemy.activeSlowMultiplier) * -DirectionToPlayer(), enemy.retreatVelocity.y);
            enemy.HandleFlip(DirectionToPlayer());
        }
    }

    public override void Update()
    {
        base.Update();

        if (enemy.IsPlayerDetected() == true)
        {
            UpdateTargetIfNeeded();
            UpdateBattleTimer();
        }

        if (IsBattleTimeIsOver())
        {
            stateMachine.ChangeState(enemy.idleState);
        }

        if (WithinAttackRange() && enemy.IsPlayerDetected() && CanAttack())
        {
            stateMachine.ChangeState(enemy.attackState);
        }
        else
        {
            float xVelocity = enemy.canChasePlayer ? enemy.GetBattleMoveSpeed()  : 0.0001f;
            enemy.SetVelocity(xVelocity * DirectionToPlayer(), rb.linearVelocity.y);
        }
    }

    protected bool CanAttack() => Time.time > lastTimeAttacked + enemy.attackCooldown;

    protected void UpdateTargetIfNeeded()
    {
        if (enemy.IsPlayerDetected() == false)
            return;

        Transform newTarget = enemy.IsPlayerDetected().transform;

        if(newTarget != lastTarget)
        {
            lastTarget = newTarget;
            player = newTarget;
        }
    }

    protected void UpdateBattleTimer() => lastTImeWasInBattle = Time.time;

    protected bool IsBattleTimeIsOver() => Time.time > lastTImeWasInBattle + enemy.battleTimeDuration;

    protected bool WithinAttackRange() => DistanceToPlayer() < enemy.attackDistance;

    protected bool ShouldRetreat() => DistanceToPlayer() < enemy.minRetreatDistance;

    protected float DistanceToPlayer()
    {
        if (player == null)
            return float.MaxValue;

        return Mathf.Abs(player.position.x - enemy.transform.position.x);
    }

    protected int DirectionToPlayer()
    {
        if (player == null) return 0;

        return player.position.x > enemy.transform.position.x ? 1 : -1;
    }
}
