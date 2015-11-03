using UnityEngine;
using System.Collections;

public class ScrollingBackground : MonoBehaviour {
    public float MAX_OFFSET = 0.1f;
    public GameObject[] layers;
    public float[] layersSpeed;
    private PlayerController turret;

	void Start () {
        var playerGameObject = GameObject.Find("Player");
        turret = playerGameObject.transform.Find("turret").GetComponent<PlayerController>();
    }
	
	void Update () {
        //Debug.Log("Wat");
        var turretRotationOffset = turret.transform.localEulerAngles.z;
        if (turretRotationOffset > 180)
            turretRotationOffset -= 360;
        turretRotationOffset /= turret.MAX_ANGLE;

        for (int i = 0; i < layers.Length; i++)
        {
            var layer = layers[i];
            var currentPos = layer.transform.localPosition;
            currentPos.x = turretRotationOffset * MAX_OFFSET * layersSpeed[i];
            layer.transform.localPosition = new Vector3(currentPos.x, currentPos.y, currentPos.z);
        }
	}
}
