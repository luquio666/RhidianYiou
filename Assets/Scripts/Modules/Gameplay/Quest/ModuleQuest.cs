using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskType
{
	FIND,
	COLLECT,
	KILL_DESTROY,
	TAKE_PICTURE,
	DELIVER,
	CRAFT
}

public enum TaskTarget
{
	ITEM,
	PERSON,
	LOCATION
}

public class Task
{
	public TaskType TType;
	public TaskTarget TTarget;

	public int TargetID;
	public Vector3 TargetPosition;
}

public class ModuleQuest : Module
{
	public Task[] QuestTasks;
}
