using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TGame : Game
{
	public void RestartGame()
	{
		//TODO
		Debug.Log("restarted");
		uiManager.EraseGameOver();
		foreach (CannonController cannon in cannons)
		{
			Destroy(cannon.transform.parent.gameObject);
		}
		cannons.Clear();
		board.ResetBoard();
		StartGame();
	}

	public override void EndGame(Player winner)
	{
		gameState = GameState.GAME_OVER;

		//TODO
		Debug.Log("GAME OVER");
		uiManager.ShowGameOver(winner);
		if (winner == null)
		{
			Debug.Log("DRAW");
		}
		else
		{
			Debug.Log(winner.playerType + " WINS");
		}
		GetComponent<GameOverController>().enabled = true;
	}
}
