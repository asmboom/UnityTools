
using UnityEngine;
using System.Collections;

public class Align : MonoBehaviour
{
	public enum AlignTo
	{
		TopLeft, 
		Top, 
		TopRight,
		Right,
		BottomRight,
		Bottom, 
		BottomLeft,
		Left
	}
	public bool autoSetToStatic = false;
	public AlignTo alignTo;
	public float percent = 1f;
	public float margin = 0f;
	Vector3 _selfPosition;
	Bounds _cameraBounds;

	
	void Start ()
	{
		UpdateInterface();
		AlignManager.OnOrientationChange += HandleOnOrientationChange;
	}

	void UpdateInterface()
	{
		if(autoSetToStatic)
		{
			gameObject.isStatic = false;
		}

		_selfPosition = Vector3.zero;
		_cameraBounds = FTools.ScreenOrthoBounds(Camera.main);

		switch(alignTo)
		{
		case AlignTo.TopLeft:
			_selfPosition.x = (- _cameraBounds.extents.x * percent) + margin;
			_selfPosition.y = (_cameraBounds.extents.y * percent) + margin;
			break;
		case AlignTo.Top:
			_selfPosition.x = (_cameraBounds.center.x * percent) + margin;
			_selfPosition.y = (_cameraBounds.extents.y * percent) + margin;
			break;
		case AlignTo.TopRight:
			_selfPosition.x = (_cameraBounds.extents.x * percent) + margin;
			_selfPosition.y = (_cameraBounds.extents.y * percent) + margin;
			break;
		case AlignTo.Right:
			_selfPosition.x = (_cameraBounds.extents.x * percent) + margin;
			_selfPosition.y = (_cameraBounds.center.y * percent) + margin;
			break;
		case AlignTo.BottomRight:
			_selfPosition.x = (_cameraBounds.extents.x * percent) + margin;
			_selfPosition.y = (- _cameraBounds.extents.y * percent) + margin;
			break;
		case AlignTo.Bottom:
			_selfPosition.x = (_cameraBounds.center.x * percent) + margin;
			_selfPosition.y = (- _cameraBounds.extents.y * percent) + margin;
			break;
		case AlignTo.BottomLeft:
			_selfPosition.x = (- _cameraBounds.extents.x * percent) + margin;
			_selfPosition.y = (- _cameraBounds.extents.y * percent) + margin;
			break;
		case AlignTo.Left:
			_selfPosition.x = (- _cameraBounds.extents.x * percent) + margin;
			_selfPosition.y = (_cameraBounds.center.y * percent) + margin;
			break;
		}
		
		transform.position = _selfPosition;
		if(autoSetToStatic)
		{
			gameObject.isStatic = true;
		}
	}

	void HandleOnOrientationChange ()
	{
		UpdateInterface();

	}


}

