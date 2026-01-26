using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Quest Data/Quest Database", fileName = "QUEST DATABASE")]
public class QuestDatabaseSO : ScriptableObject
{
    public QuestDataSO[] allQuest;

    public QuestDataSO GetQuestById(string id)
    {
        return allQuest.FirstOrDefault(quest => quest != null && quest.questSaveId == id);
    }

#if UNITY_EDITOR
    [ContextMenu("Auto-fill wiht all QuestDataSO")]
    public void CollectItemsData()
    {
        string[] guids = AssetDatabase.FindAssets("t:QuestDataSO");

        allQuest = guids
            .Select(guid => AssetDatabase.LoadAssetAtPath<QuestDataSO>(AssetDatabase.GUIDToAssetPath(guid)))
            .Where(quest => quest != null)
            .ToArray();

        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }
#endif
}
