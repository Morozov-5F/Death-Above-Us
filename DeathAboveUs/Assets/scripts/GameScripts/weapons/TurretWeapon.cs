using UnityEngine;

public class TurretWeapon : Weapon 
{
	new void Start () 
	{
        base.Start();
	}
	
	new void Update () 
	{
		if (isShooting && currentReloadTime >= ReloadTime) 
		{
            SoundSource.Play();
            foreach (var currentBarrel in Barrels)
            {
                var currentProjectile = (Instantiate(Projectile, currentBarrel.transform.position, transform.rotation) as Transform).gameObject;
				currentProjectile.GetComponent<ProjectileController>().CollidableLayers = CollidableLayers;
            }
            currentReloadTime = 0;
		}
		base.Update();
	}
}
