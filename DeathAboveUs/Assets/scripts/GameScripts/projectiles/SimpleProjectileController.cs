using UnityEngine;

public class SimpleProjectileController : ProjectileController 
{	
    new void Start () 
    {
       base.Start();
	}

    new void Update()
    {
        float angle = transform.localEulerAngles.z * Mathf.Deg2Rad;
        Direction = new Vector3(-Mathf.Sin(angle), Mathf.Cos(angle));
        
        transform.Translate(Direction * Velocity * Time.deltaTime, Space.World);    
     
        base.Update();
    }
    
    new void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);
    }
}
