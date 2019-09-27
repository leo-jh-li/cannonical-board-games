using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TGame : Game
{
    protected override void InitializeGame() {
        base.InitializeGame();
		cannons[0].player.playerName = "X";
		cannons[1].player.playerName = "O";
    }
}