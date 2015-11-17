using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    private PlayerController playerTurret;
    private float maxPositionOffset;
    private const float backgroundLayerWidth = 4f;

    // Тряска камеры
    public float shakeRange = 0.0025f;
    public float shakeDelayMax = 0.5f;
    private float shakeDelay;

    void Start () {
        shakeDelay = 0f;

        var playerGameObject = GameObject.Find("Player");
        playerTurret = playerGameObject.GetComponent<PlayerController>();

        // Вычисление максимального смещения камеры по X
        maxPositionOffset = Mathf.Max(0f, (backgroundLayerWidth - GameUtils.cameraWidth) / 2f);
    }

    public void Shake()
    {
        shakeDelay = Mathf.Min(shakeDelayMax * 2f, shakeDelay + shakeDelayMax);
    }

	void Update () 
    {

        // Тряска камеры
        if (shakeDelay > 0)
        {
            shakeDelay -= Time.deltaTime;
            var shakeMul = shakeDelay / shakeDelayMax;
            transform.Translate(Random. Range(-shakeRange, shakeRange) * shakeMul, 0f, 0f);
        }
        
        // Еще один костыль?
        if (playerTurret == null)
            return;
        
        // Вычисления поворота турели
        float turretRotationOffset = playerTurret.turret.transform.localEulerAngles.z;
        if (turretRotationOffset > 180)
            turretRotationOffset -= 360;
        turretRotationOffset /= playerTurret.MAX_ANGLE;
           
        // Смещение камеры
        var currentPositon = transform.localPosition;
        currentPositon.x = -turretRotationOffset * maxPositionOffset;
        transform.localPosition = new Vector3(currentPositon.x, currentPositon.y, currentPositon.z);
    
    }
}
