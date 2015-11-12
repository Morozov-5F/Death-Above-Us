using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour 
{
    public GameObject explosionPrefab;
	private Vector3 velocity; 
	private float rotationSpeed;
	private float mass;
    public float hp;

    private bool isDestroying = false;
    new private ParticleSystem particleSystem;

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
        particleSystem = GetComponent<ParticleSystem>();
        transform.Rotate(0f, 0f, Random.Range(0f, 360f));
	}

    void Explode()
    {
        // Остановка создания частиц
        particleSystem.enableEmission = false;
        // Скрытие спрайта
        var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        // Отключение коллайдера
        var collider = gameObject.GetComponent<CircleCollider2D>();
        collider.enabled = false;
        // Создание взрыва
        if (explosionPrefab != null)
            Instantiate(explosionPrefab, transform.position, transform.rotation);
        // Удаление
        isDestroying = true;
    }

    void Update () 
	{
        if (!isDestroying)
        {
            transform.Translate(velocity * Time.deltaTime, Space.World);
            transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);
            // Взрыв и удаление астероида при уходе за нижний край экрана
            if (transform.position.y <= -GameUtils.cameraHeight / 2f)
            {
                Explode();
                // Тряска камеры
                Camera.main.SendMessage("Shake");
            }
        }

        // Удаление после исчезания всех частиц
        if (isDestroying && particleSystem.particleCount <= 0)
        {
            Destroy(gameObject);
		}
	}

    void OnBulletHit(float damage)
    {
        hp -= damage;
        if (hp <= 0)
            Explode();
    }
}
