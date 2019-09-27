using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CannonState
{
	INACTIVE,
	ACTIVE
}

public class CannonController : MonoBehaviour {

	public UIManager uiManager;
	public Game game;
	public Player player;
	public Transform spawnPoint;

	// Rotation limits
	public float maxBotRotation;
	public float maxTopRotation;
	public float maxLeftRotation;
	public float maxRightRotation;

	public CannonState cannonState;
	public GameObject projectilePrefab;
	public float projectileTorque;

	// Charging variables
	private bool charging;
	[SerializeField] //TODO:temp
	private float currMagnitude;
	public float maxMagnitude;
	public float magnitudeIncrement;

	private Camera cam;

	private void Awake()
	{
		cannonState = CannonState.INACTIVE;
		charging = false;
		cam = Camera.main;
	}

	void Update () {
		if (cannonState == CannonState.ACTIVE)
		{
			if (Input.GetButton("Vertical"))
			{
				transform.Rotate(Vector3.left, Input.GetAxis("Vertical"));
			}
			if (Input.GetButton("Horizontal"))
			{
				transform.Rotate(Vector3.up * Input.GetAxis("Horizontal"), Space.World);
			}

			// Clamp rotations
			float rotationX = Mathf.Clamp(transform.localEulerAngles.x, maxTopRotation, maxBotRotation);
			float rotationY = transform.localEulerAngles.y;
			if (rotationY > maxRightRotation && rotationY < maxLeftRotation)
			{
				if (Mathf.Abs(maxLeftRotation - rotationY) < Mathf.Abs(maxRightRotation - rotationY))
				{
					rotationY = maxLeftRotation;
				}
				else
				{
					rotationY = maxRightRotation;
				}
			}
			transform.localEulerAngles = new Vector3(rotationX, rotationY, transform.localEulerAngles.z);

			if (Input.GetButtonDown("Jump"))
			{
				charging = true;
				StartCoroutine(Charge());
			}
			if (Input.GetButtonUp("Jump"))
			{
				Fire();
			}
			if (Input.GetButtonDown("Fire2"))
			{
				game.ExitGame();
			}
		}
	}

	IEnumerator Charge()
	{
		while (charging)
		{
			currMagnitude += magnitudeIncrement;
			currMagnitude = currMagnitude > maxMagnitude ? maxMagnitude : currMagnitude;
			uiManager.UpdateChargeBar(currMagnitude/maxMagnitude);
			yield return new WaitForFixedUpdate();
		}
		yield return null;
	}

	private void Fire()
	{
		if (charging)
		{
			GameObject cannonBall = Instantiate(projectilePrefab, spawnPoint.transform.position, transform.rotation, transform.parent);
			cannonBall.GetComponent<Rigidbody>().AddForce(transform.forward * currMagnitude, ForceMode.Impulse);
			Vector3 torqueDirection = Random.insideUnitCircle.normalized;
			cannonBall.GetComponent<Rigidbody>().AddTorque(torqueDirection * projectileTorque);
			cannonBall.GetComponent<Cannonball>().player = player;
			cannonBall.GetComponent<Cannonball>().cannon = this;
			cannonState = CannonState.INACTIVE;
			currMagnitude = 0;
			charging = false;
			cam.GetComponent<CameraBehaviour>().TriggerShake();
		}
	}
}
