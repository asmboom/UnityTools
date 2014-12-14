using UnityEngine;
using System.Collections;

public class AlignManager : MonoBehaviour {
	public delegate void OnOrientationChangeAction();
	public static event OnOrientationChangeAction OnOrientationChange;
	
	int _screenHeight;
	int _screenWidth;
	float _checkInterval = 0.5f;
	void Start()
	{
		_screenHeight = Screen.height;
		_screenWidth = Screen.width;
		StartCoroutine(CheckOrientation());
	}

	/// <summary>
	/// Detecting screen orientation changes
	/// * There's a bug with Screen.orientation so I had to use Screen.width instead
	/// </summary>
	IEnumerator CheckOrientation()
	{
		while(true)
		{
			yield return new WaitForSeconds(_checkInterval);
			if(_screenHeight != Screen.height || _screenWidth != Screen.width)
			{
				_screenHeight = Screen.height;
				_screenWidth = Screen.width;
				OnOrientationChange ();
			}
		}
	}
}
