using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
	void Update()
    {
		if (Input.GetButtonDown("Jump"))
		{
			GetComponent<TGame>().RestartGame();
			this.enabled = false;
		}
	}
}
