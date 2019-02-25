using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
	public Vector3 cameraOffset;

	// Default values
	public float defaultShakeDuration;
	public float defaultShakeMagnitude;
	public float defaultDampingSpeed;

	private float currShakeDuration;
	private float currShakeMagnitude;
	private float currDampingSpeed;

	public void MoveBehindObject(Transform dest, Vector3 offset, Vector3 rotation)
	{
		transform.position = dest.position + new Vector3(offset.x, offset.y, offset.z * -dest.forward.z);
		transform.localEulerAngles = dest.localEulerAngles + rotation;
	}

	IEnumerator Shake()
	{
		Vector3 startingPosition = transform.localPosition;
		while (true)
		{
			if (currShakeDuration > 0)
			{
				transform.localPosition = startingPosition + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1)) * currShakeMagnitude;
				currShakeDuration -= Time.deltaTime * currDampingSpeed;
			}
			else
			{
				transform.localPosition = startingPosition;
				break;
			}
			yield return null;
		}
	}

	public void TriggerShake()
	{
		currShakeDuration = defaultShakeDuration;
		currShakeMagnitude = defaultShakeMagnitude;
		currDampingSpeed = defaultDampingSpeed;
		StartCoroutine(Shake());
	}

	public void TriggerShake(float duration = 0, float magnitude = 0, float dampingSpeed = 0)
	{
		if (duration == 0)
		{
			currShakeDuration = defaultShakeDuration;
		}
		else
		{
			currShakeDuration = duration;
		}
		if (magnitude == 0)
		{
			currShakeMagnitude = defaultShakeMagnitude;
		}
		else
		{
			currShakeMagnitude = magnitude;
		}
		if (dampingSpeed == 0)
		{
			currDampingSpeed = defaultDampingSpeed;
		}
		else
		{
			currDampingSpeed = dampingSpeed;
		}
		StartCoroutine(Shake());
	}
}
