using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RandomRange
{
	public float min;
	public float max;
	
	public float GetRandom()
	{
		return Random.Range(min, max);
	}
}
