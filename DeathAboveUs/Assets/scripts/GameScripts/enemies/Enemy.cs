using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
    public GameObject explosionPrefab;
    public float MAX_HEALTH = 10000f;
    protected float health;

    // Smoke
    private ParticleSystem smokeEmitter;
    public float MAX_SMOKE_RATE = 10;

	// Use this for initialization
	public void Start () 
    {
        var smokeObject = transform.Find("Engine Smoke");
        if (smokeObject)
        {
            smokeEmitter = smokeObject.GetComponent<ParticleSystem>();
        }
        if (smokeEmitter)
        {
            smokeEmitter.Play();
            smokeEmitter.emissionRate = 0;
        }

        health = MAX_HEALTH;
    }

    protected void Explode()
    {
        // Создание взрыва
        if (explosionPrefab != null)
            Instantiate(explosionPrefab, transform.position, transform.rotation);
    }

    public void OnBulletHit(float damage)
    {
        health = Mathf.Max(0f, health - damage);
        if (smokeEmitter)
        {
            smokeEmitter.emissionRate = MAX_SMOKE_RATE * (1 - health / MAX_HEALTH);
        }
        if (health <= 0)
        {
            Explode();
            // Удаление
            Destroy(gameObject);
        }
    }
}
