using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
	public delegate void OnEscPressedAction();
	public static event OnEscPressedAction OnEscPressed;

	public delegate void OnSpacePressedAction();
	public static event OnSpacePressedAction OnSpacePressed;

	public delegate void OnButtonPressedAction();
	public static event OnSpacePressedAction OnButtonPressed;

	Vector3 _inputPos;
	Touch _touch;

	void Start()
	{
		_inputPos = Vector3.zero;
	}

	public Vector3 inputPosition
	{
		get
		{
			return _inputPos;
		}

	}

	// Update is called once per frame
	void Update ()
	{

		if(Input.touchCount > 0)
		{
			_touch = Input.GetTouch(0);
			// Touch input
			_inputPos = _touch.position;
		}
		else
		{
			// Mouse Input
			_inputPos = Input.mousePosition;
		}

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			OnEscPressed();
		}
		if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Menu))
		{
			OnSpacePressed();
		}


	}
}

