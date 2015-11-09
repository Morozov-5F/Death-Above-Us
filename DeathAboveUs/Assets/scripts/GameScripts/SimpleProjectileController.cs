using UnityEngine;
using System.Collections;

public class SimpleProjectileController : MonoBehaviour 
{	
    public float Velocity = 2;
    public float Damage = 5;
    private Vector3 direction = Vector3.up;

    void Start () 
    {
       
	}

	void Update () 
    {
        float angle = transform.localEulerAngles.z * Mathf.Deg2Rad;
        direction = new Vector3(-Mathf.Sin(angle), Mathf.Cos(angle));

        transform.Translate(direction * Velocity * Time.deltaTime, Space.World);

        if (transform.position.y >= GameUtils.cameraHeight / 2)
        {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Asteroid")
        {
            Destroy(gameObject);
            collider.gameObject.SendMessage("OnCollision", Damage);
        }
    }
}
