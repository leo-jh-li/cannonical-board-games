using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
	NONE,
	X,
	O
}

public class Player : MonoBehaviour {

	//TODO
	//material/colour
	//public GameObject cannon { get; set; }
	public PlayerType playerType;
	public Material playerMaterial;

	//public Player(PlayerType playerType)
	//{

	//}

}
