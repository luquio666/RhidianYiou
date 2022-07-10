using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum TaskType
{
	PICK_ITEM,
	TALK_TO,
	GO_TO_POSITION
}

[System.Serializable]
public class Task
{
	public string TaskDescription;
	public TaskType TType;
	public string TargetID;
	public int TargetAmount = 1;
	public Vector2 TargetPosition;
	public float TargetArea = 1;
	[Space]
	public int TargetAmountProgress;
	public bool TaskCompleted
	{
		get { return TargetAmountProgress >= TargetAmount; }
	}
}

public class ModuleQuest : Module
{
	// Name of the whole quest
	public string QuestName;
	// Tasks to complete quest
	public Task[] QuestTasks;
	// Reward after completing all
	public string QuestReward;

	public Task GetFirstTaskNotCompleted()
	{
		return QuestTasks.FirstOrDefault(x => x.TaskCompleted == false);
	}
	public void ClearProgress()
	{
        foreach (var item in QuestTasks)
        {
			item.TargetAmountProgress = 0;
        }
	}

}
