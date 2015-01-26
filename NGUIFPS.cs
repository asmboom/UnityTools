﻿using UnityEngine;
using System.Collections;

public class NGUIFPS : MonoBehaviour {
	public  float updateInterval = 0.5F;
	
	private float accum   = 0; // FPS accumulated over the interval
	private int   frames  = 0; // Frames drawn over the interval
	private float timeleft; // Left time for current interval
	
	void Start(){
		if( !GetComponent<UILabel>() ){
			Debug.Log("FPS counter needs a UILabel component!");
			enabled = false;
			return;
		}
		timeleft = updateInterval;  
	}
	
	void Update(){
		timeleft -= Time.deltaTime;
		accum += Time.timeScale/Time.deltaTime;
		++frames;
		
		// Interval ended - update GUI text and start new interval
		if( timeleft <= 0.0 ){
			// display two fractional digits (f2 format)
			float fps = accum/frames;
			string format = System.String.Format("{0:F2} FPS",fps);
			GetComponent<UILabel>().text = format;
			
			if(fps < 30)
			{
				GetComponent<UILabel>().material.color = Color.yellow;
			}
			else if(fps < 10)
			{
				GetComponent<UILabel>().material.color = Color.red;
			}
			else
			{
				GetComponent<UILabel>().material.color = Color.green;
			}
			//    DebugConsole.Log(format,level);
			timeleft = updateInterval;
			accum = 0.0F;
			frames = 0;
		}
	}
}