using UnityEngine;

public class Enemy_ArcherElfBattleState : Enemy_BattleState
{
    private bool canFlip;
    private bool reachedDeadEnd;
    public Enemy_ArcherElfBattleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        reachedDeadEnd = false;
    }

    public override void Update()
    {
        stateTimer -= Time.deltaTime;
        UpdateAnimationParameters();

        if (enemy.groundDetected == false || enemy.wallDetected)
            reachedDeadEnd = true;

        if (enemy.IsPlayerDetected())
        {
            UpdateTargetIfNeeded();
            UpdateBattleTimer();
        }

        if (IsBattleTimeIsOver())
        {
            stateMachine.ChangeState(enemy.idleState);
        }

        if (CanAttack())
        {
            if (enemy.IsPlayerDetected() == false && canFlip)
            {
                enemy.HandleFlip(DirectionToPlayer());
                canFlip = false;
            }

            enemy.SetVelocity(0, rb.linearVelocity.y);

            if (WithinAttackRange() && enemy.IsPlayerDetected())
            {
                canFlip = true;
                lastTimeAttacked = Time.time;
                stateMachine.ChangeState(enemy.attackState);
            }
        }
        else
        {
            bool shouldWalkAway = reachedDeadEnd == false && DistanceToPlayer() < (enemy.attackDistance * .85f);

            if (shouldWalkAway)
            {
                enemy.SetVelocity((enemy.GetBattleMoveSpeed() * -1) * DirectionToPlayer(), rb.linearVelocity.y);
            }
            else
            {
                enemy.SetVelocity(0, rb.linearVelocity.y);

                if (enemy.IsPlayerDetected() == false)
                    enemy.HandleFlip(DirectionToPlayer());
            }
        }
    }
}
