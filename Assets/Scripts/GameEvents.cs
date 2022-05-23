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
}