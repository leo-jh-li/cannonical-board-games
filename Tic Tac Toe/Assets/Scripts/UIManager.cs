using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public GameObject gameOverGraphic;
	public Image chargeBar;
	public Text winnerText;
	public Text winnerTextShadow;

	public void UpdateChargeBar(float percent)
	{
		chargeBar.fillAmount = percent;
	}

	public void ShowGameOver(Player winner)
	{
		if (winner == null)
		{
			winnerText.text = "DRAW";
			winnerText.color = Color.white;
			winnerTextShadow.text = "DRAW";
		}
		else
		{
			winnerText.color = winner.playerMaterial.color;
			if (winner.playerType == PlayerType.X)
			{
				winnerText.text = "X WINS";
				winnerTextShadow.text = "X WINS";
			}
			else if (winner.playerType == PlayerType.O)
			{
				winnerText.text = "O WINS";
				winnerTextShadow.text = "O WINS";
			}
		}
		gameOverGraphic.SetActive(true);
	}

	public void EraseGameOver()
	{
		gameOverGraphic.SetActive(false);
	}
}
