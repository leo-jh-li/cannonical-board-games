using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cell : MonoBehaviour
{
	public BoardManager board { get; set; }
	[HideInInspector]
	public Player owner;
	public Coord coord { get; set; }
}
