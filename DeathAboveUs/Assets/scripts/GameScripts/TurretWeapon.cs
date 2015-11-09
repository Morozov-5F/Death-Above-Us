﻿using UnityEngine;
using System.Collections.Generic;

public class TurretWeapon : MonoBehaviour 
{
	public Transform Projectile;

	public float ReloadTime = 200;
	private float currentReloadTime;
    private float audioTimer;
    public bool isShooting;

	public List<GameObject> Barrels;
    public AudioSource SoundSource;

	void Start () 
	{
        isShooting = false;
        currentReloadTime = 0;
        audioTimer = 0;
        SoundSource.Stop();
	}
	
	void Update () 
	{
        if (isShooting && !SoundSource.isPlaying)
            SoundSource.Play();
        else if (!isShooting)
            SoundSource.Stop();
		if (isShooting && currentReloadTime >= ReloadTime) 
		{
            float angle = transform.localEulerAngles.z * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle));
            foreach (var currentBarrel in Barrels)
            {
                var currentProjectile = Instantiate(Projectile, currentBarrel.transform.position, transform.rotation) as GameObject;
            }
            currentReloadTime = 0;
		}
		else 
        {
            currentReloadTime += Time.deltaTime * 1000;
		}

	}
}