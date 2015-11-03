using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    private Vector3 dragOrigin;
    public float MAX_ANGLE = 65f;
	
	void Start () 
	{
        Input.simulateMouseWithTouches = true;
	}
	
	void Update () 
	{
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            var dragDelta = dragOrigin - Input.mousePosition;
            dragOrigin = Input.mousePosition;

            var currentRotation = transform.localEulerAngles.z;
            currentRotation += dragDelta.x;
            currentRotation = Mathf.Clamp(currentRotation > 180 ? currentRotation - 360 : currentRotation, -MAX_ANGLE, MAX_ANGLE);
            transform.localEulerAngles = new Vector3(0f, 0f, currentRotation);
        }
	}
}
