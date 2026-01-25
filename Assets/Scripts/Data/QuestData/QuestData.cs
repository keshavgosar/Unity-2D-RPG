using System;
using UnityEngine;

[Serializable]
public class QuestData
{
    public QuestDataSO questDataSO;
    public int currentAmount;
    public bool canGetReward;


    public QuestData(QuestDataSO questSO)
    {
        this.questDataSO = questSO;
    }

    public void AddQuestProgress(int amount = 1)
    {
        currentAmount += amount;
        canGetReward = CanGetReward();
    }

    public bool CanGetReward() => currentAmount >= questDataSO.requiredAmount;
}
