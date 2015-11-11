using UnityEngine;
using System.Collections.Generic;

public class SimpleTurretController : MonoBehaviour 
{
	private GameObject target;
	public float RadarRadius = 2;
	public float RadarError = 5;
	public float RotationSpeed = 5;
	private TurretWeapon weapon;
	public float MAX_ANGLE = 65f;
	void Start () 
	{
		weapon = GetComponent<TurretWeapon>();
		target = null;
	}
	
	GameObject FindTarget()
	{
		GameObject newTarget = null;
		// Пока тест. Далее сделать слои столкновений
		var colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, RadarRadius);
		float minRadius = 100; 
		foreach (var currentCollider in colliders)
		{
			if (currentCollider.gameObject.tag == "Asteroid")
			{
				float currentRadius = (currentCollider.gameObject.transform.position - gameObject.transform.position).sqrMagnitude;
				if (currentRadius < minRadius)
				{
					minRadius = currentRadius;
					newTarget = currentCollider.gameObject;
				}
			}
		}
		if (newTarget)
		{
			var sr = newTarget.GetComponent<SpriteRenderer>();
			sr.color = Color.red;
		}
		else 
		{
			weapon.isShooting = false;
		}
		return newTarget;
	}
	
	void Update () 
	{
		if (target == null)
			target = FindTarget();
		if (target == null)
		{
			return;
		}
		var targetScript = target.GetComponent<AsteroidController>();
		if (targetScript.hp <= 0)
		{
			target = null;
			return;
		}
		Vector3 vecToTarget = target.transform.position - gameObject.transform.position;	
		float desiredAngle = GameUtils.WrapAngle(Mathf.Atan2(vecToTarget.y, vecToTarget.x) * Mathf.Rad2Deg - 90);
		float delta = desiredAngle - GameUtils.WrapAngle(gameObject.transform.eulerAngles.z);
		if (Mathf.Abs(delta) < RadarError)
		{
			weapon.isShooting = true;
		}
		else 
		{
			transform.Rotate(new Vector3(0, 0, Mathf.Sign(delta) * RotationSpeed * Time.deltaTime));
			weapon.isShooting = false;
		}
	}
}
