using UnityEngine;
using System.Collections;

public class UIInputController : MonoBehaviour 
{
	public void StartGameButtonClickHandler()
	{
		Debug.Log("Clicked!");
		// Потом сделать сцену для каждого уровня? 
		Application.LoadLevel(1);
	}
	
	void Start () {
	
	}
	
	void Update () {
	
	}
}
