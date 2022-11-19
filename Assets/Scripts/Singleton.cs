using UnityEngine;

/// <summary>
/// Generic Singleton class.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : Component
{
	private static T instance;
	/// <summary>
	/// Single instance of inherited class 
	/// </summary>
	public static T Instance
	{
		get
		{
			if (instance == null)
			{
				// Finding inherited class if it is already placed in scene
				instance = FindObjectOfType<T>();
				// Placing inherited class to scene hierarchy 
				//if (instance == null)
				//{
				//	GameObject obj = new GameObject();
				//	obj.name = typeof(T).Name;
				//	instance = obj.AddComponent<T>();
				//}
			}
			return instance;
		}
	}
	/// <summary>
	/// Property for this singleton is already initialized (readonly)
	/// </summary>
	public bool Initialized { get => instance != null; }

	protected virtual void Awake()
	{
		if (this.Equals(instance))
		{
			DontDestroyOnLoad(gameObject);
		}
		// Set this object as only isnstance
		if (instance == null)
		{
			instance = this as T;
			DontDestroyOnLoad(gameObject);
		}
		// If there are duplicates of singleton, destroy them
		else if (!this.Equals(instance))
		{
			Destroy(gameObject);
		}
	}
}
