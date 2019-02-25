using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCell : Cell {

	public GameObject blueCell;
	public GameObject redCell;
	public float cellLockAnimTime;

	private void OnTriggerEnter(Collider other)
	{
		if (owner == null)
		{
			Cannonball cannonball = other.GetComponent<Cannonball>();
			if (cannonball != null)
			{
				Debug.Log("HIT!");
				cannonball.ConvertToCell();
				Vector3 destination = transform.position;
				cannonball.transform.SetParent(transform);
				StartCoroutine(AnimateCellLock(cannonball));
				owner = cannonball.player;
				GetComponent<Collider>().isTrigger = false;
				board.CheckGameOver(this);
			}
		}
	}

	IEnumerator AnimateCellLock(Cannonball cannonball)
	{
		Transform goalTransform = transform; // Initialize
		if (cannonball.player.playerType == PlayerType.X)
		{
			goalTransform = blueCell.transform.GetChild(0);
		}
		else if (cannonball.player.playerType == PlayerType.O)
		{
			goalTransform = redCell.transform.GetChild(0);
		}
		Vector3 startPosition = transform.position;
		Vector3 startRotation = cannonball.transform.localEulerAngles;
		Vector3 startScale = cannonball.transform.localScale;
		float i = 0;
		float rate = 1 / cellLockAnimTime;
		while (i < 1)
		{
			i += Time.deltaTime * rate;
			cannonball.transform.position = Vector3.Lerp(startPosition, transform.position, i);
			cannonball.transform.localScale = Vector3.Lerp(startScale, goalTransform.localScale, i);
			cannonball.transform.localEulerAngles = Vector3.Lerp(startRotation, goalTransform.localEulerAngles, i);
			yield return null;
		}
	}
}
