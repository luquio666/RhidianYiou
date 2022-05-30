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

	// TODO: all

    #endregion
}