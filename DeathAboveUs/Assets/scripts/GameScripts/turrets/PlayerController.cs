using UnityEngine;
using System.Collections;

public class PlayerController : Turret 
{
    private Vector3 dragOrigin;
	new void Start () 
	{
#if UNITY_EDITOR
        Input.simulateMouseWithTouches = true;
#else
        Input.simulateMouseWithTouches = false;
#endif
        base.Start();
    }
    
	void Update () 
	{
        float inputDeltaX = 0f;
        // Управление мышью только в редакторе
        weapon.isShooting = false;
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

        // Стрельба
        if (Input.GetKey(KeyCode.Space))
        {
            weapon.isShooting = true;
        }
#else
        foreach (var touch in Input.touches)
        {
            if (touch.position.x >= Screen.width / 2f)
            {
                inputDeltaX = -touch.deltaPosition.x;
            }
            else
            {
                weapon.isShooting = true;
            }
        }
#endif
        Rotate(inputDeltaX);
    }
    
    new public void OnBulletHit(float damage)
    {
        hp -= damage;
        
        if (hp <= 0)
        {
            // Пока костыль
            var toDestroy = GameObject.Find("Player");
            Destroy(toDestroy);
            // Вызов экрана Game Over 
            var ui = Camera.main.GetComponent<GameUIController>();
            ui.ShowDeathUI();
        }   
    }
}
