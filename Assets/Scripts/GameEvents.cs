using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{

	public static Action<string> OnLog;
	public static void Log(string s)
	{
		OnLog?.Invoke(s);
	}

	public static Action<string> OnSaveName;
	public static void SaveName(string s)
	{
		OnSaveName?.Invoke(s);
	}
	#region Quests

	public static Action<string> OnSetQuestInfo;
	public static void SetQuestInfo(string questDescription)
	{
		OnSetQuestInfo?.Invoke(questDescription);
	}

	public static Action<string, int> OnQuestEvent_PickItem;
	public static void QuestEvent_PickItem(string itemName, int itemAmount)
	{
		OnQuestEvent_PickItem?.Invoke(itemName, itemAmount);
	}

	public static Action<string> OnQuestEvent_TalkToNpc;
	public static void QuestEvent_TalkToNpc(string npcName)
	{
		OnQuestEvent_TalkToNpc?.Invoke(npcName);
	}

	#endregion

	#region Messages
	public static Action<string> OnButtonInputPressed;
	public static void ButtonInputPressed(string s)
	{
        OnButtonInputPressed?.Invoke(s);
    }

	public static Action<string> OnShowDialog;
	public static void ShowDialog(string id)
	{
		OnShowDialog?.Invoke(id);
	}

	public static Action<ModuleDialogData> OnShowDialogData;
	public static void ShowDialogData(ModuleDialogData dialogData)
	{
		OnShowDialogData?.Invoke(dialogData);
	}

	public static Action<string, int> OnGiveItem;
	public static void GiveItem(string itemName, int itemAmount)
	{
		OnGiveItem?.Invoke(itemName, itemAmount);
	}

	public static Action OnHideDialog;
	public static void HideDialog()
	{
		OnHideDialog?.Invoke();
	}
    #endregion

    #region MainMenu events

    public static Action OnLoadGameplay;
	public static void LoadGameplay()
	{
		OnLoadGameplay?.Invoke();
	}

	public static Action OnShowIntro;
	public static void ShowIntro()
	{
		OnShowIntro?.Invoke();
	}

	public static Action OnShowMainMenu;
	public static void ShowMainMenu()
	{
		OnShowMainMenu?.Invoke();
	}

	public static Action OnShowNameInput;
	public static void ShowNameInput()
	{
		OnShowNameInput?.Invoke();
	}

	public static Action OnShowOptions;
	public static void ShowOptions()
	{
		OnShowOptions?.Invoke();
	}
	#endregion

	#region Gameplay events

	public static Action OnBlockGame;
	public static void BlockGame()
	{
		OnBlockGame?.Invoke();
	}

	public static Action<List<InventoryItem>> OnShowInventory;
	public static void ShowInventory(List<InventoryItem> inventory)
	{
		OnShowInventory?.Invoke(inventory);
	}

	public static Action OnShowMoreOptions;
	public static void ShowMoreOptions()
	{
		OnShowMoreOptions?.Invoke();
	}

	public static Action<PlayerAction> OnPlayerActionSelected;
	public static void PlayerActionSelected(PlayerAction pa)
	{
		OnPlayerActionSelected?.Invoke(pa);
	}

	public static Action OnSwapTopInfo;
	public static void SwapTopInfo()
	{
		OnSwapTopInfo?.Invoke();
	}

	#endregion

	#region Audio
	public static Action<AudioNames> OnPlayAudio;
	public static void PlayAudio(AudioNames audioName)
	{
		OnPlayAudio?.Invoke(audioName);
	}

	public static Action OnStopCurrentMusic;
	public static void StopCurrentMusic()
	{
		OnStopCurrentMusic?.Invoke();
	}

	public static Action<Vector2> OnQuestEvent_ReackPosition;
	public static void QuestEvent_ReackPosition(Vector2 pos)
	{
		OnQuestEvent_ReackPosition?.Invoke(pos);
	}
    #endregion
}