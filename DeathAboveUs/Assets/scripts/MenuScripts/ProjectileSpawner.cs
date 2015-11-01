using UnityEngine;
using System.Collections;

public class ProjectileSpawner : MonoBehaviour 
{
	public GameObject template;
	public float MIN_PROJECTILE_VELOCITY = 1;
	public float MAX_PROJECTILE_VELOCITY = 2;
	public float MIN_SPAWN_INTERVAL = 1;
	public float MAX_SPAWN_INTERVAL = 5;
	private float current_spawn_time;
	private float spawn_timer_value;
	void Start () 
	{
		//  Debug.Assert(template == null, "Projectile Spawner: template object is null!");
		
		current_spawn_time = Random.Range(MIN_SPAWN_INTERVAL, MAX_SPAWN_INTERVAL);
		spawn_timer_value = 0;
	}
	
	void Update () 
	{
		spawn_timer_value += Time.deltaTime;
		if (spawn_timer_value >= current_spawn_time)
		{
			float projectie_velocity = Random.Range(MIN_PROJECTILE_VELOCITY, MAX_PROJECTILE_VELOCITY);
			float angle = Random.Range(-Mathf.PI / 3.0f, Mathf.PI / 3.0f);
			float position_x = Random.Range(-1.0f, 1.0f);
			GameObject projectile = Instantiate(template, new Vector3(position_x, -1, 0), Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg)) as GameObject;
			projectile.SendMessage("Initialize", projectie_velocity);
			
			current_spawn_time = Random.Range(MIN_SPAWN_INTERVAL, MAX_SPAWN_INTERVAL);
			spawn_timer_value = 0;
		}
	}
}
