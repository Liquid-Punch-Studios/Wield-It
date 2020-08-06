using UnityEngine;

/// <summary>
/// Inherit from this base class to create a singleton.
/// e.g. public class MyClassName : Singleton<MyClassName> {}
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	// Check to see if we're about to be destroyed.
	private static bool shuttingDown = false;

	private static T instance;

	/// <summary>
	/// Access singleton instance through this propriety.
	/// </summary>
	public static T Instance
	{
		get
		{
			if (shuttingDown)
			{
				Debug.LogWarning($"[Singleton] Instance {typeof(T).Name} already destroyed. Returning null.");
				return null;
			}

			return instance;
		}
	}

	private void Awake()
	{
		if (instance == null)
		{
			instance = this as T;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void OnApplicationQuit()
	{
		shuttingDown = true;
	}

	private void OnDestroy()
	{
		shuttingDown = true;
	}
}