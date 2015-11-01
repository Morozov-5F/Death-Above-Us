using UnityEngine;

public class ProjectileMovementController : MonoBehaviour
{
	private bool initialized;
	private float velocity;
	
	void Start () 
	{
		
	}
	
	void Initialize(float vel)
	{
		velocity = vel;
	}
	
	void Update () 
	{	
		var angle = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
		
		var offsetX = velocity * Mathf.Sin(-angle) * Time.deltaTime;
		var offsetY = velocity * Mathf.Cos(angle) * Time.deltaTime;
		
		//  transform.position.Set(newPosX, newPosY, 0);
		
		transform.Translate(offsetX, offsetY, 0);
		
		if (transform.position.y >= 2)
		{
			Destroy(gameObject);
			return;
		}
	}
}
