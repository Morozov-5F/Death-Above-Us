using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour 
{
    public GameObject explosionPrefab;
	private Vector3 velocity; 
	private float rotationSpeed;
	private float mass;

    private bool isDestroying = false;
    private ParticleSystem particleSystem;

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

			rotationSpeed = Random.Range(-10.0f, 10.0f);
		}
	}

	void Start () 
	{
        particleSystem = GetComponent<ParticleSystem>();
        transform.Rotate(0f, 0f, Random.Range(0f, 360f));
	}

    void Update () 
	{
        if (!isDestroying)
        {
            transform.Translate(velocity * Time.deltaTime, Space.World);
            transform.Rotate(new Vector3(0, 0, rotationSpeed / mass) * Time.deltaTime);

            var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            // Взрыв и удаление астероида при уходе за нижний край экрана
            if (transform.position.y <= -GameUtils.cameraHeight / 2f)
            {
                // Остановка создания частиц
                particleSystem.enableEmission = false;
                // Скрытие спрайта
                spriteRenderer.enabled = false;
                // Тряска камеры
                Camera.main.SendMessage("Shake");
                // Создание взрыва
                if (explosionPrefab != null)
                    Instantiate(explosionPrefab, transform.position, transform.rotation);
                // Удаление
                isDestroying = true;
            }
        }

        // Удаление после исчезания всех частиц
        if (isDestroying && particleSystem.particleCount <= 0)
        {
            Destroy(gameObject);
        }
    }
}
