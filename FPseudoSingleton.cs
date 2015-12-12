using UnityEngine;
using System.Collections;

/// <summary>
/// Pseudo singleton.
/// Generic Type extends MonoBehaviour and T also must extend MonoBehaviour
/// @author Fabio Paes Pedro
/// </summary>
public class FPseudoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
	#region PseudoSingleton

	private static T _instance;
	/// <summary>
	/// Returns a static reference of this instance
	/// </summary>
	/// <value>The instance.</value>
	public static T Instance {
		get {
			return _instance;
		}
	}

	/// <summary>
	/// Applying the static reference
	/// </summary>
	protected virtual void Awake ()
	{
		if (_instance != null) {	
			Debug.LogWarning ("There is another instance of " + this + " already. Destroying the new one");
			Destroy (this);
			return;
		}
		_instance = this as T;
	}

	/// <summary>
	/// If the object is the original instance, nullify the static reference
	/// </summary>
	protected virtual void OnDestroy ()
	{
		if (this == _instance) {
			_instance = null;
		}
	}
	#endregion
}

