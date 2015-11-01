using UnityEngine;

public class BackgroundUnitMovementController : MonoBehaviour 
{
	public const float MAX_VELOCITY = -0.5f;
	public float layer_depth = 1;
	void Start () 
	{
		if (layer_depth > 1)
			layer_depth = 1;
	}
	
	void Update () 
	{
		transform.Translate(MAX_VELOCITY * layer_depth * Time.deltaTime, 0, 0);
		var postitionX = transform.position.x;
		if (postitionX >= 0 && MAX_VELOCITY > 0)
			transform.Translate(-4, 0, 0);
		if (postitionX <= -4 && MAX_VELOCITY < 0)
			transform.Translate(4, 0, 0);
	}
}
