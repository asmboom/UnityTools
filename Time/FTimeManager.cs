/// <summary>
/// Time Manager.
/// You can also use this Manager as a Pause / Play Manager
/// Time.deltaTime independent Time.timeScale Lerp.
/// Author: Fabio Paes Pedro
/// </summary>
///
/// 

using UnityEngine;
using System.Collections;

public class FTimeManager : MonoBehaviour
{
    /// <summary>
    /// FTimeManager is Paused or not
    /// </summary>
    [SerializeField]
    private bool _isPaused = false;
    /// <summary>
    /// FTimeManager is Fading (from _minScale to _scale or vice-versa)
    /// </summary>
    [SerializeField]
    private bool _isFading = false;
    /// <summary>
    /// FTimeManager will pause after fading (is going from _scale to _minScale)
    /// </summary>
    [SerializeField]
    private bool _willPause = false;

    [SerializeField]
    private float _scale = 1f;
    private float _fadeToScaleDifference = 0f;
    private float _scaleToFade = 0f;
    [SerializeField]
    private float _deltaTime = 0f;
    private float _lastTime = 0f;
    private float _maxScale = 3f;
    private float _minScale = 0f;
    private bool _fadeToScaleIsGreater = false;



    #region PseudoSingleton
    private static FTimeManager _instance;
    public static FTimeManager Instance
    {
        get
        {
            return _instance;
        }
    }
    void Awake()
    {
        if (_instance != null)
		{	
			Destroy (this);
			return;
		}
        _instance = this;
		Scale = Time.timeScale;
		StartCoroutine("UpdateDeltaTime");
    }
    void OnDestroy()
    {
		StopAllCoroutines ();
		if(this != _instance)
		{
			Debug.LogWarning(this.gameObject.name+": There's another instance of " + this + " already at "+_instance.gameObject.name+". Destroying this new one.");
			return;
		}
        _instance = null;
    }
    #endregion


    public bool WillPause
    {
        get
        {
            return _willPause;
        }
    }

    public bool IsFading
    {
        get
        {
            return _isFading;
        }
    }

    public bool IsPaused
    {
        get
        {
            return _isPaused;
        }
    }


    /// <summary>
    /// Time.timeScale independent deltaTime
    /// </summary>
    /// <value>
    /// time.timeScale independent Delta Time
    /// </value>
    public float DeltaTime
    {
        get
        {
            return _deltaTime;
        }
    }

    /// <summary>
    /// Getter and Setter for the FTimeManager Scale (time.timeScale). This will set IsPaused to true automatically if the scale == 0f
    /// </summary>
    /// <value>
    /// Scale (Time.timeScale)
    /// </value>
    public float Scale
    {
        get
        {
            return _scale;
        }
        set
        {
            _scale = value;
            _scale = _scale < _minScale ? _minScale : _scale > _maxScale ? _maxScale : _scale;
            Time.timeScale = _scale;
            _isPaused = _scale <= 0.001f;
            if (_isPaused)
            {
                _scale = 0f;
                _willPause = false;
            }
        }
    }

    /// <summary>
    /// Pause toggle (Changes the "IsPaused" flag from true to false and vice-versa)
    /// </summary>
    /// </param>
    /// <param name='time'>
    /// Time until Pause or Play
    /// </param>
    /// <param name='playScale'>
    /// Play scale.
    /// </param>
    public void TogglePause(float time = 0f, float playScale = -1f)
    {
        StopStepper();
        // WillPause == true means that a "Pause" was already called: this will make "WillPause" change to false and call "Play" function. 
        // Else just toggle.
        _willPause = _willPause == true ? false : !_isPaused;
        if (_willPause)
        {
            Pause(time);
        }
        else
        {
            Play(time, playScale);
        }
    }

    void StopStepper()
    {
        _instance.StopCoroutine("FadeStepper");
    }

    /// <summary>
    /// FTimeManager Pause
    /// </summary>
    /// <param name='time'>Fading time until Time.timeScale == 0f</param>
    public void Pause(float time = 0f)
    {
        if (Mathf.Approximately(time, 0f))
        {
            _willPause = false;
            _instance.StopCoroutine("FadeStepper");
            Scale = 0f;
        }
        else
        {
            _willPause = true;
            FadeTo(0f, time);
        }
    }

    /// <summary>
    /// FTimeManager Play 
    /// </summary>
    /// <param name='time'>
    /// Fading time until Time.timeScale == scale param
    /// </param>
    /// <param name='scale'>
    /// Final scale for Time.timeScale
    /// </param>
    public void Play(float time = 0f, float scale = 1f)
    {
        if (Mathf.Approximately(time, 0f))
        {
            _instance.StopCoroutine("FadeStepper");
            Scale = scale;
        }
        else
        {
            FadeTo(scale, time);
        }
    }

    /// <summary>
    /// FTimeManager Scale Fading.
    /// </summary>
    /// <param name='scale'>
    /// The final Time.timeScale
    /// </param>
    /// <param name='time'>
    /// The transition time to reach the desired scale
    /// </param>
    public void FadeTo(float scale, float time)
    {
        _instance.StopCoroutine("FadeStepper");
        _scaleToFade = scale;
        _fadeToScaleDifference = scale - _scale;
        _fadeToScaleIsGreater = _fadeToScaleDifference > 0f;
        float scalePerFrame = _fadeToScaleDifference / time;
        _instance.StartCoroutine("FadeStepper", scalePerFrame);
    }

    /// <summary>
    /// Coroutine to fade the Unity's timeScale
    /// </summary>
    IEnumerator FadeStepper(float scalePerFrame)
    {
        bool achieved = false;
        _isFading = true;
        while (achieved == false)
        {
            Scale += scalePerFrame * _deltaTime;
            if (_fadeToScaleIsGreater)
            {
                achieved = _scale >= _scaleToFade;
            }
            else
            {
                achieved = _scale <= _scaleToFade;
            }
            yield return null;
        }
        Scale = _scaleToFade;
        _isFading = false;
        _willPause = false;
    }

    /// <summary>
    /// Updating our internal _deltaTime
    /// </summary>
    IEnumerator UpdateDeltaTime()
    {
        while (true)
        {
            float timeSinceStartup = Time.realtimeSinceStartup;
            _deltaTime = timeSinceStartup - _lastTime;
            _lastTime = timeSinceStartup;
            yield return null;
        }
    }

}
