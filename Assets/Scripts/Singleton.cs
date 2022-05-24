using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {

	private static T s_Instance;

	public static T Instance
	{ 
		get {
			if (!s_Instance)
			{
				Debug.Log ("Singleton doesnt exist in the scene " + typeof(T));
			}
			return s_Instance;
		}
	}

	public virtual void Awake ()
	{
		if (s_Instance != null)
		{
			//Debug.LogError ("There's already an instance of MyManager. Is it more than once in the scene?", this);
		}

		s_Instance = (T)(object)this;
	}
}