using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour {
	public Player player;
	public CannonController cannon;
	public bool expended = false;

	public void OnHitCell()
	{
		expended = true;
		Destroy(GetComponent<Collider>());
		Destroy(GetComponent<Rigidbody>());
	}
}
