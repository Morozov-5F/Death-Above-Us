using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    private PlayerController playerTurret;
    private float maxPositionOffset;
    private const float backgroundLayerWidth = 4f;

    void Start () {
        var playerGameObject = GameObject.Find("Player");
        playerTurret = playerGameObject.transform.Find("turret").GetComponent<PlayerController>();

        // Вычисление максимального смещения камеры по X
        maxPositionOffset = Mathf.Max(0f, (backgroundLayerWidth - GameUtils.cameraWidth) / 2f);
    }

	void Update () {
        // Вычисления поворота турели
        float turretRotationOffset = playerTurret.transform.localEulerAngles.z;
        if (turretRotationOffset > 180)
            turretRotationOffset -= 360;
        turretRotationOffset /= playerTurret.MAX_ANGLE;
           
        // Смещение камеры
        var currentPositon = transform.localPosition;
        currentPositon.x = -turretRotationOffset * maxPositionOffset;
        transform.localPosition = new Vector3(currentPositon.x, currentPositon.y, currentPositon.z);
    }
}
