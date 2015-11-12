using UnityEngine;
using System.Collections;

public class FlyingAssEnemyController : Enemy 
{
	private Weapon weapon;
	new void Start()
	{
		weapon = GetComponent<Weapon>();
		base.Start();
	}
	
	void Update () 
	{
		var target = Physics2D.Raycast(transform.position, Vector2.down, 100, weapon.CollidableLayers).collider;
		
        if (target == null)
		{
			transform.Translate(Time.deltaTime * 0.1f, 0f, 0f);
			weapon.isShooting = false;
		}
		else 
		{
			weapon.isShooting = true;
		}
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Sin(Time.time * 2f) * 5f);
	}
}
