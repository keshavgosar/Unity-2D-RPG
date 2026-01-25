using TMPro;
using UnityEngine;

public class UI_ActiveQuestPreview : MonoBehaviour
{
    private Player_QuestManager questManager;

    [SerializeField] private TextMeshProUGUI questName;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI progress;
    [SerializeField] private UI_QuestRewardSlot[] questRewardSlots;


    public void SetupQuestPreview(QuestData questData)
    {
        questManager = Player.instance.questManager;
        QuestDataSO questDataSO = questData.questDataSO;

        questName.text = questDataSO.name;
        description.text = questDataSO.description;

        progress.text = questDataSO.questGoal + " " + questManager.GetQuestProgress(questData) + "/" + questDataSO.requiredAmount;

        foreach(var obj in questRewardSlots)
            obj.gameObject.SetActive(false);

        for (int i = 0; i < questDataSO.rewardItems.Length; i++)
        {
            questRewardSlots[i].gameObject.SetActive(true);
            questRewardSlots[i].UpdateSlot(questDataSO.rewardItems[i]);
        }
    }
}
