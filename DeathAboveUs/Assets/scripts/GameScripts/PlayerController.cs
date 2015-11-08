using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    public float INPUT_SENSITIVITY = 0.5f;
    public float MAX_ANGLE = 65f;

    // Предыдущее положение мыши
    private Vector3 dragOrigin;
	
	void Start () 
	{
        #if UNITY_EDITOR
        Input.simulateMouseWithTouches = true;
        #else
        Input.simulateMouseWithTouches = false;
        #endif
    }

    void Rotate(float angle)
    {
        var currentRotation = transform.localEulerAngles.z;
        currentRotation += angle * INPUT_SENSITIVITY;
        currentRotation = Mathf.Clamp(currentRotation > 180 ? currentRotation - 360 : currentRotation, -MAX_ANGLE, MAX_ANGLE);
        transform.localEulerAngles = new Vector3(0f, 0f, currentRotation);
    }
	
	void Update () 
	{
        float inputDeltaX = 0f;
        // Управление мышью только в редакторе
        #if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                dragOrigin = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                var dragDelta = dragOrigin - Input.mousePosition;
                dragOrigin = Input.mousePosition;

                inputDeltaX = dragDelta.x;
            }
#else
        foreach (var touch in Input.touches)
        {
            if (touch.position.x >= Screen.width / 2f)
            {
                inputDeltaX = -touch.deltaPosition.x;
                break;
            }
        }
#endif

        Rotate(inputDeltaX);
    }
}
