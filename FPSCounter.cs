using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FPSCounter : MonoBehaviour
{
	public  float updateInterval = 0.5F;
	private float _ac = 0; // FPS accumulated over the interval
	private int   _frames = 0; // Frames drawn over the interval
	private float _timeLeft; // Left time for current interval
	private Text _textField;

	void Start ()
	{
		_textField = GetComponent<Text> ();
		if (_textField == null) {
			Debug.Log ("FPS counter needs a Text component!");
			enabled = false;
			return;
		}
		_timeLeft = updateInterval;  
	}
	
	void Update ()
	{
		_timeLeft -= Time.deltaTime;
		_ac += Time.timeScale / Time.deltaTime;
		++_frames;

		if (_timeLeft <= 0.0f) {
			float fps = _ac / _frames;
			_textField.text = string.Format ("{0:F2} FPS", fps);
			
			if (fps < 30f) {
				_textField.color = Color.yellow;
			} else if (fps < 10f) {
				_textField.color = Color.red;
			} else {
				_textField.color = Color.green;
			}
			_timeLeft = updateInterval;
			_ac = 0.0f;
			_frames = 0;
		}
	}
}