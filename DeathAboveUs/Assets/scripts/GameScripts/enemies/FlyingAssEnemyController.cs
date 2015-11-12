﻿using UnityEngine;
using System.Collections;

public class Enemy1Controller : Enemy 
{
	new void Start()
	{
		base.Start();
	}
	
	void Update () 
	{
        transform.Translate(Time.deltaTime * 0.1f, 0f, 0f);
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Sin(Time.time * 2f) * 5f);
	}
}