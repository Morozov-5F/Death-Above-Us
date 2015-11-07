using UnityEngine;
using System.Collections.Generic;

public class AsteroidSpawner : MonoBehaviour 
{
	public List<Sprite> AsteroidSprites;
	public GameObject AsteroidPrefab; 

	public float SpawnDelay = 1;
	public int MinAsteroidsCount = 0;
	public int MaxAsteroidsCount = 1;

	public int DispersionAngle = 0;

	private float currentSpawnDelay;

	void Start () 
	{
		Debug.Assert (AsteroidSprites.Count != 0, "Asteroid textures not set!");
		Debug.Assert (AsteroidPrefab != null, "Asteroid prefab is null!");

		currentSpawnDelay = 0;
	}

	void Update () 
	{
		if (currentSpawnDelay >= SpawnDelay) 
		{
			int asteroidsCountToSpawn = Random.Range(MinAsteroidsCount, MaxAsteroidsCount + 1);
			float baseAngle = transform.eulerAngles.z;
			for (int i = 0; i < asteroidsCountToSpawn; ++i)
			{
				float currentAngle = (Random.Range(-DispersionAngle / 2, DispersionAngle / 2) + baseAngle) * Mathf.Deg2Rad;
				float velocityRandom = Random.Range(0.5f, 1.5f);
				Vector3 direction = new Vector3(Mathf.Cos(currentAngle), Mathf.Sin(currentAngle)) * velocityRandom;

				var spawnedAsteroid = 
					(Instantiate(AsteroidPrefab, transform.position, Quaternion.Euler(Vector3.zero)) as GameObject).GetComponent<AsteroidController>();

				spawnedAsteroid.transform.position = transform.position;
				spawnedAsteroid.AseroidSprite = AsteroidSprites[Random.Range(0, AsteroidSprites.Count)];
				spawnedAsteroid.Direction = direction;
			}
			currentSpawnDelay = 0;
		}
		currentSpawnDelay += Time.deltaTime;
	}
}
