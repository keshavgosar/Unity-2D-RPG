using UnityEngine;

public class Enemy_Slime : Enemy, ICounterable
{
    public bool CanBeCountered { get => canBeStunned; }
    public Enemy_SlimeDeadState slimeDeadState { get; private set; }

    [Header("Slime Specifics")]
    [SerializeField] private GameObject slimeToCreatePrefab;
    [SerializeField] private int amountOfSlimesToCreate = 2;
    [SerializeField] private Vector2 newSlimeVelocity;

    [SerializeField] private bool hasRecoveryAnimation = true;

    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_IdleState(this, stateMachine, "Idle");
        moveState = new Enemy_MoveState(this, stateMachine, "Move");
        attackState = new Enemy_AttackState(this, stateMachine, "Attack");
        battleState = new Enemy_BattleState(this, stateMachine, "Battle");
        deadState = new Enemy_DeadState(this, stateMachine, "Idle"); // No Parameter Used as not applying animation!
        stunnedState = new Enemy_StunnedState(this, stateMachine, "Stunned");
        slimeDeadState = new Enemy_SlimeDeadState(this, stateMachine, "Idle");

        anim.SetBool("HasStunRecovery", hasRecoveryAnimation);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    public override void EntityDeath()
    {
        stateMachine.ChangeState(slimeDeadState);
    }

    public void HandleCounter()
    {
        if (CanBeCountered == false)
        {
            return;
        }

        stateMachine.ChangeState(stunnedState);
    }

    public void CreateSlimeOnDeath()
    {
        if (slimeToCreatePrefab == null)
            return;

        for (int i = 0; i < amountOfSlimesToCreate; i++)
        {
            GameObject newSlime = Instantiate(slimeToCreatePrefab, transform.position, Quaternion.identity);
            Enemy_Slime slimeScript = newSlime.GetComponent<Enemy_Slime>();

            slimeScript.stats.SetupStatsWithPenalty(stats.resources, stats.offense, stats.defence, .6f, 1.2f);
            slimeScript.ApplyRespawnVelocity();
            slimeScript.StartBattleStateCheck(player);
            
        }

    }

    public void ApplyRespawnVelocity()
    {
        Vector2 velocity = new Vector2(stunnedVelocity.x * Random.Range(-1f, 1f), stunnedVelocity.y * Random.Range(1f, 2f));
        SetVelocity(velocity.x, velocity.y);
    }

    public void StartBattleStateCheck(Transform player)
    {
        TryEnterBatlleState(player);
        InvokeRepeating(nameof(ReEnterBattleState), 0, .3f);
    }

    private void ReEnterBattleState()
    {
        if(stateMachine.currentState == battleState || stateMachine.currentState == attackState)
        {
            CancelInvoke(nameof(ReEnterBattleState));
            return;
        }

        stateMachine.ChangeState(battleState);
    }
}
