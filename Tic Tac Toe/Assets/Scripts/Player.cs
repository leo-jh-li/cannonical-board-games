using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
	NONE,
	ONE, // Player 1 / X / blue
	TWO // Player 2 / O / red
};

public class Player : MonoBehaviour {

	//TODO
	//material/colour
	//public GameObject cannon { get; set; }
	public PlayerType playerType;
	public string playerName;
	public Material playerMaterial;

	//public Player(PlayerType playerType)
	//{

	//}

}
