﻿using UnityEngine;
using UnityEngine.UI;

public class FPSController : MonoBehaviour {

	private Text txt;
	public float updateTime = 0;
	private float currentUpdateTime;
	// Use this for initialization
	void Start () 
	{
		txt = gameObject.GetComponent("Text") as Text;
		txt.text = "FPS: 0";
	}
	
	// Update is called once per frame
	void Update () 
	{
		var fps = Mathf.Round(1.0f / Time.deltaTime);
		
		currentUpdateTime += Time.deltaTime;
		if (currentUpdateTime * 1000f >= updateTime)
		{
			txt.text = "FPS: " + fps;
			if (fps >= 50)
				txt.color = Color.green;
			else if (fps < 50 && fps >= 30)
				txt.color = Color.yellow;
			else if (fps < 30)
				txt.color = Color.red;
			currentUpdateTime = 0;
		}
	}
}
