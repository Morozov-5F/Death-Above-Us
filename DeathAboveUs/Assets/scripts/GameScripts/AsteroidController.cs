using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour 
{
	private Vector3 velocity; 
	private float rotationSpeed;
	private float mass;
    private float hp;

	public Vector3 Direction
	{
		get
		{
			return velocity;
		}
		set 
		{
			velocity = value.normalized / mass;
		}
	}

	public Sprite AseroidSprite
	{
		get
		{
			var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
			return spriteRenderer.sprite;
		}
		set 
		{
			var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
			spriteRenderer.sprite = value;

			var collider = gameObject.GetComponent<CircleCollider2D>();
			collider.radius = spriteRenderer.bounds.size.magnitude / 2 * 0.8f;

			mass = spriteRenderer.bounds.size.magnitude * 10;
            hp = 100 * mass;
			rotationSpeed = Random.Range(-50.0f, 50.0f);
		}
	}

	void Start () 
	{
        transform.Rotate(0f, 0f, Random.Range(0f, 360f));
	}

    void Update () 
	{
		transform.Translate(velocity * Time.deltaTime, Space.World);
		transform.Rotate (new Vector3 (0, 0, rotationSpeed) * Time.deltaTime);

        var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (transform.position.y <= -GameUtils.cameraHeight / 2f - spriteRenderer.bounds.size.y / 2f) 
		{
            Destroy(gameObject);
			return;
		}
	}

    void OnCollision(float damage)
    {
        hp -= damage;
        if (hp <= 0)
            Destroy(gameObject);
    }
}
