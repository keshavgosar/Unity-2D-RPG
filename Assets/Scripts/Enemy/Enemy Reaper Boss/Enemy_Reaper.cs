using System.Collections;
using UnityEngine;

public class Enemy_Reaper : Enemy, ICounterable
{
    public bool CanBeCountered { get => canBeStunned; }
    public Enemy_ReaperAttackState reaperAttackState { get; private set; }
    public Enemy_ReaperBattleState reaperBattleState { get; private set; }
    public Enemy_ReaperTeleportState reaperTeleportState { get; private set; }
    public Enemy_ReaperSpellCastState reaperSpellCastState { get; private set;}

    [Header("Reaper Specifics")]
    public float maxBattleIdleTime = 5;

    [Header("Reaper SpellCast")]
    [SerializeField] private DamageScaleData spellDamageScale;
    [SerializeField] private GameObject spellCastPrefab;
    [SerializeField] private int amountToCast = 6;
    [SerializeField] private float spellCastRate = 1.2f;
    [SerializeField] private float spellCastStateCooldown = 10;
    [SerializeField] private Vector2 playerOffsetPrediction;
    private float lastTimeCastedSpells = float.NegativeInfinity;
    public bool spellCastPerformed { get; private set; }
    private Player playerScript;

    [Header("Reaper Teleport")]
    [SerializeField] private BoxCollider2D arenaBounds;
    [SerializeField] private float offsetCenterY = 1.65f;
    [SerializeField] private float chanceToTeleport = .25f;
    private float defaultTeleportChance;
    public bool teleportTrigger { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_IdleState(this, stateMachine, "Idle");
        moveState = new Enemy_MoveState(this, stateMachine, "Move");
        deadState = new Enemy_DeadState(this, stateMachine, "Idle"); // No Parameter Used as not applying animation!
        stunnedState = new Enemy_StunnedState(this, stateMachine, "Stunned");

        reaperBattleState = new Enemy_ReaperBattleState(this, stateMachine, "Battle");
        reaperAttackState = new Enemy_ReaperAttackState(this, stateMachine, "Attack");
        reaperTeleportState = new Enemy_ReaperTeleportState(this, stateMachine, "Teleport");
        reaperSpellCastState = new Enemy_ReaperSpellCastState(this, stateMachine, "SpellCast");

        battleState = reaperBattleState;

    }

    protected override void Start()
    {
        base.Start();
        arenaBounds.transform.parent = null;
        defaultTeleportChance = chanceToTeleport;

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

    public void SetSpellCastPerformed(bool spellCastStatus) => spellCastPerformed = spellCastStatus;

    public bool ShouldTeleport()
    {
        if (Random.value < chanceToTeleport)
        {
            chanceToTeleport = defaultTeleportChance;
            return true;
        }

        chanceToTeleport = chanceToTeleport + .05f;
        return false;
    }

    public void SetTeleportTrigger(bool triggerStatus) => teleportTrigger = triggerStatus;

    public override void SpecialAttack()
    {
        StartCoroutine(CastSpellCo());
    }

    private IEnumerator CastSpellCo()
    {

        if (playerScript == null)
            playerScript = player.GetComponent<Player>();

        for (int i = 0; i < amountToCast; i++)
        {
            bool playerIsMoving = playerScript.rb.linearVelocity.magnitude > 0;

            float xOffset = playerIsMoving ? playerOffsetPrediction.x * playerScript.facingDir : 0;
            Vector3 spellPosition = playerScript.transform.position + new Vector3(xOffset, playerOffsetPrediction.y);

            Enemy_ReaperSpell projectile =
                Instantiate(spellCastPrefab, spellPosition, Quaternion.identity).GetComponent<Enemy_ReaperSpell>();

            projectile.SetupSpell(combat, spellDamageScale);
            yield return new WaitForSeconds(spellCastRate);
        }

        SetSpellCastPerformed(true);
    }

    public bool CanDoSpellCast() => Time.time > lastTimeCastedSpells + spellCastStateCooldown;

    public void SetSpellCastOnCooldown() => lastTimeCastedSpells = Time.time;

    public Vector3 FindTeleportPoint()
    {
        int maxAttempts = 10;
        float bossWithColliderHalf = col.bounds.size.x / 2;

        for (int i = 0; i < maxAttempts; i++)
        {
            float randomX = Random.Range(arenaBounds.bounds.min.x + bossWithColliderHalf, arenaBounds.bounds.max.x - bossWithColliderHalf);

            Vector2 rayCastPoint = new Vector2(randomX, arenaBounds.bounds.max.y);

            RaycastHit2D hit = Physics2D.Raycast(rayCastPoint, Vector2.down, Mathf.Infinity, whatIsGround);

            if (hit.collider != null)
                return hit.point + new Vector2(0, offsetCenterY);
        }

        return transform.position;
    }
}
