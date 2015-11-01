using UnityEngine;

public class StarsMovementController : MonoBehaviour 
{
	public float velocity = 0.3f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate(0f, velocity * Time.deltaTime, 0);
		if (transform.position.y >= 2)
			transform.Translate(0, -2, 0);
	}
}
