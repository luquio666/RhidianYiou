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

	public static Action<string> OnGiveItem;
	public static void GiveItem(string id)
	{
		OnGiveItem?.Invoke(id);
	}

	public static Action OnHideDialog;
	public static void HideDialog()
	{
		OnHideDialog?.Invoke();
	}

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

	public static Action<Vector2> OnPlayerNewPosition;
	public static void PlayerNewPosition(Vector2 pos)
	{
		OnPlayerNewPosition?.Invoke(pos);
	}

	#endregion
}