using UnityEngine;
using System.Collections.Generic;

public class TurretWeapon : MonoBehaviour 
{
	public Transform Projectile;

	public float ReloadTime = 200;
	private float currentReloadTime;
    public bool isShooting;

	public List<GameObject> Barrels;
    public AudioSource SoundSource;

	void Start () 
	{
        isShooting = false;
        currentReloadTime = 0;
        SoundSource.Stop();
	}
	
	void Update () 
	{

		if (isShooting && currentReloadTime >= ReloadTime) 
		{
            SoundSource.Play();
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
