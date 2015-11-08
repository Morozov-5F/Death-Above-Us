using UnityEngine;
using System.Collections;

public class GameInitializationController : MonoBehaviour {

	void Awake ()
	{
		Application.targetFrameRate = 60;

        GameUtils.cameraHeight = Camera.main.orthographicSize * 2f;
        GameUtils.cameraWidth = GameUtils.cameraHeight * Camera.main.aspect;
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
