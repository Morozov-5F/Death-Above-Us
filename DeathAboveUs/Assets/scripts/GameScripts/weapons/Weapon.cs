using UnityEngine;
using System.Collections.Generic;

public class Weapon : MonoBehaviour 
{
	public Transform Projectile;
	public float ReloadTime = 200;
	protected float currentReloadTime;
    public bool isShooting;
	public List<GameObject> Barrels;
    public AudioSource SoundSource;
	public LayerMask CollidableLayers;
	
	public void Start () 
	{
        currentReloadTime = 0;	
        SoundSource.Stop();
	}
	
	public void Update () 
	{
		currentReloadTime += Time.deltaTime * 1000;
	}
}
