using UnityEngine;
using System.Collections.Generic;

public class ProjectileController : MonoBehaviour 
{
	public float Damage = 5;
	public float Velocity = 2;
	public LayerMask CollidableLayers;
	public Vector3 Direction = Vector3.up;
	
	public void Start () 
	{
	}
	
	public void Update () 
	{
        if (transform.position.y >= GameUtils.cameraHeight / 2 || transform.position.y <= -GameUtils.cameraHeight / 2)
        {
            Destroy(gameObject);
        }
	}
	
	public void OnTriggerEnter2D(Collider2D collider)
    {
		if (((CollidableLayers.value >> collider.gameObject.layer) & 0x1) != 0)
		{
			Destroy(gameObject);
            collider.gameObject.SendMessage("OnBulletHit", Damage);
		}
    }
}
