using UnityEngine;
using System.Collections;

/// <summary>
/// Simple timer class with events for each timer step (Started, Paused, Stopped and Completed)
/// </summary>
public class FTimer : MonoBehaviour
{
	public delegate void TimerStarted ();

	public event TimerStarted OnTimerStarted;

	public delegate void TimerPaused ();

	public event TimerPaused OnTimerPaused;

	public delegate void TimerStopped ();

	public event TimerStopped OnTimerStopped;

	public delegate void TimerCompleted ();

	public event TimerCompleted OnTimerCompleted;

	private bool _isComplete = false;
	private bool _isRunning = false;
	private float _startTime;
	private float _timeLeft;
	private float _secondsLeft;
	private float _s;
	private float _m;
	private string _timeString;
	private float _secondsToCount;

	public string TimeString {
		get {
			return _timeString;
		}
	}

	public float SecondsLeft {
		get {
			return _secondsLeft;
		}
	}
	
	public float PercentLeft {
		get {
			return _secondsLeft / _secondsToCount;
		}
	}
	
	public bool IsComplete {
		get {
			return _isComplete;
		}
	}

	public bool IsRunning {
		get {
			return _isRunning;
		}
	}

	void OnDestroy ()
	{
		StopAllCoroutines ();
	}

	/// <summary>
	/// Starts the timer.
	/// </summary>
	/// <param name="seconds">Seconds.</param>
	public void StartTimer (float seconds)
	{
		_startTime = Time.time;
		_secondsToCount = seconds;
		_secondsLeft = seconds;
		_isRunning = true;
		StartCoroutine (UpdateTimer ());
		if (OnTimerStarted != null) {
			OnTimerStarted ();
		}
	}
	
	public void PauseTimer ()
	{
		_isRunning = false;
		if (OnTimerPaused != null) {
			OnTimerPaused ();
		}
	}
	
	public void StopTimer ()
	{
		_isRunning = false;
		_secondsLeft = _secondsToCount;
		if (OnTimerStopped != null) {
			OnTimerStopped ();
		}
	}
	
	IEnumerator UpdateTimer ()
	{
		while (true) {
			if (_isRunning == false) {
				yield return null;
			}
			if (_secondsLeft < 1f && _isRunning == true) {
				_isRunning = false;
				_isComplete = true;
				if (OnTimerCompleted != null) {
					OnTimerCompleted ();
				}
				yield break;
			}
			_isComplete = false;
			_timeLeft = Time.time - _startTime;
			_secondsLeft = Mathf.Ceil (_secondsToCount - _timeLeft);
			_s = _secondsLeft % 60f;
			_m = (_secondsLeft / 60f) % 60f;
			_timeString = string.Concat (_m.ToString (), ":");
			if (_s > 9f) {
				_timeString = string.Concat (_timeString, _s.ToString ());
			} else {
				_timeString = string.Concat (_timeString, "0", _s.ToString ());
			}
			yield return new WaitForSeconds (1f);
		}
	}
}