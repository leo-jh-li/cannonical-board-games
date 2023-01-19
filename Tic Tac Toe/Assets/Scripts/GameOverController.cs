using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
	public GameManager gameManager;

	void Update()
    {
		if (Input.GetButtonDown("Jump"))
		{
			GetComponent<Game>().RestartGame();
			this.enabled = false;
		}
		if (Input.GetButtonDown("Fire2"))
		{
			gameManager.activeGame.ExitGame();
			this.enabled = false;
		}
	}
}
