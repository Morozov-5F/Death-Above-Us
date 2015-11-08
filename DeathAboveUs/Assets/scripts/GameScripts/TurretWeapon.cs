using UnityEngine;
using System.Collections.Generic;

public class TurretWeapon : MonoBehaviour 
{
	public Transform Projectile;

	public float ReloadTime = 200;
	private float currentReloadTime;

	public List<GameObject> Barrels;
	
	void Start () 
	{
		currentReloadTime = 0;
	}
	
	void Update () 
	{
		bool input = false;
#if UNITY_EDITOR
		input = Input.GetKey(KeyCode.Space);
#else
        foreach (var currentTouch in Input.touches) 
        {
            // TODO: add input align (left or right)
            if (currentTouch.phase != TouchPhase.Ended && currentTouch.phase != TouchPhase.Canceled)
            {
                input = (currentTouch.position <= GameUtils.cameraWidth * GameUtils.PIXELS_IN_UNITS)
            }
        }
#endif
		if (input && currentReloadTime >= ReloadTime) 
		{
            float angle = transform.localEulerAngles.z * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle));
            foreach (var currentBarrel in Barrels)
            {
                var currentProjectile = Instantiate(Projectile, currentBarrel.transform.position, transform.rotation) as GameObject;
            }
            currentReloadTime = 0;
		}
		else 
        {
            currentReloadTime += Time.deltaTime * 1000;
		}
	}
}
