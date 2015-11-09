using UnityEngine;
using System.Collections;

public class GameUIController : MonoBehaviour {
    public GameObject gameUI;
    public GameObject deathUI;

	void Start () {
        if (gameUI == null)
            gameUI = GameObject.Find("Game UI");
        if (deathUI == null)
            deathUI = GameObject.Find("Death UI");

        ShowGameUI();
    }

    public void HideUI()
    {
        gameUI.SetActive(false);
        deathUI.SetActive(false);
    }

    public void ShowGameUI()
    {
        HideUI();
        gameUI.SetActive(true);
    }

    public void ShowDeathUI()
    {
        HideUI();
        deathUI.SetActive(true);
    }

    // Обработка кнопок
    public void DeathUIBackButtonClick()
    {
        Application.LoadLevel("menu_scene");
    }
}
