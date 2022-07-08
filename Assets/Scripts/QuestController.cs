using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(QuestController))]
public class QuestControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        QuestController myScript = (QuestController)target;
        if (GUILayout.Button("StartActiveQuest"))
        {
            myScript.StartActiveQuest();
        }
    }
}
#endif

public class QuestController : Singleton<QuestController>
{
    public ModuleQuest ActiveQuest;

    private Task _currentTask;
    private ModuleDialogData _dialogData;

    public void StartActiveQuest()
    {
        string questDescription = "no quests active";

        if (ActiveQuest != null)
        {
            // TODO: maybe quest name just should be shown on a quest panel -_O.O_-
            questDescription = $"{ActiveQuest.QuestName}";
            Invoke(nameof(ShowQuestTask), 3f);
        }
        GameEvents.SetQuestInfo(questDescription);
    }

    private void ShowQuestTask()
    {
        string questDescription = $"{ActiveQuest.GetFirstTaskNotCompleted().TaskDescription}";
        GameEvents.SetQuestInfo(questDescription);
    }

    private void OnEnable()
    {
        GameEvents.OnQuestEvent_PickItem += QuestEvent_PickItem;
        GameEvents.OnQuestEvent_TalkToNpc += QuestEvent_TalkToNpc;
    }

    private void OnDisable()
    {
        GameEvents.OnQuestEvent_PickItem -= QuestEvent_PickItem;
        GameEvents.OnQuestEvent_TalkToNpc -= QuestEvent_TalkToNpc;
    }

    private void QuestEvent_PickItem(string itemName, int itemAmount)
    {
        if (ActiveQuest == null) return; 

        _currentTask = ActiveQuest.GetFirstTaskNotCompleted();

        bool matchTaskType = _currentTask.TType == TaskType.FIND_ITEM;
        bool matchItemName = _currentTask.TargetID == itemName;

        if (matchTaskType && matchItemName)
        {
            _currentTask.TargetAmountProgress += itemAmount;
        }

        CheckQuestTasks(_currentTask);
    }

    private void QuestEvent_TalkToNpc(string npcName)
    {
        if (ActiveQuest == null) return;

        _currentTask = ActiveQuest.GetFirstTaskNotCompleted();

        bool matchNpcName = _currentTask.TargetID == npcName;

        if (matchNpcName)
        {
            _currentTask.TargetAmountProgress += 1;
        }

        CheckQuestTasks(_currentTask);
    }

    private void CheckQuestTasks(Task currentTask)
    {
        // if task completed, look for next task
        if (_currentTask.TaskCompleted)
        {
            // try find next task
            _currentTask = ActiveQuest.GetFirstTaskNotCompleted();

            // if no tasks, quest is completed. So give reward
            if (_currentTask == null)
            {
                GameEvents.SetQuestInfo($"{ActiveQuest.QuestName} Completed!");

                // Show reward
                Invoke(nameof(ShowQuestRewardMessage), 3f);
            }
            else
            {
                GameEvents.SetQuestInfo($"{_currentTask.TaskDescription}");
            }
        }
        else
        {
            GameEvents.SetQuestInfo($"{_currentTask.TaskDescription}");
        }
    }

    private void ShowQuestRewardMessage()
    {
        GameEvents.SetQuestInfo($"You get {ActiveQuest.QuestReward}");

        // Clean Active quest
        ActiveQuest.ClearProgress();
        ActiveQuest = null;

        Invoke(nameof(GameEvents.SwapTopInfo), 3f);
    }

}
