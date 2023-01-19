using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4Game: Game
{
	protected override void SetupLayout() {
		base.SetupLayout();
		mainCam.gameObject.SetActive(true);
		mainCam.rect = new Rect(0, 0.5f, 1, 1);
		c4BoardCam.gameObject.SetActive(true);
		c4BoardCam.rect = new Rect(0, 0, 1, 0.5f);

		// Move charge bar to upper half of screen
		chargeContent.localPosition = new Vector2(gameManager.chargeContentDefaultPos.x, uiManager.gameCanvas.rect.height / 4);
	}

    protected override void InitializeGame() {
        base.InitializeGame();
		cannons[0].player.playerName = "BLUE";
		cannons[1].player.playerName = "RED";
    }
}
