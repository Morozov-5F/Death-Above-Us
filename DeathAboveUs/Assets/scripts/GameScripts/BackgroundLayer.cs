using UnityEngine;
using System.Collections;

public class BackgroundLayer : MonoBehaviour
{
    /* Смещение положения слоя относительно камеры
        0 - слой неподвижен
        1 - слой движется вместе с камерой
    */
    public float layerOffset = 0f;
    
    // Положение слоя обновляется в LateUpdate, чтобы слой плавно следовал за камерой
	void LateUpdate()
    {
        float cameraX = Camera.main.transform.position.x;
        var currentPositon = transform.localPosition;
        currentPositon.x = cameraX * layerOffset;
        transform.localPosition = new Vector3(currentPositon.x, currentPositon.y, currentPositon.z);
    }
}
