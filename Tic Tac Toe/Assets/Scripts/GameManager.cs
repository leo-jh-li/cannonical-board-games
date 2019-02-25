using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject[] games;

	//TODO: temp. true to play TTT, false to play C4
	public bool playTicTacToe;

    void Start()
    {
        if (playTicTacToe)
		{
			games[0].SetActive(true);
		} else
		{
			games[1].SetActive(true);
		}
	}
}
