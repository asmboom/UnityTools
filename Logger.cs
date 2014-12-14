/// <summary>
/// Logger.
/// @author: Fabio Paes Pedro
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Logger
{
	private static Dictionary<string, string> _messages = new Dictionary<string, string>();
	private static Dictionary<string, float> _times = new Dictionary<string, float>();
	public static void Log(object obj, string message, bool isWarning = false)
	{
		Debug.Log (String.Concat((isWarning ? "\n - - WARNING : " : " + "), Time.realtimeSinceStartup.ToString(), " - ", obj.ToString(), " - ", message));
	}

	public static void BeginProfiling(object obj, string message)
	{
		string objKey = obj.ToString();
		if(!_times.ContainsKey(objKey))
		{
			_messages.Add(objKey, message);
			_times.Add(objKey, Time.realtimeSinceStartup); 
			Log (obj, String.Concat(objKey, ": start profiling tool. - ", message)); 
		}
		else
		{
			Log (obj, String.Concat(objKey, ": profiling was not finished.")); 
		}
	}

	public static void EndProfiling(object obj)
	{
		string objKey = obj.ToString();
		if(_times.ContainsKey(objKey))
		{
			float timeElapsed = Time.realtimeSinceStartup - _times[objKey];
			Log(obj, String.Concat(objKey, ": profile complete - ", "time elapsed: ", timeElapsed.ToString(), " - ", _messages[objKey]), timeElapsed > 0.05);
			_times.Remove(objKey);
			_messages.Remove(objKey);
		}
		else
		{
			Log (obj, String.Concat(objKey, " profiling was not started.")); 
		}
	}
}

