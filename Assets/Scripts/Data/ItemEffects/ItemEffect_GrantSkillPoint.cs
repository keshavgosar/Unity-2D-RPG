using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Item Data/Item Effect/ Grant Skill Point", fileName = "Item Effect data - Grant Skill Point")]
public class ItemEffect_GrantSkillPoint : ItemEffect_DataSO
{
    [SerializeField] private int pointToAdd;

    public override void ExecuteEffects()
    {
        UI ui = FindFirstObjectByType<UI>();
        ui.skillTreeUI.AddSkillPoints(pointToAdd);
    }
}
