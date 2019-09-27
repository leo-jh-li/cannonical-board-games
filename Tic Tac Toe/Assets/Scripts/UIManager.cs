using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public RectTransform gameCanvas;
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
			winnerText.text = winner.playerName + " WINS";
			winnerTextShadow.text = winner.playerName + " WINS";
		}
		gameOverGraphic.SetActive(true);
	}

	public void EraseGameOver()
	{
		gameOverGraphic.SetActive(false);
	}
}
