using System.Xml;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Enemy_ArcherElf : Enemy, ICounterable
{
    public bool CanBeCountered { get => canBeStunned; }
    public Enemy_ArcherElfBattleState elfBattleState { get;  set; }

    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_IdleState(this, stateMachine, "Idle");
        moveState = new Enemy_MoveState(this, stateMachine, "Move");
        attackState = new Enemy_AttackState(this, stateMachine, "Attack");
        deadState = new Enemy_DeadState(this, stateMachine, "Idle"); // No Parameter Used as not applying animation!
        stunnedState = new Enemy_StunnedState(this, stateMachine, "Stunned");
        
        elfBattleState = new Enemy_ArcherElfBattleState(this, stateMachine, "Battle");
        battleState = elfBattleState;
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    public void HandleCounter()
    {
        if (CanBeCountered == false)
        {
            return;
        }

        stateMachine.ChangeState(stunnedState);
    }
}
