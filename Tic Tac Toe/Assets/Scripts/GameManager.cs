using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum BoardGame {
	TIC_TAC_TOE,
	CONNECT_FOUR
};

public class GameManager : MonoBehaviour
{
	public GameObject[] games;
	[HideInInspector]
	public Game activeGame;
	public GameObject menuCanvas;
	public GameObject gameCanvas;

	public Selectable firstButton;

	public RectTransform chargeContent;
	public Vector2 chargeContentDefaultPos;

	// For debugging; if set to true, gameToLoad is started automatically
	public bool skipMenu;
	public BoardGame gameToLoad;

    void Start()
    {
		// Get charge bar's default values
		chargeContentDefaultPos = chargeContent.localPosition;
		gameCanvas.SetActive(false);
		firstButton.Select();
        if (skipMenu)
		{
			StartGame((int)gameToLoad);
		}
	}

	public void StartGame(int game)
	{
		menuCanvas.SetActive(false);
		gameCanvas.SetActive(true);
		games[game].SetActive(true);
		activeGame = games[game].GetComponent<Game>();
		activeGame.StartGame();
	}

	public void ReturnToMenu() {
		activeGame.gameObject.SetActive(false);
		gameCanvas.SetActive(false);
		menuCanvas.SetActive(true);
		firstButton.Select();
	}

	void Update() {
		// If no button is selected and the player tries to navigate, select the first button
		if (Input.GetButton("Vertical") || Input.GetButton("Horizontal"))
		{
			if (EventSystem.current.currentSelectedGameObject == null) {
				firstButton.Select();
			}
		}
	}
}
