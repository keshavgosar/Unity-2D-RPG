using UnityEngine;

public class Skill_Base : MonoBehaviour
{
    public Player player { get; private set; }


    [Header("General Details")]
    [SerializeField] protected SkillType skillType;
    [SerializeField] protected SkillUpgradeType upgradeType;
    [SerializeField] protected float cooldown;
    private float lastTimeUsed;


    protected virtual void Awake()
    {
        player = GetComponentInParent<Player>();
        lastTimeUsed = lastTimeUsed - cooldown;
    }

    public virtual void TryUseSkill()
    {

    }

    public void SetSkillUpgrade(UpgradeData upgrade)
    {
        upgradeType = upgrade.upgradeType;
        cooldown = upgrade.cooldown;
    }


    public bool CanUseSkill()
    {
        if(upgradeType == SkillUpgradeType.None) 
            return false;

        if (OnCooldown())
        {
            Debug.Log("On Cooldown!!");
            return false;
        }
            

        return true;
    }

    protected bool Unlocked(SkillUpgradeType upgradeCheck) => upgradeType == upgradeCheck;

    protected bool OnCooldown() => Time.time < lastTimeUsed + cooldown;
    public void SetSkillOnCoolDown() => lastTimeUsed = Time.time;
    public void ResetCooldownBy(float cooldownReduction) => lastTimeUsed = lastTimeUsed + cooldownReduction;
    public void ResetCooldown() => lastTimeUsed = Time.time;
}
