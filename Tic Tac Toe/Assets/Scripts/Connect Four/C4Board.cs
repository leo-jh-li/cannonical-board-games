using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4Board : TBoard
{
	protected override void GetCellWidth()
	{
		GameObject cell = Instantiate(cellPrefab);
		cellWidth = cell.GetComponentInChildren<Renderer>().bounds.size.x;
		Destroy(cell);
	}
}
