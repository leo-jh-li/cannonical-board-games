using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour {
	public Player player;
	public CannonController cannon;

	public void ConvertToCell()
	{
		Destroy(GetComponent<Collider>());
		Destroy(GetComponent<Rigidbody>());
	}
}
