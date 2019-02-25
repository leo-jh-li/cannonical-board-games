using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4Game: Game
{

	//TODO
	public override void EndGame(Player winner)
	{
		gameState = GameState.GAMEOVER;

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
